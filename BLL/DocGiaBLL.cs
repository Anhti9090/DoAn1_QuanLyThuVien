using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class DocGiaBLL
    {
        public List<DocGia> SelectDocGia()
        {
            return DocGiaAccess.SelectDocGia();
        }
        public void InsertDocGia(DocGia docGia)
        {
            DocGiaAccess.InsertDocGia(docGia);
        }
        public void DeleteDocGia(string maDocGia)
        {
            DocGiaAccess.DeleteDocGia(maDocGia);
        }
        public void UpdateDocGia(DocGia docGia)
        {
            DocGiaAccess.UpdateDocGia(docGia);
        }
        public List<DocGia> SearchDocGia(string text, List<DocGia> listDocGia)
        {
            return listDocGia.Where(d => d.MaDocGia.Contains(text) || d.TenDocGia.Contains(text)).ToList();
        }
    }
}
