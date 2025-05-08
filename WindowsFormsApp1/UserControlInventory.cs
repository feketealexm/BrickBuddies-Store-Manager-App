using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.Classes;

namespace WindowsFormsApp1
{
    public partial class UserControlInventory : UserControl
    {
        private const string ApiKey = "1-9f81dabb-d513-43b3-a568-13b7282b6ce0";
        private const string BaseUrl = "http://rendfejl10003.northeurope.cloudapp.azure.com/DesktopModules/Hotcakes/API/rest/v1";

        public UserControlInventory()
        {
            InitializeComponent();
            buttonAddOptimal.Click += buttonAddOptimal_ClickAsync;
            this.Load += UserControlInventory_Load;
        }


        private async void UserControlInventory_Load(object sender, EventArgs e)
        {

            try
            {
                var products = await GetProductsAsync();
                var inventory = await GetInventoryAsync();

                // Összekötés: Product Bvin = Inventory ProductBvin
                var combinedList = from product in products
                                   join stock in inventory
                                   on product.Bvin equals stock.ProductBvin into ps
                                   from stock in ps.DefaultIfEmpty()
                                   select new ProductWithInventory
                                   {
                                       Sku = product.Sku,
                                       ProductName = product.ProductName,
                                       SitePrice = product.SitePrice,
                                       SiteCost = product.SiteCost,
                                       QuantityOnHand = stock?.QuantityOnHand ?? 0,
                                       LowStockPoint = stock?.LowStockPoint ?? 0,
                                       OptimalStock = stock?.LowStockPoint * 3 ?? 0  // OptimalStock a LowStockPoint háromszorosa
                                   };

                dataGridView1.DataSource = combinedList.ToList();

                // Formázás a számokhoz: SitePrice és SiteCost
                dataGridView1.Columns["SitePrice"].DefaultCellStyle.Format = "0 'HUF'";
                dataGridView1.Columns["SiteCost"].DefaultCellStyle.Format = "0 'HUF'";

                // Oszlopok automatikus méretezése
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Feltételes formázás a QuantityOnHand cellákhoz
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.DataBoundItem is ProductWithInventory product)
                    {
                        if (product.QuantityOnHand < product.LowStockPoint)
                        {
                            row.Cells["QuantityOnHand"].Style.BackColor = Color.Red;
                            row.Cells["QuantityOnHand"].Style.ForeColor = Color.White;
                        }
                        else if (product.QuantityOnHand < product.OptimalStock)
                        {
                            row.Cells["QuantityOnHand"].Style.BackColor = Color.Yellow;
                            row.Cells["QuantityOnHand"].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            row.Cells["QuantityOnHand"].Style.BackColor = Color.White;
                            row.Cells["QuantityOnHand"].Style.ForeColor = Color.Black;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}");
            }
        }


        private async void buttonAddOptimal_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                var products = await GetProductsAsync();
                var inventory = await GetInventoryAsync();

                var filteredList = from product in products
                                   join stock in inventory
                                   on product.Bvin equals stock.ProductBvin
                                   where stock.QuantityOnHand < stock.LowStockPoint * 3
                                   select new VendorOrder
                                   {
                                       ProductBvin = product.Bvin,
                                       VendorId = product.VendorId,
                                       InventoryBvin = stock.Bvin,
                                       Sku = product.Sku,
                                       ProductName = product.ProductName,
                                       QuantityNeeded = (stock.LowStockPoint * 3) - stock.QuantityOnHand,
                                       SiteCostTotal = product.SiteCost * ((stock.LowStockPoint * 3) - stock.QuantityOnHand)
                                   };

                var csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vendor_orders.csv");

                using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
                {
                    writer.WriteLine("ProductBvin;VendorId;InventoryBvin;Sku;ProductName;QuantityNeeded;SiteCostTotal");

                    foreach (var item in filteredList)
                    {
                        writer.WriteLine($"{item.ProductBvin};{item.VendorId};{item.InventoryBvin};{item.Sku};{item.ProductName};{item.QuantityNeeded};{item.SiteCostTotal}");
                    }
                }


                DialogResult dialogResult = MessageBox.Show("Haladjon tovább a Rendelés áttekintése menüpontra!", "Rendelés létrehozva!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
            }

        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            SearchAsync();
        }

        private async Task SearchAsync()
        {
            string filterText = textBoxFilter.Text.ToLower();  // Kisbetűsre alakítjuk a keresett szót

            var products = await GetProductsAsync();
            var inventory = await GetInventoryAsync();

            // Szűrés a terméknevek alapján
            var filteredList = from product in products
                               join stock in inventory
                               on product.Bvin equals stock.ProductBvin into ps
                               from stock in ps.DefaultIfEmpty()
                               select new ProductWithInventory
                               {
                                   Sku = product.Sku,
                                   ProductName = product.ProductName,
                                   SitePrice = product.SitePrice,
                                   SiteCost = product.SiteCost,
                                   QuantityOnHand = stock?.QuantityOnHand ?? 0,
                                   LowStockPoint = stock?.LowStockPoint ?? 0,
                                   OptimalStock = (stock?.LowStockPoint ?? 0) * 3
                               };

            // Ha van szűrési szöveg, akkor szűrjük a listát
            if (!string.IsNullOrEmpty(filterText))
            {
                filteredList = filteredList.Where(p => p.ProductName.ToLower().Contains(filterText));
            }

            // Az adatforrás beállítása a szűrt listára
            dataGridView1.DataSource = filteredList.ToList();
        }



        // Adatok lehívása
        private async Task<List<Product>> GetProductsAsync()
        {
            using (var client = new HttpClient())
            {
                var url = $"{BaseUrl}/products?key={ApiKey}";
                var response = await client.GetStringAsync(url);
                var json = JsonConvert.DeserializeObject<ProductApiResponse>(response);

                if (json?.Content?.Products == null)
                {
                    throw new Exception("Nem sikerült betölteni a termékeket!");
                }

                return json.Content.Products;
            }
        }

        private async Task<List<InventoryItem>> GetInventoryAsync()
        {
            using (var client = new HttpClient())
            {
                var url = $"{BaseUrl}/productinventory?key={ApiKey}";
                var response = await client.GetStringAsync(url);
                var json = JsonConvert.DeserializeObject<InventoryApiResponse>(response);

                if (json?.Content == null)
                {
                    throw new Exception("Nem sikerült betölteni a készlet adatokat!");
                }

                return json.Content;
            }
        }

    }
}
