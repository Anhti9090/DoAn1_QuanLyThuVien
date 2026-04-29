using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace DoAn1_QuanLyThuVien
{
    public partial class QuanLySach : Form
    {
        Sach sach = new Sach();
        SachBLL sachBLL = new SachBLL();
        TacGiaBLL tacGiaBLL = new TacGiaBLL();
        TheLoaiBLL theLoaiBLL = new TheLoaiBLL();
        public QuanLySach()
        {
            InitializeComponent();

            // Cho phép nhập số lớn hơn mặc định (100)
            numericUpDownTongSL.Maximum = int.MaxValue;
            numericUpDownSLCon.Maximum = int.MaxValue;

            // Khi đổi Tổng số lượng -> Số lượng còn hiện lên ngay
            numericUpDownTongSL.ValueChanged += numericUpDownTongSL_ValueChanged;
        }

        private void numericUpDownTongSL_ValueChanged(object sender, EventArgs e)
        {
            // Đồng bộ: Số lượng còn = Tổng số lượng khi đang nhập mới
            numericUpDownSLCon.Value = numericUpDownTongSL.Value;
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            sach.MaSach = textBoxMaSach.Text;
            if (string.IsNullOrWhiteSpace(sach.MaSach))
            {
                ThongBao.Show(this, "Mã sách không được để trống", ThongBaoType.Error);
                return;
            }
            sach.TenSach = textBoxTenSach.Text;
            sach.MaTacGia = comboBoxMaTG.SelectedValue?.ToString();
            sach.MaTheLoai = comboBoxMaTheLoai.SelectedValue?.ToString();
            sach.NamXuatBan = textBoxNamXB.Text;
            sach.NhaXuatBan = textBoxNhaXB.Text;
            sach.TongSoLuong = (int)numericUpDownTongSL.Value;
            sach.SoLuongCon = (int)numericUpDownSLCon.Value;
            sachBLL.InsertSach(sach);
            ThongBao.Show(this, "Thêm sách thành công", ThongBaoType.Success);
            LoadData();
            ClearData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonCapNhat_Click(object sender, EventArgs e)
        {
            sach.MaSach = textBoxMaSach.Text;
            sach.TenSach = textBoxTenSach.Text;
            sach.MaTacGia = comboBoxMaTG.SelectedValue.ToString();
            sach.MaTheLoai = comboBoxMaTheLoai.SelectedValue.ToString();
            sach.NamXuatBan = textBoxNamXB.Text;
            sach.NhaXuatBan = textBoxNhaXB.Text;
            sach.TongSoLuong = (int)numericUpDownTongSL.Value;
            sach.SoLuongCon = (int)numericUpDownSLCon.Value;
            sachBLL.UpdateSach(sach);
            LoadData();
            ClearData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sach.MaSach = textBoxMaSach.Text;
            sachBLL.DeleteSach(sach.MaSach);
            LoadData();
            ClearData();
        }
        public void LoadData()
        {
            comboBoxMaTG.DataSource = tacGiaBLL.SelectTacGia();
            comboBoxMaTG.DisplayMember = "TenTacGia";
            comboBoxMaTG.ValueMember = "MaTacGia";

            comboBoxMaTheLoai.DataSource = theLoaiBLL.GetTheLoai();
            comboBoxMaTheLoai.DisplayMember = "TenTheLoai";
            comboBoxMaTheLoai.ValueMember = "MaTheLoai";

            dataGridView1.DataSource = sachBLL.SelectSach();
            dataGridView1.Columns["MaSach"].HeaderText = "Mã Sách";
            dataGridView1.Columns["TenSach"].HeaderText = "Tên Sách";
            
            // Ẩn cột mã, hiện tên
            dataGridView1.Columns["MaTacGia"].Visible = false;
            dataGridView1.Columns["MaTheLoai"].Visible = false;
            dataGridView1.Columns["TenTacGia"].HeaderText = "Tác Giả";
            dataGridView1.Columns["TenTheLoai"].HeaderText = "Thể Loại";
            
            dataGridView1.Columns["NamXuatBan"].HeaderText = "Năm Xuất Bản";
            dataGridView1.Columns["NhaXuatBan"].HeaderText = "Nhà Xuất Bản";
            dataGridView1.Columns["TongSoLuong"].HeaderText = "Tổng Số Lượng";
            dataGridView1.Columns["SoLuongCon"].HeaderText = "Số Lượng Còn";

            // Sắp xếp thứ tự cột (tuỳ chọn)
            dataGridView1.Columns["MaSach"].DisplayIndex = 0;
            dataGridView1.Columns["TenSach"].DisplayIndex = 1;
            dataGridView1.Columns["TenTacGia"].DisplayIndex = 2;
            dataGridView1.Columns["TenTheLoai"].DisplayIndex = 3;
            dataGridView1.Columns["NamXuatBan"].DisplayIndex = 4;
            dataGridView1.Columns["NhaXuatBan"].DisplayIndex = 5;
            dataGridView1.Columns["TongSoLuong"].DisplayIndex = 6;
            dataGridView1.Columns["SoLuongCon"].DisplayIndex = 7;
        }
        public void ClearData()
        {
            textBoxMaSach.Clear();
            textBoxTenSach.Clear();
            comboBoxMaTG.SelectedIndex = -1;
            comboBoxMaTheLoai.SelectedIndex = -1;
            textBoxNamXB.Clear();
            textBoxNhaXB.Clear();
            numericUpDownTongSL.Value = 0;
            numericUpDownSLCon.Value = 0;
            textBoxMaSach.ReadOnly = false;
        }
        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearData();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            string searchText = textBoxTimKiem.Text;
            List<Sach> filteredList = sachBLL.SearchSach(searchText, sachBLL.SelectSach());
            dataGridView1.DataSource = filteredList;

        }

        private void QuanLySach_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBoxMaSach.Text = row.Cells["MaSach"].Value.ToString();
                textBoxTenSach.Text = row.Cells["TenSach"].Value.ToString();

                // Lấy MÃ từ cột ẩn để ComboBox có thể lưu đúng khi Update/Delete
                comboBoxMaTG.SelectedValue = row.Cells["MaTacGia"].Value.ToString();
                comboBoxMaTheLoai.SelectedValue = row.Cells["MaTheLoai"].Value.ToString();

                textBoxNamXB.Text = row.Cells["NamXuatBan"].Value.ToString();
                textBoxNhaXB.Text = row.Cells["NhaXuatBan"].Value.ToString();
                numericUpDownTongSL.Value = Convert.ToInt32(row.Cells["TongSoLuong"].Value);
                numericUpDownSLCon.Value = Convert.ToInt32(row.Cells["SoLuongCon"].Value);
                textBoxMaSach.ReadOnly = true; // Mã sách không được sửa
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
