using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            buttonInventory.Enabled = true;
            buttonOrder.Enabled = false;
            buttonUploadPage.Enabled = false;
        }

        private void buttonInventory_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            UserControlInventory uc = new UserControlInventory();
            panel1.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            buttonInventory.Enabled = false;
            buttonOrder.Enabled = true;
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            UserControlOrder uc = new UserControlOrder();
            panel1.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            buttonInventory.Enabled = false;
            buttonOrder.Enabled = false;
            buttonUploadPage.Enabled = true;
        }

        private void buttonUploadPage_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            UserControlUploadData uc = new UserControlUploadData();
            panel1.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            buttonInventory.Enabled = false;
            buttonOrder.Enabled = false;
            buttonUploadPage.Enabled = false;
        }
    }
}
