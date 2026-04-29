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
    public partial class DangNhap : Form
    {
        TaiKhoan tk = new TaiKhoan();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        public DangNhap()
        {
            InitializeComponent();
        }

        private void buttonDangNhap_Click(object sender, EventArgs e)
        {
            if (comboBoxRole.SelectedItem == null)
            {
                ThongBao.Show(this, "Vui lòng chọn quyền đăng nhập", ThongBaoType.Cancel);
                return;
            }

            tk.Username = textTenDangNhap.Text;
            tk.Password = textMatKhau.Text;

            string result = TKBLL.CheckLogin(tk);
            switch (result)
            {
                case "required_username":
                    ThongBao.Show(this, "Vui lòng nhập tên đăng nhập", ThongBaoType.Cancel);
                    return;
                case "required_password":
                    ThongBao.Show(this, "Vui lòng nhập mật khẩu", ThongBaoType.Cancel);
                    return;
                case "invalid_account":
                    ThongBao.Show(this, "Tên đăng nhập hoặc mật khẩu không đúng", ThongBaoType.Error);
                    return;
            }

            string selectedRole = comboBoxRole.SelectedItem.ToString();
            if (result != selectedRole)
            {
                ThongBao.Show(this, "Bạn không có quyền đăng nhập với vai trò này", ThongBaoType.Error);
                return;
            }

            ThongBao.Show(this, "Đăng nhập thành công", ThongBaoType.Success);
            Menu menu = new Menu(result);
            menu.Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            comboBoxRole.Items.Add("Admin");
            comboBoxRole.Items.Add("ThuThu");
            comboBoxRole.SelectedIndex = 1;
        }
    }
}
