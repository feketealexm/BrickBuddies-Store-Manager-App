namespace WindowsFormsApp1
{
    partial class UserControlUploadData
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
            this.pictureBoxSuccess = new System.Windows.Forms.PictureBox();
            this.pictureBoxDragAndDrop = new System.Windows.Forms.PictureBox();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDragAndDrop)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxSuccess
            // 
            this.pictureBoxSuccess.Image = global::WindowsFormsApp1.Properties.Resources.uploadsuccess;
            this.pictureBoxSuccess.Location = new System.Drawing.Point(29, 22);
            this.pictureBoxSuccess.Name = "pictureBoxSuccess";
            this.pictureBoxSuccess.Size = new System.Drawing.Size(1008, 452);
            this.pictureBoxSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSuccess.TabIndex = 0;
            this.pictureBoxSuccess.TabStop = false;
            // 
            // pictureBoxDragAndDrop
            // 
            this.pictureBoxDragAndDrop.Image = global::WindowsFormsApp1.Properties.Resources.dragndrop;
            this.pictureBoxDragAndDrop.Location = new System.Drawing.Point(29, 22);
            this.pictureBoxDragAndDrop.Name = "pictureBoxDragAndDrop";
            this.pictureBoxDragAndDrop.Size = new System.Drawing.Size(1008, 452);
            this.pictureBoxDragAndDrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDragAndDrop.TabIndex = 1;
            this.pictureBoxDragAndDrop.TabStop = false;
            // 
            // buttonEnd
            // 
            this.buttonEnd.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonEnd.Location = new System.Drawing.Point(431, 500);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(208, 80);
            this.buttonEnd.TabIndex = 2;
            this.buttonEnd.Text = "Beérkeztetés";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(558, 450);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(111, 32);
            this.buttonUpload.TabIndex = 3;
            this.buttonUpload.Text = "Feltöltés";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(394, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vagy töltse fel itt a fájlt:";
            // 
            // UserControlUploadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.buttonEnd);
            this.Controls.Add(this.pictureBoxDragAndDrop);
            this.Controls.Add(this.pictureBoxSuccess);
            this.Name = "UserControlUploadData";
            this.Size = new System.Drawing.Size(1238, 600);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.UserControlUploadData_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.UserControlUploadData_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDragAndDrop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSuccess;
        private System.Windows.Forms.PictureBox pictureBoxDragAndDrop;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Label label1;
    }
}
