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
using BLL;

namespace DoAn1_QuanLyThuVien
{
    public partial class QuanLyNhaXuatBan : Form
    {
        NhaXuatBan nhaXuatBan = new NhaXuatBan();
        NhaXuatBanBLL nhaXBBLL = new NhaXuatBanBLL();
        public QuanLyNhaXuatBan()
        {
            InitializeComponent();
        }

        private void QuanLyNhaXuatBan_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBoxTrangThai();
        }

        private void LoadComboBoxTrangThai()
        {
            comboBoxTrangThai.Items.Clear();
            comboBoxTrangThai.Items.Add("Hoạt động");
            comboBoxTrangThai.Items.Add("Không hoạt động");
            comboBoxTrangThai.SelectedIndex = 0;
        }

        public void LoadData()
        {
            dataGridView1.DataSource = nhaXBBLL.SelectNhaXuatBan();
            dataGridView1.Columns["MaNhaXuatBan"].HeaderText = "Mã NXB";
            dataGridView1.Columns["TenNhaXuatBan"].HeaderText = "Tên Nhà Xuất Bản";
            dataGridView1.Columns["Email"].HeaderText = "Email";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private void ClearData()
        {
            textBoxNhaXB.Clear();
            textBoxTenNhaXB.Clear();
            textBoxEmail.Clear();
            comboBoxTrangThai.SelectedIndex = 0;
            textBoxNhaXB.ReadOnly = false;
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxNhaXB.Text))
                {
                    ThongBao.Show(this, "Mã nhà xuất bản không được để trống", ThongBaoType.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxTenNhaXB.Text))
                {
                    ThongBao.Show(this, "Tên nhà xuất bản không được để trống", ThongBaoType.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
                {
                    ThongBao.Show(this, "Email không được để trống", ThongBaoType.Error);
                    return;
                }

                nhaXuatBan.MaNhaXuatBan = textBoxNhaXB.Text;
                nhaXuatBan.TenNhaXuatBan = textBoxTenNhaXB.Text;
                nhaXuatBan.Email = textBoxEmail.Text;
                nhaXuatBan.TrangThai = comboBoxTrangThai.SelectedItem.ToString();

                nhaXBBLL.InsertNhaXuatBan(nhaXuatBan);
                ThongBao.Show(this, "Thêm nhà xuất bản thành công", ThongBaoType.Success);
                LoadData();
                ClearData();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxNhaXB.Text))
                {
                    ThongBao.Show(this, "Vui lòng chọn nhà xuất bản cần cập nhật", ThongBaoType.Info);
                    return;
                }

                nhaXuatBan.MaNhaXuatBan = textBoxNhaXB.Text;
                nhaXuatBan.TenNhaXuatBan = textBoxTenNhaXB.Text;
                nhaXuatBan.Email = textBoxEmail.Text;
                nhaXuatBan.TrangThai = comboBoxTrangThai.SelectedItem.ToString();

                nhaXBBLL.UpdateNhaXuatBan(nhaXuatBan);
                ThongBao.Show(this, "Cập nhật nhà xuất bản thành công", ThongBaoType.Success);
                LoadData();
                ClearData();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxNhaXB.Text))
                {
                    ThongBao.Show(this, "Vui lòng chọn nhà xuất bản cần xóa", ThongBaoType.Info);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa nhà xuất bản này?\nLưu ý: Không thể xóa nếu có sách liên quan.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    nhaXBBLL.DeleteNhaXuatBan(textBoxNhaXB.Text);
                    ThongBao.Show(this, "Xóa nhà xuất bản thành công", ThongBaoType.Success);
                    LoadData();
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message + "\nKhông thể xóa nhà xuất bản có sách liên quan.", ThongBaoType.Error);
            }
        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearData();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = textBoxTimKiem.Text.Trim();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadData();
                }
                else
                {
                    List<NhaXuatBan> filteredList = nhaXBBLL.SearchNhaXuatBan(keyword);
                    dataGridView1.DataSource = filteredList;
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBoxNhaXB.Text = row.Cells["MaNhaXuatBan"].Value.ToString();
                textBoxTenNhaXB.Text = row.Cells["TenNhaXuatBan"].Value.ToString();
                textBoxEmail.Text = row.Cells["Email"].Value.ToString();
                comboBoxTrangThai.SelectedItem = row.Cells["TrangThai"].Value.ToString();
                textBoxNhaXB.ReadOnly = true;
            }
        }
    }
}
