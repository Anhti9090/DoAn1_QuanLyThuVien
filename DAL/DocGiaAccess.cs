using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DocGiaAccess
    {
        public static List<DocGia> SelectDocGia()
        {
            List<DocGia> list = new List<DocGia>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT * FROM DocGia";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DocGia docGia = new DocGia
                    {
                        MaDocGia = reader["MaDocGia"].ToString(),
                        TenDocGia = reader["TenDocGia"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        NgayLapThe = Convert.ToDateTime(reader["NgayLapThe"]),
                        Lop = reader["Lop"].ToString(),
                        Email = reader["Email"].ToString(),
                        TrangThai = reader["TrangThai"].ToString(),
                    };
                    list.Add(docGia);
                }
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
        public static void InsertDocGia(DocGia docGia)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "INSERT INTO DocGia (MaDocGia, TenDocGia, NgaySinh, GioiTinh, DiaChi, SoDienThoai, NgayLapThe, Lop, Email, TrangThai) VALUES (@MaDocGia, @TenDocGia, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai, @NgayLapThe, @Lop, @Email, @TrangThai)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@MaDocGia", docGia.MaDocGia);
                command.Parameters.AddWithValue("@TenDocGia", docGia.TenDocGia);
                command.Parameters.AddWithValue("@NgaySinh", docGia.NgaySinh);
                command.Parameters.AddWithValue("@GioiTinh", docGia.GioiTinh);
                command.Parameters.AddWithValue("@DiaChi", docGia.DiaChi);
                command.Parameters.AddWithValue("@SoDienThoai", docGia.SoDienThoai);
                command.Parameters.AddWithValue("@NgayLapThe", docGia.NgayLapThe);
                command.Parameters.AddWithValue("@Lop", (object)docGia.Lop ?? DBNull.Value);
                command.Parameters.AddWithValue("@Email", docGia.Email);
                command.Parameters.AddWithValue("@TrangThai", docGia.TrangThai);
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void UpdateDocGia(DocGia docGia)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "UPDATE DocGia SET TenDocGia=@TenDocGia, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, DiaChi=@DiaChi, SoDienThoai=@SoDienThoai, NgayLapThe=@NgayLapThe, Lop=@Lop, Email=@Email, TrangThai=@TrangThai WHERE MaDocGia=@MaDocGia";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@MaDocGia", docGia.MaDocGia);
                command.Parameters.AddWithValue("@TenDocGia", docGia.TenDocGia);
                command.Parameters.AddWithValue("@NgaySinh", docGia.NgaySinh);
                command.Parameters.AddWithValue("@GioiTinh", docGia.GioiTinh);
                command.Parameters.AddWithValue("@DiaChi", docGia.DiaChi);
                command.Parameters.AddWithValue("@SoDienThoai", docGia.SoDienThoai);
                command.Parameters.AddWithValue("@NgayLapThe", docGia.NgayLapThe);
                command.Parameters.AddWithValue("@Lop", (object)docGia.Lop ?? DBNull.Value);
                command.Parameters.AddWithValue("@Email", docGia.Email);
                command.Parameters.AddWithValue("@TrangThai", docGia.TrangThai);
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DeleteDocGia(string maDocGia)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM DocGia WHERE MaDocGia=@MaDocGia";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@MaDocGia", maDocGia);
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static List<DocGia> SearchDocGia(string keyword)
        {
            List<DocGia> list = new List<DocGia>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT * FROM DocGia WHERE MaDocGia LIKE @keyword OR TenDocGia LIKE @keyword OR DiaChi LIKE @keyword OR SoDienThoai LIKE @keyword OR Email LIKE @keyword";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DocGia docGia = new DocGia
                    {
                        MaDocGia = reader["MaDocGia"].ToString(),
                        TenDocGia = reader["TenDocGia"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        NgayLapThe = Convert.ToDateTime(reader["NgayLapThe"]),
                        Lop = reader["Lop"].ToString(),
                        Email = reader["Email"].ToString(),
                        TrangThai = reader["TrangThai"].ToString(),
                    };
                    list.Add(docGia);
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
