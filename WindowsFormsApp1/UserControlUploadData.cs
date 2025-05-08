using Hotcakes.CommerceDTO.v1.Catalog;
using Hotcakes.CommerceDTO.v1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.Classes;
using Hotcakes.CommerceDTO.v1.Client;
using Hotcakes.CommerceDTO.v1.Catalog;
using Hotcakes.CommerceDTO.v1;

namespace WindowsFormsApp1
{
    public partial class UserControlUploadData : UserControl
    {
        private List<InventoryUpdateItem> csvInventoryList = new List<InventoryUpdateItem>();

        public UserControlUploadData()
        {
            InitializeComponent();

            this.AllowDrop = true;
            buttonEnd.Enabled = false;
            this.DragEnter += UserControlUploadData_DragEnter;
            this.DragDrop += UserControlUploadData_DragDrop;
            pictureBoxSuccess.Visible = false;
            pictureBoxDragAndDrop.Visible = true;
        }


        //Feltöltés gombbal
        private void buttonUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV fájlok (*.csv)|*.csv";
                openFileDialog.Title = "Válassz egy CSV fájlt";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    LoadCsvData(selectedFilePath);
                }
            }
        }

        //feltöltés drag and drop módszerrel
        private void UserControlUploadData_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void UserControlUploadData_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string csvPath = files[0];
                LoadCsvData(csvPath);
            }
        }


        //Feldolgozás gomb, ez az API segítségével hozzáadja a szükséges mennyiséget a termékekhez.
        private void buttonEnd_Click(object sender, EventArgs e)
        {
            buttonEnd.Enabled = false;
            pictureBoxSuccess.Visible = false;

            string apiKey = "1-9f81dabb-d513-43b3-a568-13b7282b6ce0";
            string storeUrl = "http://rendfejl10003.northeurope.cloudapp.azure.com";

            if (csvInventoryList.Count == 0)
            {
                MessageBox.Show("Nincs feldolgozandó adat a CSV-ből.", "Nincs adat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonEnd.Enabled = true;
                return;
            }

            Api proxy;
            try
            {
                proxy = new Api(storeUrl, apiKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az API kliens inicializálása közben: {ex.Message}", "API Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonEnd.Enabled = true;
                return;
            }

            StringBuilder resultsLog = new StringBuilder();
            int successCount = 0;
            int errorCount = 0;

            foreach (var item in csvInventoryList)
            {
                string inventoryBvin = item.InventoryBvin;
                int quantityNeededFromCsv = item.QuantityNeeded;

                try
                {
                    ApiResponse<ProductInventoryDTO> findResponse = proxy.ProductInventoryFind(inventoryBvin);

                    if (findResponse.Errors != null && findResponse.Errors.Any())
                    {
                        string errorMessages = string.Join("; ", findResponse.Errors.Select(err => err.Description));
                        resultsLog.AppendLine($"Hiba az '{inventoryBvin}' lekérdezésekor: {errorMessages}");
                        errorCount++;
                        continue;
                    }

                    if (findResponse.Content == null)
                    {
                        resultsLog.AppendLine($"Az '{inventoryBvin}' Bvin-nel rendelkező készlet elem nem található.");
                        errorCount++;
                        continue;
                    }

                    ProductInventoryDTO currentInventory = findResponse.Content;
                    int newQuantityOnHand = currentInventory.QuantityOnHand + quantityNeededFromCsv;
                    currentInventory.QuantityOnHand = newQuantityOnHand;

                    ApiResponse<ProductInventoryDTO> updateResponse = proxy.ProductInventoryUpdate(currentInventory);

                    if (updateResponse.Errors != null && updateResponse.Errors.Any())
                    {
                        string errorMessages = string.Join("; ", updateResponse.Errors.Select(err => err.Description));
                        resultsLog.AppendLine($"Hiba az '{inventoryBvin}' frissítésekor: {errorMessages}");
                        errorCount++;
                    }
                    else
                    {
                        resultsLog.AppendLine($"Az '{inventoryBvin}' készlete sikeresen frissítve. Régi: {newQuantityOnHand - quantityNeededFromCsv}, Hozzáadva: {quantityNeededFromCsv}, Új: {newQuantityOnHand}.");
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    resultsLog.AppendLine($"Váratlan hiba az '{inventoryBvin}' feldolgozása közben: {ex.ToString()}");
                    errorCount++;
                }
            }

            string summary = $"Feldolgozás befejezve.\nSikeres: {successCount}\nHibás: {errorCount}\n\n";
            MessageBox.Show(summary + resultsLog.ToString(), "Feldolgozási Eredmények", MessageBoxButtons.OK,
                errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            buttonEnd.Enabled = true;
            if (errorCount == 0 && successCount > 0)
            {
                pictureBoxSuccess.Visible = true;
            }
        }

        

        //CSV fájl betöltése, hibakezelés
        private void LoadCsvData(string path)
        {
            if (Path.GetExtension(path).ToLower() != ".csv")
            {
                MessageBox.Show("Csak .csv fájlokat lehet betölteni.", "Hibás formátum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (IOException)
            {
                MessageBox.Show("A fájl nem elérhető. Valószínűleg más program használja, pl. Excel.", "Fájl megnyitási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ismeretlen hiba a fájl olvasása közben: " + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lines.Length < 2)
            {
                MessageBox.Show("A fájl nem tartalmaz adatot.", "Üres vagy hiányos fájl", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string header = lines[0].Trim();
            string expectedHeader = "InventoryBvin;QuantityNeeded";

            if (!string.Equals(header, expectedHeader, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"A fájl fejléc nem megfelelő. Elvárt:\n{expectedHeader}", "Hibás fejléc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.csvInventoryList.Clear();

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 2)
                {
                    MessageBox.Show($"A(z) {i + 1}. sor hiányos, kihagyva.", "Adat hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                string inventoryBvin = parts[0].Trim();
                if (string.IsNullOrWhiteSpace(inventoryBvin))
                {
                    MessageBox.Show($"A(z) {i + 1}. sorban hiányzik az InventoryBvin, kihagyva.", "Adat hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                int quantity;
                if (!int.TryParse(parts[1].Trim(), out quantity))
                {
                    MessageBox.Show($"A(z) {i + 1}. sor Quantity értéke hibás, 0-val helyettesítve.", "Adat hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    quantity = 0;
                }

                this.csvInventoryList.Add(new InventoryUpdateItem
                {
                    InventoryBvin = inventoryBvin,
                    QuantityNeeded = quantity
                });
            }

            if (this.csvInventoryList.Count > 0)
            {
                MessageBox.Show($"Sikeresen betöltve {this.csvInventoryList.Count} sor.", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonEnd.Enabled = true;
                pictureBoxSuccess.Visible = true;
                pictureBoxDragAndDrop.Visible = false;
            }
            else
            {
                MessageBox.Show("Nem sikerült érvényes adatokat betölteni.", "Betöltési hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonEnd.Enabled = false;
                pictureBoxSuccess.Visible = false;
                pictureBoxDragAndDrop.Visible = true;
            }
        }
        
    }
}
