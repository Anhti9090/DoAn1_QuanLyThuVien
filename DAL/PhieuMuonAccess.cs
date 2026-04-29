using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class PhieuMuonAccess
    {
        public static List<PhieuMuon> SelectPhieuMuon()
        {
            SqlConnection conn = SqlConnectionData.Connect();
            List<PhieuMuon> list = new List<PhieuMuon>();
            try
            {
                conn.Open();
                string query = "SELECT MaPhieu, MaDocGia, MaNhanVien, NgayMuon, NgayHenTra, GhiChu, TrangThai FROM PhieuMuon";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PhieuMuon phieuMuon = new PhieuMuon
                    {
                        MaPhieuMuon = reader["MaPhieu"].ToString(),
                        MaDocGia = reader["MaDocGia"].ToString(),
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        NgayMuon = Convert.ToDateTime(reader["NgayMuon"]),
                        NgayTra = Convert.ToDateTime(reader["NgayHenTra"]),
                        GhiChu = reader["GhiChu"].ToString(),
                        TrangThai = reader["TrangThai"].ToString()
                    };
                    list.Add(phieuMuon);

                }
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
        public static void InsertPhieuMuon(PhieuMuon phieuMuon)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "INSERT INTO PhieuMuon (MaPhieu, MaDocGia, MaNhanVien, NgayMuon, NgayHenTra, GhiChu, TrangThai) VALUES (@MaPhieu, @MaDocGia, @MaNhanVien, @NgayMuon, @NgayHenTra, @GhiChu, @TrangThai)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", phieuMuon.MaPhieuMuon);
                cmd.Parameters.AddWithValue("@MaDocGia", phieuMuon.MaDocGia);
                cmd.Parameters.AddWithValue("@MaNhanVien", phieuMuon.MaNhanVien);
                cmd.Parameters.AddWithValue("@NgayMuon", phieuMuon.NgayMuon);
                cmd.Parameters.AddWithValue("@NgayHenTra", phieuMuon.NgayTra);
                cmd.Parameters.AddWithValue("@GhiChu", phieuMuon.GhiChu);
                cmd.Parameters.AddWithValue("@TrangThai", phieuMuon.TrangThai);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void UpdatePhieuMuon(PhieuMuon phieuMuon)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "UPDATE PhieuMuon SET MaDocGia = @MaDocGia, MaNhanVien = @MaNhanVien, NgayMuon = @NgayMuon, NgayHenTra = @NgayHenTra, GhiChu = @GhiChu, TrangThai = @TrangThai WHERE MaPhieu = @MaPhieu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", phieuMuon.MaPhieuMuon);
                cmd.Parameters.AddWithValue("@MaDocGia", phieuMuon.MaDocGia);
                cmd.Parameters.AddWithValue("@MaNhanVien", phieuMuon.MaNhanVien);
                cmd.Parameters.AddWithValue("@NgayMuon", phieuMuon.NgayMuon);
                cmd.Parameters.AddWithValue("@NgayHenTra", phieuMuon.NgayTra);
                cmd.Parameters.AddWithValue("@GhiChu", phieuMuon.GhiChu);
                cmd.Parameters.AddWithValue("@TrangThai", phieuMuon.TrangThai);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DeletePhieuMuon(string maPhieu)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM PhieuMuon WHERE MaPhieu = @MaPhieu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static List<PhieuMuon> SearchPhieuMuon(string madocgia, string maphieu, string trangthai, DateTime tungay, DateTime denngay)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            List<PhieuMuon> list = new List<PhieuMuon>();
            try
            {
                conn.Open();
                string query = "SELECT MaPhieu, MaDocGia, MaNhanVien, NgayMuon, NgayHenTra, GhiChu, TrangThai FROM PhieuMuon WHERE MaDocGia LIKE @MaDocGia OR MaPhieu LIKE @MaPhieu OR TrangThai LIKE @TrangThai OR (NgayMuon >= @TuNgay AND NgayMuon <= @DenNgay)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDocGia", "%" + madocgia + "%");
                cmd.Parameters.AddWithValue("@MaPhieu", "%" + maphieu + "%");
                cmd.Parameters.AddWithValue("@TrangThai", "%" + trangthai + "%");
                cmd.Parameters.AddWithValue("@TuNgay", tungay);
                cmd.Parameters.AddWithValue("@DenNgay", denngay);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PhieuMuon phieuMuon = new PhieuMuon
                    {
                        MaPhieuMuon = reader["MaPhieu"].ToString(),
                        MaDocGia = reader["MaDocGia"].ToString(),
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        NgayMuon = Convert.ToDateTime(reader["NgayMuon"]),
                        NgayTra = Convert.ToDateTime(reader["NgayHenTra"]),
                        GhiChu = reader["GhiChu"].ToString(),
                        TrangThai = reader["TrangThai"].ToString()
                    };
                    list.Add(phieuMuon);
                }
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
    }
}

