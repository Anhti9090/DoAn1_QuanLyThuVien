namespace DoAn1_QuanLyThuVien
{
    partial class TrangChu
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panelTiles = new System.Windows.Forms.FlowLayoutPanel();
            this.labelOverdue = new System.Windows.Forms.Label();
            this.dataGridViewOverdue = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOverdue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tổng quan";
            // 
            // panelTiles
            // 
            this.panelTiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTiles.Location = new System.Drawing.Point(12, 51);
            this.panelTiles.Name = "panelTiles";
            this.panelTiles.Size = new System.Drawing.Size(1118, 272);
            this.panelTiles.TabIndex = 3;
            // 
            // labelOverdue
            // 
            this.labelOverdue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOverdue.AutoSize = true;
            this.labelOverdue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOverdue.Location = new System.Drawing.Point(12, 326);
            this.labelOverdue.Name = "labelOverdue";
            this.labelOverdue.Size = new System.Drawing.Size(411, 25);
            this.labelOverdue.TabIndex = 4;
            this.labelOverdue.Text = "DANH SÁCH ĐỘC GIẢ MƯỢN QUÁ HẠN";
            this.labelOverdue.Click += new System.EventHandler(this.labelOverdue_Click);
            // 
            // dataGridViewOverdue
            // 
            this.dataGridViewOverdue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOverdue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOverdue.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewOverdue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOverdue.Location = new System.Drawing.Point(12, 354);
            this.dataGridViewOverdue.Name = "dataGridViewOverdue";
            this.dataGridViewOverdue.RowHeadersWidth = 51;
            this.dataGridViewOverdue.RowTemplate.Height = 24;
            this.dataGridViewOverdue.Size = new System.Drawing.Size(1118, 250);
            this.dataGridViewOverdue.TabIndex = 5;
            // 
            // TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1142, 616);
            this.Controls.Add(this.dataGridViewOverdue);
            this.Controls.Add(this.labelOverdue);
            this.Controls.Add(this.panelTiles);
            this.Controls.Add(this.label1);
            this.Name = "TrangChu";
            this.Text = "TrangChu";
            this.Load += new System.EventHandler(this.TrangChu_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOverdue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel panelTiles;
        private System.Windows.Forms.Label labelOverdue;
        private System.Windows.Forms.DataGridView dataGridViewOverdue;
    }
}