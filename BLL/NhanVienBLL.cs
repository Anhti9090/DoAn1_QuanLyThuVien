using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class NhanVienBLL
    {
        public void InsertNhanVien(NhanVien nhanVien)
        {
            NhanVienAccess.InsertNhanVien(nhanVien);
        }
        public List<NhanVien> SelectNhanVien()
        {
            return NhanVienAccess.SelectNhanVien();
        }
        public static List<NhanVien> SearchNhanVien(string text, List<NhanVien> listNhanVien)
        {
            return listNhanVien.Where(n => n.MaNhanVien.Contains(text) || n.TenNhanVien.Contains(text)).ToList();
        }
        public void UpdateNhanVien(NhanVien nhanVien)
        {
            NhanVienAccess.UpdateNhanVien(nhanVien);
        }
        public void DeleteNhanVien(string maNhanVien)
        {
            NhanVienAccess.DeleteNhanVien(maNhanVien);
        }
    }
}
