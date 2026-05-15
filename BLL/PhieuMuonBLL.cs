using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class PhieuMuonBLL
    {
        public List<PhieuMuon> SelectPhieuMuon()
        {
            return PhieuMuonAccess.SelectPhieuMuon();
        }
        public void InsertPhieuMuon(PhieuMuon phieuMuon)
        {
            PhieuMuonAccess.InsertPhieuMuon(phieuMuon);
        }
        public void UpdatePhieuMuon(PhieuMuon phieuMuon)
        {
            PhieuMuonAccess.UpdatePhieuMuon(phieuMuon);
        }
        public void DeletePhieuMuon(string maPhieu)
        {
            PhieuMuonAccess.DeletePhieuMuon(maPhieu);
        }
        public List<PhieuMuon> SearchPhieuMuon(string maDocGia, string maPhieu, string trangThai, DateTime tuNgay, DateTime denNgay)
        {
            return PhieuMuonAccess.SearchPhieuMuon(maDocGia, maPhieu, trangThai, tuNgay, denNgay);
        }
        public List<PhieuMuon> SearchPhieuMuon(string text, List<PhieuMuon> list)
        {
            if (string.IsNullOrEmpty(text)) return list;
            return list.Where(p => (p.MaPhieuMuon ?? "").Contains(text)
                                || (p.MaDocGia ?? "").Contains(text)
                                || (p.MaNhanVien ?? "").Contains(text)).ToList();
        }
        public string TaoMaPhieuMoi()
        {
            var list = PhieuMuonAccess.SelectPhieuMuon();
            int max = 0;
            foreach (var p in list)
            {
                if (p.MaPhieuMuon != null && p.MaPhieuMuon.StartsWith("PM"))
                {
                    int n;
                    if (int.TryParse(p.MaPhieuMuon.Substring(2), out n) && n > max) max = n;
                }
            }
            return "PM" + (max + 1).ToString("D2");
        }

        public List<SachViewItem> GetSachView(List<Sach> dsSach)
        {
            return dsSach.Select(s => new SachViewItem
            {
                MaSach = s.MaSach,
                TenSach = s.TenSach,
                TenTheLoai = s.TenTheLoai,
                SoLuongCon = s.SoLuongCon
            }).ToList();
        }

        public List<ChiTietMuonViewItem> GetChiTietView(List<ChiTietMuon> dsChiTiet, List<Sach> dsSach)
        {
            return (from ct in dsChiTiet
                    join s in dsSach on ct.MaSach equals s.MaSach into gj
                    from s in gj.DefaultIfEmpty()
                    select new ChiTietMuonViewItem
                    {
                        MaSach = ct.MaSach,
                        TenSach = s != null ? s.TenSach : "",
                        TenTheLoai = s != null ? s.TenTheLoai : "",
                        SoLuong = ct.SoLuong,
                        TrangThai = ct.TrangThai
                    }).ToList();
        }
    }
}
