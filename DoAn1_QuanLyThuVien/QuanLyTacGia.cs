using BLL;
using DTO;
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
    public partial class QuanLyTacGia : Form
    {
        TacGia tacGia = new TacGia();
        TacGiaBLL tacGiaBLL = new TacGiaBLL();
        public QuanLyTacGia()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            dataGridView1.DataSource = tacGiaBLL.SelectTacGia();
            dataGridView1.Columns["MaTacGia"].HeaderText = "Mã Tác Giả";
            dataGridView1.Columns["TenTacGia"].HeaderText = "Tên Tác Giả";
            dataGridView1.Columns["MoTa"].HeaderText = "Mô Tả";
        }
        public void ClearData()
        {
            textBoxTacGia.Clear();
            textBoxTenTG.Clear();
            textBoxMoTa.Clear();
            textBoxTacGia.ReadOnly = false;
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            tacGia.MaTacGia = textBoxTacGia.Text;
            tacGia.TenTacGia = textBoxTenTG.Text;
            tacGia.MoTa = textBoxMoTa.Text;

            tacGiaBLL.AddTacGia(tacGia);
            ThongBao.Show(this, "Thêm tác giả thành công", ThongBaoType.Success);
            LoadData();
            ClearData();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maTacGia = textBoxTacGia.Text;
                tacGiaBLL.DeleteTacGia(maTacGia);
                ThongBao.Show(this, "Xóa tác giả thành công", ThongBaoType.Success);
                LoadData();
                ClearData();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi xóa tác giả: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void QuanLyTacGia_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBoxTacGia.Text = row.Cells["MaTacGia"].Value.ToString();
                textBoxTenTG.Text = row.Cells["TenTacGia"].Value.ToString();
                textBoxMoTa.Text = row.Cells["MoTa"].Value.ToString();
                textBoxTacGia.ReadOnly = true;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            string searchText = textBoxTimKiem.Text;
            List<TacGia> filteredList = tacGiaBLL.SearchTacGia(searchText, tacGiaBLL.SelectTacGia());
            dataGridView1.DataSource = filteredList;
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {

        }
    }
}
