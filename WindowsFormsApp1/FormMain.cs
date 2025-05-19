using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string filePath = "vendor_orders.csv";

            // A fejléc tartalma – ezt állítsd be a tényleges oszlopneveid alapján
            string header = "ProductBvin;VendorId;InventoryBvin;Sku;ProductName;QuantityNeeded;SiteCostTotal";

            try
            {
                File.WriteAllText(filePath, header + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a fájl inicializálásakor: " + ex.Message);
            }
        }

        private void buttonInventory_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            UserControlInventory uc = new UserControlInventory();
            panel1.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;

        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            UserControlOrder uc = new UserControlOrder();
            panel1.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;

        }

        private void buttonUploadPage_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            UserControlUploadData uc = new UserControlUploadData();
            panel1.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }

        
    }
}
