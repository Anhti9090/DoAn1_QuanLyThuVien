using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class ChiTietMuonBLL
    {
        public List<ChiTietMuon> SelectChiTietMuon()
        {
            return ChiTietMuonAccess.SelectChiTietMuon();
        }
        public List<ChiTietMuon> SelectByMaPhieu(string maPhieu)
        {
            return ChiTietMuonAccess.SelectByMaPhieu(maPhieu);
        }
        public void InsertChiTietMuon(ChiTietMuon ct)
        {
            ChiTietMuonAccess.InsertChiTietMuon(ct);
        }
        public void UpdateChiTietMuon(ChiTietMuon ct)
        {
            ChiTietMuonAccess.UpdateChiTietMuon(ct);
        }
        public void DeleteChiTietMuon(string maPhieu, string maSach)
        {
            ChiTietMuonAccess.DeleteChiTietMuon(maPhieu, maSach);
        }
        public void DeleteByMaPhieu(string maPhieu)
        {
            ChiTietMuonAccess.DeleteByMaPhieu(maPhieu);
        }
    }
}
