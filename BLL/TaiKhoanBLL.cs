using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class TaiKhoanBLL
    {
        TaiKhoanAccess taiKhoanAccess = new TaiKhoanAccess();
        public string CheckLogic(TaiKhoan taikhoan)
        {
            //Kiểm tra nghiệp vụ
            if (string.IsNullOrEmpty(taikhoan.TenDangNhap))
                return "required_username";
            if (string.IsNullOrEmpty(taikhoan.MatKhau))
                return "required_password";
            string info = taiKhoanAccess.CheckLogic(taikhoan);
            return info;
        }

        public string CheckLogin(TaiKhoan taikhoan)
        {
            if (string.IsNullOrEmpty(taikhoan.TenDangNhap))
                return "required_username";
            if (string.IsNullOrEmpty(taikhoan.MatKhau))
                return "required_password";
            string role = TaiKhoanAccess.CheckLogin(taikhoan);
            if (role == null)
                return "invalid_account";
            return role;
        }
        public List<TaiKhoan> SelectTaiKhoan()
        {
            return TaiKhoanAccess.SelectTaiKhoan();
        }
        public void InsertTaiKhoan(TaiKhoan taikhoan)
        {
            TaiKhoanAccess.InsertTaiKhoan(taikhoan);
        }
        public void UpdateTaiKhoan(TaiKhoan taikhoan)
        {
            TaiKhoanAccess.UpdateTaiKhoan(taikhoan);
        }
        public void DeleteTaiKhoan(string username)
        {
            TaiKhoanAccess.DeleteTaiKhoan(username);
        }
        public List<TaiKhoan> SearchTaiKhoan(string keyword, List<TaiKhoan> listTaiKhoan)
        {
            if (listTaiKhoan == null)
                return new List<TaiKhoan>();

            if (string.IsNullOrWhiteSpace(keyword))
                return listTaiKhoan.ToList();

            string key = keyword.Trim().ToLowerInvariant();

            return listTaiKhoan
                .Where(tk => tk != null &&
                            ((!string.IsNullOrEmpty(tk.TenDangNhap) && tk.TenDangNhap.ToLowerInvariant().Contains(key)) ||
                             (!string.IsNullOrEmpty(tk.MaNhanVien) && tk.MaNhanVien.ToLowerInvariant().Contains(key))))
                .ToList();
        }
    }
}
