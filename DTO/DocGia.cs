using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DocGia
    {
        //MaDocGia, TenDocGia, NgaySinh, GioiTinh, DiaChi, Lop, SoDienThoai, Email, NgayLapThe, TrangThai
        public string MaDocGia { get; set; }
        public string TenDocGia { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Lop { get; set; }
        public DateTime NgayLapThe { get; set; }
        public string TrangThai { get; set; }
    }
}
