using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class TheLoaiBLL
    {
        public List<TheLoai> GetTheLoai()
        {
            return TheLoaiAccess.SelectTheLoai();
        }
    
        public void AddTheLoai(TheLoai theLoai)
        {
            TheLoaiAccess.InsertTheLoai(theLoai);
        }
    
        public void DeleteTheLoai(string maTheLoai)
        {
            TheLoaiAccess.DeleteTheLoai(maTheLoai);
        }
    
        public List<TheLoai> SearchTheLoai(string text, List<TheLoai> listTheLoai)
        {
            return listTheLoai.Where(t => t.MaTheLoai.Contains(text) || t.TenTheLoai.Contains(text)).ToList();
        }
    }
}
