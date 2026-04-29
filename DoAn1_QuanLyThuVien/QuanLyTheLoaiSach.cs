using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;


namespace DoAn1_QuanLyThuVien
{
    public partial class QuanLyTheLoaiSach : Form
    {
        TheLoai theLoai = new TheLoai();
        TheLoaiBLL theLoaiBLL = new TheLoaiBLL();

        public QuanLyTheLoaiSach()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            dataGridView1.DataSource = theLoaiBLL.GetTheLoai();
            dataGridView1.Columns["MaTheLoai"].HeaderText = "Mã Thể Loại";
            dataGridView1.Columns["TenTheLoai"].HeaderText = "Tên Thể Loại";
            dataGridView1.Columns["MoTa"].HeaderText = "Mô Tả";
        }
        public void ClearData()
        {
            textMaTheLoai.Clear();
            textTenTheLoai.Clear();
            textBoxMoTa.Clear();
            textMaTheLoai.ReadOnly = false;
        }
        private void buttonThem_Click(object sender, EventArgs e)
        {
            theLoai.MaTheLoai = textMaTheLoai.Text;
            theLoai.TenTheLoai = textTenTheLoai.Text;
            theLoai.MoTa = textBoxMoTa.Text;

            theLoaiBLL.AddTheLoai(theLoai);
            ThongBao.Show(this, "Thêm thể loại thành công", ThongBaoType.Success);
            LoadData();
            ClearData();
        }

        private void TheLoaiSach_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = textBoxTimKiem.Text;
                List<TheLoai> filteredList = theLoaiBLL.SearchTheLoai(searchText, theLoaiBLL.GetTheLoai());
                dataGridView1.DataSource = filteredList;
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi tìm kiếm thể loại: " + ex.Message, ThongBaoType.Error);
            }

        }
        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maTheLoai = textMaTheLoai.Text;
                theLoaiBLL.DeleteTheLoai(maTheLoai);
                ThongBao.Show(this, "Xóa thể loại thành công", ThongBaoType.Success);
                LoadData();
                ClearData();

            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi xóa thể loại: " + ex.Message, ThongBaoType.Error);
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textMaTheLoai.Text = row.Cells["MaTheLoai"].Value?.ToString();
                textTenTheLoai.Text = row.Cells["TenTheLoai"].Value?.ToString();
                textBoxMoTa.Text = row.Cells["MoTa"].Value?.ToString();
                textMaTheLoai.ReadOnly = true;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearData();
        }
    }
}
