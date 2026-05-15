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
    public partial class Menu : Form
    {
        private string currentRole;

        public Menu(string role)
        {
            InitializeComponent();
            currentRole = role;
        }

        private void LoadForm(Form form)
        {
            panelForm.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
            form.Show();
        }

        private void buttonTrangChu_Click(object sender, EventArgs e)
        {
            LoadForm(new TrangChu());
        }

        private void buttonTheLoai_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyTheLoaiSach());
        }

        private void buttonSach_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLySach());
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            buttonTrangChu_Click(sender, e);

            // Ẩn nút Thống Kê nếu không phải Admin
            if (currentRole != "Admin")
            {
                buttonThongKe.Visible = false;
                buttonTaiKhoan.Visible = false;
            }
        }

        private void buttonThongKe_Click(object sender, EventArgs e)
        {
            LoadForm(new ThongKe());
        }

        private void buttonDocGia_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyDocGia());
        }

        private void buttonPhieuMuonSach_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyPhieuMuon());
        }

        private void buttonThongKe_Click_1(object sender, EventArgs e)
        {
            LoadForm(new ThongKe());
        }
        private void buttonTaiKhoan_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyNhanVien());
        }

        private void buttonTacGia_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyTacGia());
        }

        private void buttonTraSach_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyTraSach());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonDanhMuc_Click(object sender, EventArgs e)
        {
            LoadForm(new DanhMuc());
        }
    }
}
