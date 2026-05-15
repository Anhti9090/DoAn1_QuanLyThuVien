using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class NhanVienAccess
    {
        public static List<NhanVien> SelectNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT MaNhanVien, TenNhanVien, GioiTinh, NgaySinh, DiaChi, SoDienThoai, Email, ChucVu FROM NhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NhanVien nv = new NhanVien
                    {
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        TenNhanVien = reader["TenNhanVien"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                        DiaChi = reader["DiaChi"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        Email = reader["Email"].ToString(),
                        ChucVu = reader["ChucVu"].ToString()
                    };
                    list.Add(nv);
                }
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
        public static void InsertNhanVien(NhanVien nv)
        {
            if (CheckMaNhanVienExists(nv.MaNhanVien))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại.");
            }
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, GioiTinh, NgaySinh, DiaChi, SoDienThoai, Email, ChucVu) VALUES (@MaNhanVien, @TenNhanVien, @GioiTinh, @NgaySinh, @DiaChi, @SoDienThoai, @Email, @ChucVu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", nv.MaNhanVien);
                cmd.Parameters.AddWithValue("@TenNhanVien", nv.TenNhanVien);
                cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
                cmd.Parameters.AddWithValue("@SoDienThoai", nv.SoDienThoai);
                cmd.Parameters.AddWithValue("@Email", nv.Email);
                cmd.Parameters.AddWithValue("@ChucVu", (object)nv.ChucVu ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void SearchNhanVien(string keyword, DataGridView dataGridView)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT MaNhanVien, TenNhanVien, GioiTinh, NgaySinh, DiaChi, SoDienThoai, Email, ChucVu FROM NhanVien WHERE MaNhanVien LIKE @Keyword OR TenNhanVien LIKE @Keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                List<NhanVien> list = new List<NhanVien>();
                while (reader.Read())
                {
                    NhanVien nv = new NhanVien
                    {
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        TenNhanVien = reader["TenNhanVien"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                        DiaChi = reader["DiaChi"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        Email = reader["Email"].ToString(),
                        ChucVu = reader["ChucVu"].ToString()
                    };
                    list.Add(nv);
                }
                dataGridView.DataSource = list;
            }
            finally
            {
                conn.Close();
            }
        }
        public static void UpdateNhanVien(NhanVien nv)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "UPDATE NhanVien SET TenNhanVien=@TenNhanVien, GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, DiaChi=@DiaChi, SoDienThoai=@SoDienThoai, Email=@Email, ChucVu=@ChucVu WHERE MaNhanVien=@MaNhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", nv.MaNhanVien);
                cmd.Parameters.AddWithValue("@TenNhanVien", nv.TenNhanVien);
                cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
                cmd.Parameters.AddWithValue("@SoDienThoai", nv.SoDienThoai);
                cmd.Parameters.AddWithValue("@Email", nv.Email);
                cmd.Parameters.AddWithValue("@ChucVu", (object)nv.ChucVu ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DeleteNhanVien(string maNhanVien)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM NhanVien WHERE MaNhanVien=@MaNhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static bool CheckMaNhanVienExists(string maNhanVien)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien=@MaNhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
