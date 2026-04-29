using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class TacGiaBLL
    {
        public List<TacGia> SelectTacGia()
        {
            return TacGiaAccess.SelectTacGia();
        }
        public void AddTacGia(TacGia tacGia)
        {
            TacGiaAccess.InsertTacGia(tacGia);
        }
        public void DeleteTacGia(string maTacGia)
        {
            TacGiaAccess.DeleteTacGia(maTacGia);
        }
        public List<TacGia> SearchTacGia(string text, List<TacGia> listTacGia)
        {
            return listTacGia.Where(t => t.MaTacGia.Contains(text) || t.TenTacGia.Contains(text)).ToList();
        }
    }
}
