using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class NhaXuatBanAccess
    {
        public static List<NhaXuatBan> SelectNhaXuatBan()
        {
            List<NhaXuatBan> list = new List<NhaXuatBan>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT MaNhaXuatBan, TenNhaXuatBan, Email, TrangThai FROM NhaXuatBan";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NhaXuatBan nxb = new NhaXuatBan
                    {
                        MaNhaXuatBan = reader["MaNhaXuatBan"].ToString(),
                        TenNhaXuatBan = reader["TenNhaXuatBan"].ToString(),
                        Email = reader["Email"].ToString(),
                        TrangThai = reader["TrangThai"].ToString()
                    };
                    list.Add(nxb);
                }
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        public static void InsertNhaXuatBan(NhaXuatBan nxb)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "INSERT INTO NhaXuatBan (MaNhaXuatBan, TenNhaXuatBan, Email, TrangThai) VALUES (@MaNhaXuatBan, @TenNhaXuatBan, @Email, @TrangThai)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@MaNhaXuatBan", nxb.MaNhaXuatBan);
                command.Parameters.AddWithValue("@TenNhaXuatBan", nxb.TenNhaXuatBan);
                command.Parameters.AddWithValue("@Email", nxb.Email);
                command.Parameters.AddWithValue("@TrangThai", nxb.TrangThai);
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateNhaXuatBan(NhaXuatBan nxb)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "UPDATE NhaXuatBan SET TenNhaXuatBan=@TenNhaXuatBan, Email=@Email, TrangThai=@TrangThai WHERE MaNhaXuatBan=@MaNhaXuatBan";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@MaNhaXuatBan", nxb.MaNhaXuatBan);
                command.Parameters.AddWithValue("@TenNhaXuatBan", nxb.TenNhaXuatBan);
                command.Parameters.AddWithValue("@Email", nxb.Email);
                command.Parameters.AddWithValue("@TrangThai", nxb.TrangThai);
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DeleteNhaXuatBan(string maNhaXuatBan)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM NhaXuatBan WHERE MaNhaXuatBan=@MaNhaXuatBan";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@MaNhaXuatBan", maNhaXuatBan);
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<NhaXuatBan> SearchNhaXuatBan(string keyword)
        {
            List<NhaXuatBan> list = new List<NhaXuatBan>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT MaNhaXuatBan, TenNhaXuatBan, Email, TrangThai FROM NhaXuatBan WHERE MaNhaXuatBan LIKE @keyword OR TenNhaXuatBan LIKE @keyword";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NhaXuatBan nxb = new NhaXuatBan
                    {
                        MaNhaXuatBan = reader["MaNhaXuatBan"].ToString(),
                        TenNhaXuatBan = reader["TenNhaXuatBan"].ToString(),
                        Email = reader["Email"].ToString(),
                        TrangThai = reader["TrangThai"].ToString()
                    };
                    list.Add(nxb);
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
