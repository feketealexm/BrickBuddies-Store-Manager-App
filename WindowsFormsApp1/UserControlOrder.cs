using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.Classes;

namespace WindowsFormsApp1
{
    public partial class UserControlOrder : UserControl
    {
        private const string ApiKey = "1-9f81dabb-d513-43b3-a568-13b7282b6ce0";
        private const string BaseUrl = "http://rendfejl10003.northeurope.cloudapp.azure.com/DesktopModules/Hotcakes/API/rest/v1";
        private List<VendorOrderDisplay> allOrders = new List<VendorOrderDisplay>();

        public UserControlOrder()
        {
            InitializeComponent();
        }

        private void listBoxVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxVendors.SelectedValue is string selectedVendorId)
            {
                var filtered = allOrders
                    .Where(order => listBoxVendors.Text == order.Vendor)
                    .ToList();

                dataGridViewOrder.DataSource = filtered;
            }
        }

        private void buttonShowAll_Click_1(object sender, EventArgs e)
        {
            dataGridViewOrder.DataSource = null;
            dataGridViewOrder.DataSource = allOrders;
            listBoxVendors.ClearSelected();
        }

        private void dataGridViewOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is decimal || e.Value is double || e.Value is float)
            {
                e.Value = string.Format("{0:0.## HUF}", e.Value);
                e.FormattingApplied = true;
            }
        }



        private async Task<List<Vendor>> GetVendorsAsync()
        {
            using (var client = new HttpClient())
            {
                var url = $"{BaseUrl}/vendors?key={ApiKey}";
                var response = await client.GetStringAsync(url);

                var vendorResponse = JsonConvert.DeserializeObject<VendorApiResponse>(response);
                return vendorResponse != null ? vendorResponse.Content : new List<Vendor>();
            }
        }


        private void buttonCSVsave_Click(object sender, EventArgs e)
        {
            string originalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vendor_orders.csv");

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV fájl (*.csv)|*.csv";
                saveFileDialog.Title = "Rendelés mentése";
                saveFileDialog.FileName = "BrickBuddies_Order_Insert.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (!File.Exists(originalPath))
                        {
                            MessageBox.Show("Az eredeti fájl nem található.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var lines = File.ReadAllLines(originalPath);
                        var outputLines = new List<string>();

                        // Fejléc hozzáadása
                        outputLines.Add("InventoryBvin;QuantityNeeded");

                        foreach (var line in lines.Skip(1))
                        {
                            var parts = line.Split(';');
                            if (parts.Length >= 6)
                            {
                                string inventoryBvin = parts[2].Trim();
                                string quantityNeeded = parts[5].Trim();
                                outputLines.Add($"{inventoryBvin};{quantityNeeded}");
                            }
                        }

                        File.WriteAllLines(saveFileDialog.FileName, outputLines, Encoding.UTF8);

                        MessageBox.Show("Az beérkeztetés oldalra feltöltendő fájlt sikeresen elmentette a saját meghajtójára.", "Sikeres mentés!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hiba történt a mentés során: " + ex.Message);
                    }
                }
            }
        }

        private void buttonFormattedCSVsave_Click(object sender, EventArgs e)
        {
            if (allOrders == null || allOrders.Count == 0)
            {
                MessageBox.Show("Nincs menthető rendelés.");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV fájl (*.csv)|*.csv";
                saveFileDialog.Title = "Rendelés mentése";
                saveFileDialog.FileName = "BrickBuddies_Order.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                        {
                            writer.WriteLine("Vendor;Sku;ProductName;QuantityNeeded;SiteCostTotal");

                            foreach (var order in allOrders)
                            {
                                writer.WriteLine($"{order.Vendor};{order.Sku};{order.ProductName};{order.QuantityNeeded};{order.SiteCostTotal.ToString("N0", new CultureInfo("hu-HU"))}");
                            }
                        }

                        MessageBox.Show("A rendelést tartalmazó fájlt sikeresen elmentette a saját meghajtójára.", "Sikeres mentés!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hiba történt a mentés során: " + ex.Message);
                    }
                }
            }
        }

        private async void UserControlOrder_Load(object sender, EventArgs e)
        {
            try
            {
                // Vendor API-ból töltés
                var vendors = await GetVendorsAsync();

                listBoxVendors.DataSource = vendors;
                listBoxVendors.DisplayMember = "DisplayName";
                listBoxVendors.ValueMember = "Bvin";

                // Szótár a gyors kereséshez
                var vendorDict = vendors.ToDictionary(v => v.Bvin, v => v.DisplayName);

                // CSV betöltés
                var csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vendor_orders.csv");
                if (!File.Exists(csvPath))
                {
                    MessageBox.Show("A vendor_orders.csv fájl nem található.");
                    return;
                }

                var lines = File.ReadAllLines(csvPath).Skip(1); // fejléc kihagyása
                var orders = new List<VendorOrderDisplay>();
                decimal totalCost = 0; // Összes rendelés ára

                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length < 7) continue;

                    string vendorId = parts[1].Trim();
                    string vendorName = vendorDict.TryGetValue(vendorId, out var name) ? name : "Ismeretlen szállító";

                    string sku = parts[3].Trim();
                    string productName = parts[4].Trim();

                    if (!int.TryParse(parts[5].Trim(), out int quantity)) quantity = 0;

                    // A magyar vesszős tizedesek miatt "hu-HU" kultúra használata
                    if (!decimal.TryParse(parts[6].Trim(), NumberStyles.Any, new CultureInfo("hu-HU"), out decimal cost))
                        cost = 0;

                    orders.Add(new VendorOrderDisplay
                    {
                        Vendor = vendorName,
                        Sku = sku,
                        ProductName = productName,
                        QuantityNeeded = quantity,
                        SiteCostTotal = cost
                    });

                    totalCost += cost; // Hozzáadás az összárhoz
                }

                // Kiírás a DataGridView-be
                allOrders = orders;
                dataGridViewOrder.DataSource = allOrders;

                // Összköltség megjelenítése a label-ben
                labelCost.Text = $"Rendelés ára: {totalCost:N0} HUF"; // N0: egész szám szép formázással (pl. 28 200 HUF)

                // ListBox eseménykezelő hozzárendelése
                listBoxVendors.SelectedIndexChanged += listBoxVendors_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
            }
        }
    }
}
