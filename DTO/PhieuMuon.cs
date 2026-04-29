using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhieuMuon
    {
        public string MaPhieuMuon { get; set; }
        public string MaDocGia { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayTra { get; set; }
        public string GhiChu { get; set; }
        public string TrangThai { get; set; }
    }
}
