using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TheLoaiAccess
    {
        public static List<TheLoai> SelectTheLoai()
        {
            List<TheLoai> listTheLoai = new List<TheLoai>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT MaTheLoai, TenTheLoai, MoTa FROM TheLoai";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TheLoai theLoai = new TheLoai
                    {
                        MaTheLoai = reader["MaTheLoai"].ToString(),
                        TenTheLoai = reader["TenTheLoai"].ToString(),
                        MoTa = reader["MoTa"].ToString()
                    };
                    listTheLoai.Add(theLoai);
                }
                reader.Close();
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return listTheLoai;
        }
        public static void InsertTheLoai(TheLoai theLoai)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "INSERT INTO TheLoai (MaTheLoai, TenTheLoai, MoTa) VALUES (@MaTheLoai, @TenTheLoai, @MoTa)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTheLoai", theLoai.MaTheLoai);
                cmd.Parameters.AddWithValue("@TenTheLoai", theLoai.TenTheLoai);
                cmd.Parameters.AddWithValue("@MoTa", theLoai.MoTa);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }

        }

        public static void DeleteTheLoai(string maTheLoai)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM TheLoai WHERE MaTheLoai = @MaTheLoai";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTheLoai", maTheLoai);
                cmd.Connection = conn;
                cmd.ExecuteReader();
            }
            finally
            {
                conn.Close();
            }

        }

        public static List<TheLoai> TimKiemTheLoai(string text)
        {
            List<TheLoai> listTheLoai = new List<TheLoai>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT MaTheLoai, TenTheLoai, MoTa FROM TheLoai WHERE MaTheLoai LIKE @text OR TenTheLoai LIKE @text";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@text", "%" + text + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TheLoai theLoai = new TheLoai
                    {
                        MaTheLoai = reader["MaTheLoai"].ToString(),
                        TenTheLoai = reader["TenTheLoai"].ToString(),
                        MoTa = reader["MoTa"].ToString()
                    };
                    listTheLoai.Add(theLoai);
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return listTheLoai;
        }
    }
}
