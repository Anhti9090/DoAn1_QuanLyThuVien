using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Sach
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string MaTacGia { get; set; }
        public string MaTheLoai { get; set; }
        public string NamXuatBan { get; set; }
        public string NhaXuatBan { get; set; }
        public int TongSoLuong { get; set; }
        public int SoLuongCon { get; set; }

        // Property bổ sung cho hiển thị (không map bảng Sach)
        public string TenTacGia { get; set; }
        public string TenTheLoai { get; set; }
    }
}
