using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class SachBLL
    {
        public List<Sach> SelectSach()
        {
            return SachAccess.SelectSach();
        }
        public void InsertSach(Sach sach)
        {
            SachAccess.InsertSach(sach);
        }
        public void UpdateSach(Sach sach)
        {
            SachAccess.UpdateSach(sach);
        }
        public void DeleteSach(string maSach)
        {
            SachAccess.DeleteSach(maSach);
        }
        public List<Sach> SearchSach(string text, List<Sach> listSach)
        {
            return listSach.Where(s => s.MaSach.Contains(text) || s.TenSach.Contains(text)).ToList();
        }
    }
}
