namespace WindowsFormsApp1
{
    partial class UserControlOrder
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewOrder = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxVendors = new System.Windows.Forms.ListBox();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.labelCost = new System.Windows.Forms.Label();
            this.buttonFormattedCSVsave = new System.Windows.Forms.Button();
            this.buttonCSVsave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewOrder
            // 
            this.dataGridViewOrder.AllowUserToAddRows = false;
            this.dataGridViewOrder.AllowUserToDeleteRows = false;
            this.dataGridViewOrder.AllowUserToResizeColumns = false;
            this.dataGridViewOrder.AllowUserToResizeRows = false;
            this.dataGridViewOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOrder.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrder.Location = new System.Drawing.Point(254, 43);
            this.dataGridViewOrder.Name = "dataGridViewOrder";
            this.dataGridViewOrder.ReadOnly = true;
            this.dataGridViewOrder.RowHeadersWidth = 51;
            this.dataGridViewOrder.RowTemplate.Height = 24;
            this.dataGridViewOrder.Size = new System.Drawing.Size(1256, 941);
            this.dataGridViewOrder.TabIndex = 1;
            this.dataGridViewOrder.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewOrder_CellFormatting);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(250, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 33);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rendelendő termékek:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 33);
            this.label1.TabIndex = 6;
            this.label1.Text = "Szállítók:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxVendors
            // 
            this.listBoxVendors.FormattingEnabled = true;
            this.listBoxVendors.ItemHeight = 16;
            this.listBoxVendors.Location = new System.Drawing.Point(6, 43);
            this.listBoxVendors.Name = "listBoxVendors";
            this.listBoxVendors.Size = new System.Drawing.Size(242, 340);
            this.listBoxVendors.TabIndex = 7;
            this.listBoxVendors.SelectedIndexChanged += new System.EventHandler(this.listBoxVendors_SelectedIndexChanged);
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowAll.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonShowAll.Location = new System.Drawing.Point(7, 384);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(241, 41);
            this.buttonShowAll.TabIndex = 8;
            this.buttonShowAll.Text = "Összes megjelenítése";
            this.buttonShowAll.UseVisualStyleBackColor = true;
            this.buttonShowAll.Click += new System.EventHandler(this.buttonShowAll_Click_1);
            // 
            // labelCost
            // 
            this.labelCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCost.Location = new System.Drawing.Point(1516, 43);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(414, 60);
            this.labelCost.TabIndex = 9;
            this.labelCost.Text = "Rendelés ára: 0 HUF";
            this.labelCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonFormattedCSVsave
            // 
            this.buttonFormattedCSVsave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFormattedCSVsave.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonFormattedCSVsave.Location = new System.Drawing.Point(1607, 384);
            this.buttonFormattedCSVsave.Name = "buttonFormattedCSVsave";
            this.buttonFormattedCSVsave.Size = new System.Drawing.Size(270, 80);
            this.buttonFormattedCSVsave.TabIndex = 10;
            this.buttonFormattedCSVsave.Text = "Formázott CSV mentése";
            this.buttonFormattedCSVsave.UseVisualStyleBackColor = true;
            this.buttonFormattedCSVsave.Click += new System.EventHandler(this.buttonFormattedCSVsave_Click);
            // 
            // buttonCSVsave
            // 
            this.buttonCSVsave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCSVsave.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonCSVsave.Location = new System.Drawing.Point(1607, 702);
            this.buttonCSVsave.Name = "buttonCSVsave";
            this.buttonCSVsave.Size = new System.Drawing.Size(270, 80);
            this.buttonCSVsave.TabIndex = 11;
            this.buttonCSVsave.Text = "Feltöltő CSV mentése";
            this.buttonCSVsave.UseVisualStyleBackColor = true;
            this.buttonCSVsave.Click += new System.EventHandler(this.buttonCSVsave_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(1607, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 133);
            this.label3.TabIndex = 12;
            this.label3.Text = "A táblázatban látható formázott lista mentése";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(1607, 566);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(270, 133);
            this.label4.TabIndex = 13;
            this.label4.Text = "A beérkeztetéshez szükséges lista mentése a beérkeztetés oldalra való feltöltéshe" +
    "z";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserControlOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCSVsave);
            this.Controls.Add(this.buttonFormattedCSVsave);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.buttonShowAll);
            this.Controls.Add(this.listBoxVendors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewOrder);
            this.Name = "UserControlOrder";
            this.Size = new System.Drawing.Size(1963, 988);
            this.Load += new System.EventHandler(this.UserControlOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxVendors;
        private System.Windows.Forms.Button buttonShowAll;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.Button buttonFormattedCSVsave;
        private System.Windows.Forms.Button buttonCSVsave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
