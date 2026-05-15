namespace BLL
{
    public class SachViewItem
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string TenTheLoai { get; set; }
        public int SoLuongCon { get; set; }
    }

    public class ChiTietMuonViewItem
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string TenTheLoai { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; }
    }
}
