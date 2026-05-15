using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn1_QuanLyThuVien
{
    public partial class DanhMuc : Form
    {
        private QuanLyTheLoaiSach formTheLoai;
        private QuanLyTacGia formTacGia;
        private QuanLyNhaXuatBan formNhaXuatBan;

        public DanhMuc()
        {
            InitializeComponent();
            LoadFormIntoTabs();

            // Thêm event khi chuyển tab để reload dữ liệu
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
        }

        private void LoadFormIntoTabs()
        {
            // Load QuanLyTheLoaiSach vào tabPageTheLoai
            formTheLoai = new QuanLyTheLoaiSach
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            tabPageTheLoai.Controls.Add(formTheLoai);
            formTheLoai.Show();

            // Load QuanLyTacGia vào tabPageTacGia
            formTacGia = new QuanLyTacGia
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            tabPageTacGia.Controls.Add(formTacGia);
            formTacGia.Show();

            // Load QuanLyNhaXuatBan vào tabPageNhaXuatBan
            formNhaXuatBan = new QuanLyNhaXuatBan
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            tabPageNhaXuatBan.Controls.Add(formNhaXuatBan);
            formNhaXuatBan.Show();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reload dữ liệu khi chuyển tab
            switch (tabControl1.SelectedIndex)
            {
                case 0: // Tab Thể Loại
                    if (formTheLoai != null)
                        formTheLoai.LoadData();
                    break;
                case 1: // Tab Tác Giả
                    if (formTacGia != null)
                        formTacGia.LoadData();
                    break;
                case 2: // Tab Nhà Xuất Bản
                    if (formNhaXuatBan != null)
                        formNhaXuatBan.LoadData();
                    break;
            }
        }
    }
}
