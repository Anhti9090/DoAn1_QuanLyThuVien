using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ChiTietMuonAccess
    {
        public static List<ChiTietMuon> SelectChiTietMuon()
        {
            SqlConnection conn = SqlConnectionData.Connect();
            List<ChiTietMuon> list = new List<ChiTietMuon>();
            try
            {
                conn.Open();
                string query = "SELECT MaPhieu, MaSach, SoLuong, NgayTra, TienPhat, TrangThai FROM ChiTietMuon";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new ChiTietMuon
                    {
                        MaPhieuMuon = reader["MaPhieu"].ToString(),
                        MaSach = reader["MaSach"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        NgayTra = reader["NgayTra"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgayTra"]),
                        TienPhat = reader["TienPhat"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TienPhat"]),
                        TrangThai = reader["TrangThai"].ToString()
                    });
                }
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        public static List<ChiTietMuon> SelectByMaPhieu(string maPhieu)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            List<ChiTietMuon> list = new List<ChiTietMuon>();
            try
            {
                conn.Open();
                string query = "SELECT MaPhieu, MaSach, SoLuong, NgayTra, TienPhat, TrangThai FROM ChiTietMuon WHERE MaPhieu = @MaPhieu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new ChiTietMuon
                    {
                        MaPhieuMuon = reader["MaPhieu"].ToString(),
                        MaSach = reader["MaSach"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        NgayTra = reader["NgayTra"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgayTra"]),
                        TienPhat = reader["TienPhat"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TienPhat"]),
                        TrangThai = reader["TrangThai"].ToString()
                    });
                }
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        public static void InsertChiTietMuon(ChiTietMuon ct)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "INSERT INTO ChiTietMuon (MaPhieu, MaSach, SoLuong, NgayTra, TienPhat, TrangThai) " +
                               "VALUES (@MaPhieu, @MaSach, @SoLuong, @NgayTra, @TienPhat, @TrangThai)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", ct.MaPhieuMuon);
                cmd.Parameters.AddWithValue("@MaSach", ct.MaSach);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                if (ct.NgayTra == DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@NgayTra", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NgayTra", ct.NgayTra);
                cmd.Parameters.AddWithValue("@TienPhat", ct.TienPhat);
                cmd.Parameters.AddWithValue("@TrangThai", ct.TrangThai ?? "Chưa trả");
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateChiTietMuon(ChiTietMuon ct)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "UPDATE ChiTietMuon SET SoLuong = @SoLuong, NgayTra = @NgayTra, TienPhat = @TienPhat, TrangThai = @TrangThai " +
                               "WHERE MaPhieu = @MaPhieu AND MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", ct.MaPhieuMuon);
                cmd.Parameters.AddWithValue("@MaSach", ct.MaSach);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                if (ct.NgayTra == DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@NgayTra", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NgayTra", ct.NgayTra);
                cmd.Parameters.AddWithValue("@TienPhat", ct.TienPhat);
                cmd.Parameters.AddWithValue("@TrangThai", ct.TrangThai ?? "Chưa trả");
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DeleteChiTietMuon(string maPhieu, string maSach)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM ChiTietMuon WHERE MaPhieu = @MaPhieu AND MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DeleteByMaPhieu(string maPhieu)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM ChiTietMuon WHERE MaPhieu = @MaPhieu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
