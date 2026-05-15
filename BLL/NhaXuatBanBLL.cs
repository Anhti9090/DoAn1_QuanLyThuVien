using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NhaXuatBanBLL
    {
        public List<NhaXuatBan> SelectNhaXuatBan()
        {
            return NhaXuatBanAccess.SelectNhaXuatBan();
        }

        public void InsertNhaXuatBan(NhaXuatBan nxb)
        {
            NhaXuatBanAccess.InsertNhaXuatBan(nxb);
        }

        public void UpdateNhaXuatBan(NhaXuatBan nxb)
        {
            NhaXuatBanAccess.UpdateNhaXuatBan(nxb);
        }

        public void DeleteNhaXuatBan(string maNhaXuatBan)
        {
            NhaXuatBanAccess.DeleteNhaXuatBan(maNhaXuatBan);
        }

        public List<NhaXuatBan> SearchNhaXuatBan(string keyword)
        {
            return NhaXuatBanAccess.SearchNhaXuatBan(keyword);
        }
    }
}
