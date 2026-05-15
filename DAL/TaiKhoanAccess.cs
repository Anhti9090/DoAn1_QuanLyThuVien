using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class TaiKhoanAccess : DatabaseAccess
    {
        public string CheckLogic(TaiKhoan taikhoan)
        {
            string info = CheckLogicDTO(taikhoan);
            return info;
        }

        public static string CheckLogin(TaiKhoan taikhoan)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT Role FROM TaiKhoan WHERE TenDangNhap=@user AND MatKhau=@pass AND TrangThai=N'Hoạt động'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", taikhoan.TenDangNhap);
                cmd.Parameters.AddWithValue("@pass", taikhoan.MatKhau);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string role = reader["Role"].ToString();
                    reader.Close();
                    return role;
                }
                reader.Close();
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public static void InsertTaiKhoan(TaiKhoan taikhoan)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Role, MaNhanVien, TrangThai) VALUES (@TenDangNhap, @MatKhau, @Role, @MaNhanVien, @TrangThai)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", taikhoan.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", taikhoan.MatKhau);
                cmd.Parameters.AddWithValue("@Role", taikhoan.Role);
                cmd.Parameters.AddWithValue("@MaNhanVien", taikhoan.MaNhanVien);
                cmd.Parameters.AddWithValue("@TrangThai", taikhoan.TrangThai);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static List<TaiKhoan> SelectTaiKhoan()
        {
            List<TaiKhoan> list = new List<TaiKhoan>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT TenDangNhap, MatKhau, Role, TrangThai, MaNhanVien FROM TaiKhoan";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TaiKhoan tk = new TaiKhoan
                    {
                        TenDangNhap = reader["TenDangNhap"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                        Role = reader["Role"].ToString(),
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        TrangThai = reader["TrangThai"].ToString()
                    };
                    list.Add(tk);
                }
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
        public static void UpdateTaiKhoan(TaiKhoan taikhoan)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "UPDATE TaiKhoan SET MatKhau=@MatKhau, Role=@Role, MaNhanVien=@MaNhanVien, TrangThai=@TrangThai WHERE TenDangNhap=@TenDangNhap";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", taikhoan.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", taikhoan.MatKhau);
                cmd.Parameters.AddWithValue("@Role", taikhoan.Role);
                cmd.Parameters.AddWithValue("@MaNhanVien", taikhoan.MaNhanVien);
                cmd.Parameters.AddWithValue("@TrangThai", taikhoan.TrangThai);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DeleteTaiKhoan(string TenDangNhap)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM TaiKhoan WHERE TenDangNhap=@TenDangNhap";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void SearchTaiKhoan(string keyword, System.Windows.Forms.DataGridView dataGridView)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT TenDangNhap, MatKhau, Role, TrangThai, MaNhanVien FROM TaiKhoan WHERE TenDangNhap LIKE @Keyword OR Role LIKE @Keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                List<TaiKhoan> list = new List<TaiKhoan>();
                while (reader.Read())
                {
                    TaiKhoan tk = new TaiKhoan
                    {
                        TenDangNhap = reader["TenDangNhap"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                        Role = reader["Role"].ToString(),
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        TrangThai = reader["TrangThai"].ToString()
                    };
                    list.Add(tk);
                }
                dataGridView.DataSource = list;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
