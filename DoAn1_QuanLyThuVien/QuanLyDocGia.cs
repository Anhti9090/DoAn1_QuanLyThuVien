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
    public partial class QuanLyDocGia : Form
    {
        DocGia docGia = new DocGia();
        DocGiaBLL docGiaBLL = new DocGiaBLL();
        public QuanLyDocGia()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e) { }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            docGia.MaDocGia = textBoxMaDG.Text;
            docGia.TenDocGia = textBoxTenDG.Text;
            docGia.NgaySinh = dateTimePickerNgaySinh.Value;
            if (radioButtonNam.Checked) docGia.GioiTinh = "Nam";
            else if (radioButtonNu.Checked) docGia.GioiTinh = "Nữ";
            docGia.DiaChi = textBoxDiaChi.Text;
            docGia.SoDienThoai = textBoxSDT.Text;
            docGia.Email = textBoxEmail.Text;
            docGia.Lop = textBoxLop.Text;
            docGia.NgayLapThe = dateTimePickerNgayDangKy.Value;
            docGia.TrangThai = "Hoạt động";
            try
            {
                docGiaBLL.InsertDocGia(docGia);
                ThongBao.Show(this, "Thêm độc giả thành công", ThongBaoType.Success);
                LoadData();
                ClearData();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, ex.Message, ThongBaoType.Error);
            }
        }

        private void comboBoxTheDG_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý khi chọn một thẻ độc giả từ combobox
        }

        private void UpdateTheDG(object sender, EventArgs e)
        {
            string maDocGia = textBoxMaDG.Text.Trim();
            string tenDocGia = textBoxTenDG.Text.Trim();

            if (!string.IsNullOrEmpty(maDocGia) && !string.IsNullOrEmpty(tenDocGia))
            {
                comboBoxTheDG.Items.Clear();

                string maThe = $"{maDocGia}";
                string thongTinThe = $"{maThe} - {tenDocGia}";

                comboBoxTheDG.Items.Add(thongTinThe);

                comboBoxTheDG.SelectedIndex = 0;
            }
            else
            {
                comboBoxTheDG.Items.Clear();
                comboBoxTheDG.Text = "";
            }
        }
        public void LoadData()
        {
            List<DocGia> listDocGia = docGiaBLL.SelectDocGia();
            dataGridView1.DataSource = listDocGia;
        }
        public void ClearData()
        {
            textBoxMaDG.Text = "";
            textBoxMaDG.Enabled = true; // Kích hoạt lại TextBox mã độc giả
            textBoxTenDG.Text = "";
            dateTimePickerNgaySinh.Value = DateTime.Now;
            radioButtonNam.Checked = false;
            radioButtonNu.Checked = false;
            textBoxDiaChi.Text = "";
            textBoxSDT.Text = "";
            textBoxEmail.Text = "";
            textBoxLop.Text = "";
            dateTimePickerNgayDangKy.Value = DateTime.Now;
            comboBoxTheDG.Items.Clear();
        }

        private void QuanLyDocGia_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void QuanLyDocGia_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBoxMaDG.Text = row.Cells["MaDocGia"].Value.ToString();
                textBoxMaDG.Enabled = false; // Vô hiệu hóa TextBox mã độc giả
                textBoxTenDG.Text = row.Cells["TenDocGia"].Value.ToString();
                dateTimePickerNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                {
                    radioButtonNam.Checked = true;
                    radioButtonNu.Checked = false;
                }
                else if (gioiTinh == "Nữ")
                {
                    radioButtonNam.Checked = false;
                    radioButtonNu.Checked = true;
                }
                textBoxDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                textBoxSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                textBoxEmail.Text = row.Cells["Email"].Value.ToString();
                textBoxLop.Text = row.Cells["Lop"].Value.ToString();
                dateTimePickerNgayDangKy.Value = Convert.ToDateTime(row.Cells["NgayLapThe"].Value);
            }
        }

        private void buttonCapNhat_Click(object sender, EventArgs e)
        {
            docGia.MaDocGia = textBoxMaDG.Text;
            docGia.TenDocGia = textBoxTenDG.Text;
            docGia.NgaySinh = dateTimePickerNgaySinh.Value;
            if (radioButtonNam.Checked)
            {
                docGia.GioiTinh = "Nam";
            }
            else if (radioButtonNu.Checked)
            {
                docGia.GioiTinh = "Nữ";
            }
            docGia.DiaChi = textBoxDiaChi.Text;
            docGia.SoDienThoai = textBoxSDT.Text;
            docGia.Email = textBoxEmail.Text;
            docGia.Lop = textBoxLop.Text;
            docGia.NgayLapThe = dateTimePickerNgayDangKy.Value;
            docGia.TrangThai = "Hoạt động";
            docGiaBLL.UpdateDocGia(docGia);
            LoadData();
            ClearData();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            string maDocGia = textBoxMaDG.Text;
            docGiaBLL.DeleteDocGia(maDocGia);
            LoadData();
        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonHuy_Click(object sender, EventArgs e)
        {
            docGia.MaDocGia = textBoxMaDG.Text;
            docGia.TenDocGia = textBoxTenDG.Text;
            docGia.NgaySinh = dateTimePickerNgaySinh.Value;
            if (radioButtonNam.Checked)
            {
                docGia.GioiTinh = "Nam";
            }
            else if (radioButtonNu.Checked)
            {
                docGia.GioiTinh = "Nữ";
            }
            docGia.DiaChi = textBoxDiaChi.Text;
            docGia.SoDienThoai = textBoxSDT.Text;
            docGia.Email = textBoxEmail.Text;
            docGia.Lop = textBoxLop.Text;
            docGia.NgayLapThe = dateTimePickerNgayDangKy.Value;
            docGia.TrangThai = "Không hoạt động";
            docGiaBLL.UpdateDocGia(docGia);
            LoadData();
            ClearData();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            string text = textBoxTimKiem.Text.Trim();
            List<DocGia> listDocGia = docGiaBLL.SelectDocGia();
            var filteredList = listDocGia.Where(dg => dg.MaDocGia.Contains(text) || dg.TenDocGia.Contains(text)).ToList();
            dataGridView1.DataSource = filteredList;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
