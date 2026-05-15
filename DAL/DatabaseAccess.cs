using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class SqlConnectionData
    {
        public static SqlConnection Connect()
        {
            string strcon = "Data Source=CUONG\\SQLEXPRESS;Initial Catalog=QuanLyThuVien;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strcon); //Kết nối đến database
            return conn;
        }
    }
    public class DatabaseAccess
    {
        public static string CheckLogicDTO(TaiKhoan taikhoan)
        {
            string username = null;
            SqlConnection conn = SqlConnectionData.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand("proc_logic", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User", taikhoan.TenDangNhap);
            cmd.Parameters.AddWithValue("@Pass", taikhoan.MatKhau);

            cmd.Connection = conn;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    username = reader.GetString(0);
                }
                reader.Close();
                conn.Close();
            }
            else
            {
                return "Tài khoản hoặc mật khẩu không đúng!";
            }
            return username;
        }
    }
}
