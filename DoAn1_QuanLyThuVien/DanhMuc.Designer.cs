namespace DoAn1_QuanLyThuVien
{
    partial class DanhMuc
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTheLoai = new System.Windows.Forms.TabPage();
            this.tabPageTacGia = new System.Windows.Forms.TabPage();
            this.tabPageNhaXuatBan = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTheLoai);
            this.tabControl1.Controls.Add(this.tabPageTacGia);
            this.tabControl1.Controls.Add(this.tabPageNhaXuatBan);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(993, 540);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageTheLoai
            // 
            this.tabPageTheLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageTheLoai.Location = new System.Drawing.Point(4, 29);
            this.tabPageTheLoai.Name = "tabPageTheLoai";
            this.tabPageTheLoai.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTheLoai.Size = new System.Drawing.Size(985, 507);
            this.tabPageTheLoai.TabIndex = 0;
            this.tabPageTheLoai.Text = "Thể Loại";
            this.tabPageTheLoai.UseVisualStyleBackColor = true;
            // 
            // tabPageTacGia
            // 
            this.tabPageTacGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageTacGia.Location = new System.Drawing.Point(4, 29);
            this.tabPageTacGia.Name = "tabPageTacGia";
            this.tabPageTacGia.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTacGia.Size = new System.Drawing.Size(985, 507);
            this.tabPageTacGia.TabIndex = 1;
            this.tabPageTacGia.Text = "Tác Giả";
            this.tabPageTacGia.UseVisualStyleBackColor = true;
            // 
            // tabPageNhaXuatBan
            // 
            this.tabPageNhaXuatBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageNhaXuatBan.Location = new System.Drawing.Point(4, 29);
            this.tabPageNhaXuatBan.Name = "tabPageNhaXuatBan";
            this.tabPageNhaXuatBan.Size = new System.Drawing.Size(985, 507);
            this.tabPageNhaXuatBan.TabIndex = 2;
            this.tabPageNhaXuatBan.Text = "Nhà Xuất Bản";
            this.tabPageNhaXuatBan.UseVisualStyleBackColor = true;
            // 
            // DanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 540);
            this.Controls.Add(this.tabControl1);
            this.Name = "DanhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Danh Mục";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTheLoai;
        private System.Windows.Forms.TabPage tabPageTacGia;
        private System.Windows.Forms.TabPage tabPageNhaXuatBan;
    }
}