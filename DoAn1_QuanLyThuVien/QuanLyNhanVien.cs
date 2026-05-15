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
    public partial class QuanLyNhanVien : Form
    {
        NhanVien nhanVien = new NhanVien();
        NhanVienBLL nhanVienBLL = new NhanVienBLL();

        TaiKhoan taiKhoan = new TaiKhoan();
        TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void radioButtonNu_CheckedChanged(object sender, EventArgs e)
        {

        }
        public void ClearNhanVien()
        {
            textBoxMaNV.Clear();
            textBoxMaNV.ReadOnly = false;
            textBoxTenNV.Clear();
            dateTimePickerNgaySinh.Value = DateTime.Now;
            radioButtonNam.Checked = false;
            radioButtonNu.Checked = false;
            textBoxDiaChi.Clear();
            textBoxSDT.Clear();
            textBoxEmail.Clear();
        }
        public void ClearTaiKhoan()
        {
            textBoxTenDN.Clear();
            textBoxTenDN.ReadOnly = false;
            textBoxMatKhau1.Clear();
            comboBoxVT.SelectedIndex = -1;
            comboBoxNV.SelectedIndex = -1;
        }
        public void LoadNhanVien()
        {
            List<NhanVien> listNhanVien = nhanVienBLL.SelectNhanVien();
            dataGridView1.DataSource = listNhanVien;
            dataGridView1.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dataGridView1.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            dataGridView1.Columns["GioiTinh"].HeaderText = "Giới tính";
            dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dataGridView1.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dataGridView1.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            dataGridView1.Columns["Email"].HeaderText = "Email";

        }
        public void LoadTaiKhoan()
        {

            List<TaiKhoan> listTaiKhoan = taiKhoanBLL.SelectTaiKhoan();
            dataGridView2.DataSource = listTaiKhoan;
            dataGridView2.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
            dataGridView2.Columns["MatKhau"].HeaderText = "Mật khẩu";
            dataGridView2.Columns["Role"].HeaderText = "Vai trò";
            dataGridView2.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dataGridView2.Columns["TrangThai"].HeaderText = "Trạng thái";

            comboBoxNV.DataSource = nhanVienBLL.SelectNhanVien();
            comboBoxNV.DisplayMember = "TenNhanVien"; // user sees the name
            comboBoxNV.ValueMember = "MaNhanVien";  // code is used as value  

            // Hiển thị "Nhân viên"/"Admin", value là "ThuThu"/"Admin"
            var dsVaiTro = new List<dynamic>
            {
                new { Text = "Nhân viên", Value = "ThuThu" },
                new { Text = "Admin",     Value = "Admin"  }
            };
            comboBoxVT.DataSource = dsVaiTro;
            comboBoxVT.DisplayMember = "Text";
            comboBoxVT.ValueMember = "Value";
            comboBoxVT.SelectedIndex = 0;

            comboBoxTT.Items.Clear();
            comboBoxTT.Items.Add("Hoạt động");
            comboBoxTT.Items.Add("Khoá");
            comboBoxTT.SelectedIndex = 0;
        }
        private void buttonThemNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMaNV.Text) || string.IsNullOrWhiteSpace(textBoxTenNV.Text))
            {
                ThongBao.Show(this, "Vui lòng nhập mã và tên nhân viên!", ThongBaoType.Cancel);
                return;
            }
            if (!radioButtonNam.Checked && !radioButtonNu.Checked)
            {
                ThongBao.Show(this, "Vui lòng chọn giới tính!", ThongBaoType.Cancel);
                return;
            }

            try
            {
                nhanVien.MaNhanVien = textBoxMaNV.Text;
                nhanVien.TenNhanVien = textBoxTenNV.Text;
                nhanVien.NgaySinh = dateTimePickerNgaySinh.Value;
                nhanVien.GioiTinh = radioButtonNam.Checked ? "Nam" : "Nữ";
                nhanVien.DiaChi = textBoxDiaChi.Text;
                nhanVien.SoDienThoai = textBoxSDT.Text;
                nhanVien.Email = textBoxEmail.Text;
                nhanVienBLL.InsertNhanVien(nhanVien);

                ThongBao.Show(this, "Thêm nhân viên thành công!", ThongBaoType.Success);
                LoadNhanVien();
                ClearNhanVien();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi thêm nhân viên: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonCapNhatNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMaNV.Text))
            {
                ThongBao.Show(this, "Vui lòng chọn nhân viên cần cập nhật!", ThongBaoType.Cancel);
                return;
            }

            try
            {
                nhanVien.MaNhanVien = textBoxMaNV.Text;
                nhanVien.TenNhanVien = textBoxTenNV.Text;
                nhanVien.NgaySinh = dateTimePickerNgaySinh.Value;
                nhanVien.GioiTinh = radioButtonNam.Checked ? "Nam" : (radioButtonNu.Checked ? "Nữ" : "");
                nhanVien.DiaChi = textBoxDiaChi.Text;
                nhanVien.SoDienThoai = textBoxSDT.Text;
                nhanVien.Email = textBoxEmail.Text;
                nhanVienBLL.UpdateNhanVien(nhanVien);

                ThongBao.Show(this, "Cập nhật nhân viên thành công!", ThongBaoType.Success);
                LoadNhanVien();
                ClearNhanVien();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi cập nhật nhân viên: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonXoaNV_Click(object sender, EventArgs e)
        {
            string maNhanVien = textBoxMaNV.Text;
            if (string.IsNullOrWhiteSpace(maNhanVien))
            {
                ThongBao.Show(this, "Vui lòng chọn nhân viên cần xoá!", ThongBaoType.Cancel);
                return;
            }

            // Xác nhận trước khi xoá (giữ MessageBox vì cần Yes/No)
            if (MessageBox.Show("Xoá nhân viên " + maNhanVien + "?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                nhanVienBLL.DeleteNhanVien(maNhanVien);
                ThongBao.Show(this, "Xoá nhân viên thành công!", ThongBaoType.Success);
                LoadNhanVien();
                ClearNhanVien();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi xoá nhân viên: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonLamMoiNV_Click(object sender, EventArgs e)
        {
            LoadNhanVien();
            ClearNhanVien();
        }

        private void buttonHuyNV_Click(object sender, EventArgs e)
        {
            ClearNhanVien();
        }

        private void buttonTimKiemNV_Click(object sender, EventArgs e)
        {
            try
            {
                string text = textBoxTimKiemNV.Text;
                List<NhanVien> listNhanVien = nhanVienBLL.SelectNhanVien();
                var result = listNhanVien.Where(nv => nv.MaNhanVien.Contains(text) || nv.TenNhanVien.Contains(text)).ToList();
                dataGridView1.DataSource = result;
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi tìm kiếm nhân viên: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadTaiKhoan();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBoxMaNV.Text = row.Cells["MaNhanVien"].Value.ToString();
                textBoxMaNV.ReadOnly = true; // Đặt TextBox mã nhân viên thành chỉ đọc
                textBoxTenNV.Text = row.Cells["TenNhanVien"].Value.ToString();
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
                textBoxSDT.Text = Convert.ToString(row.Cells["SoDienThoai"].Value);
                textBoxEmail.Text = row.Cells["Email"].Value.ToString();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ClearNhanVien();
        }

        private void buttonThemTK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTenDN.Text) || string.IsNullOrWhiteSpace(textBoxMatKhau1.Text))
            {
                ThongBao.Show(this, "Vui lòng nhập tên đăng nhập và mật khẩu!", ThongBaoType.Cancel);
                return;
            }
            if (comboBoxVT.SelectedValue == null || comboBoxNV.SelectedValue == null)
            {
                ThongBao.Show(this, "Vui lòng chọn vai trò và nhân viên!", ThongBaoType.Cancel);
                return;
            }

            try
            {
                taiKhoan.TenDangNhap = textBoxTenDN.Text;
                taiKhoan.MatKhau = textBoxMatKhau1.Text;
                taiKhoan.Role = comboBoxVT.SelectedValue?.ToString();
                taiKhoan.MaNhanVien = comboBoxNV.SelectedValue?.ToString();
                taiKhoan.TrangThai = "Hoạt động";
                taiKhoanBLL.InsertTaiKhoan(taiKhoan);

                ThongBao.Show(this, "Thêm tài khoản thành công!", ThongBaoType.Success);
                LoadTaiKhoan();
                ClearTaiKhoan();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi thêm tài khoản: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            //LoadTaiKhoan();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            //LoadNhanVien();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                textBoxTenDN.Text = row.Cells["TenDangNhap"].Value.ToString();
                textBoxTenDN.ReadOnly = true; // Đặt TextBox tên đăng nhập thành chỉ đọc
                textBoxMatKhau1.Text = row.Cells["MatKhau"].Value.ToString();
                comboBoxVT.SelectedValue = row.Cells["Role"].Value.ToString();
                comboBoxNV.SelectedValue = row.Cells["MaNhanVien"].Value.ToString();
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            ClearTaiKhoan();
        }

        private void buttonCapNhatTK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTenDN.Text))
            {
                ThongBao.Show(this, "Vui lòng chọn tài khoản cần cập nhật!", ThongBaoType.Cancel);
                return;
            }

            try
            {
                taiKhoan.TenDangNhap = textBoxTenDN.Text;
                taiKhoan.MatKhau = textBoxMatKhau1.Text;
                taiKhoan.Role = comboBoxVT.SelectedValue?.ToString();
                taiKhoan.MaNhanVien = comboBoxNV.SelectedValue?.ToString();
                taiKhoan.TrangThai = "Hoạt động";
                taiKhoanBLL.UpdateTaiKhoan(taiKhoan);

                ThongBao.Show(this, "Cập nhật tài khoản thành công!", ThongBaoType.Success);
                LoadTaiKhoan();
                ClearTaiKhoan();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi cập nhật tài khoản: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonXoaTK_Click(object sender, EventArgs e)
        {
            string TenDangNhap = textBoxTenDN.Text;
            if (string.IsNullOrWhiteSpace(TenDangNhap))
            {
                ThongBao.Show(this, "Vui lòng chọn tài khoản cần xoá!", ThongBaoType.Cancel);
                return;
            }

            if (MessageBox.Show("Xoá tài khoản " + TenDangNhap + "?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                taiKhoanBLL.DeleteTaiKhoan(TenDangNhap);
                ThongBao.Show(this, "Xoá tài khoản thành công!", ThongBaoType.Success);
                LoadTaiKhoan();
                ClearTaiKhoan();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi xoá tài khoản: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonLamMoiTK_Click(object sender, EventArgs e)
        {
            LoadTaiKhoan();
            ClearTaiKhoan();
        }

        private void buttonHuyTK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTenDN.Text))
            {
                ThongBao.Show(this, "Vui lòng chọn tài khoản cần khoá!", ThongBaoType.Cancel);
                return;
            }

            try
            {
                taiKhoan.TenDangNhap = textBoxTenDN.Text;
                taiKhoan.MatKhau = textBoxMatKhau1.Text;
                taiKhoan.Role = comboBoxVT.SelectedValue?.ToString();
                taiKhoan.MaNhanVien = comboBoxNV.SelectedValue?.ToString();
                taiKhoan.TrangThai = "Khoá";
                taiKhoanBLL.UpdateTaiKhoan(taiKhoan);

                ThongBao.Show(this, "Đã khoá tài khoản!", ThongBaoType.Success);
                LoadTaiKhoan();
                ClearTaiKhoan();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi khoá tài khoản: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonTimKiemTK_Click(object sender, EventArgs e)
        {
            try
            {
                string text = textBoxTimKiemTK.Text;
                List<TaiKhoan> listTaiKhoan = taiKhoanBLL.SelectTaiKhoan();
                var result = listTaiKhoan.Where(tk => tk.TenDangNhap.Contains(text) || tk.MaNhanVien.Contains(text)).ToList();
                dataGridView2.DataSource = result;
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi tìm kiếm tài khoản: " + ex.Message, ThongBaoType.Error);
            }
        }
    }
}
