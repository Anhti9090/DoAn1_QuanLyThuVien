using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace DAL
{
    public class TacGiaAccess
    {
        public static bool IsMaTacGiaExists(string maTacGia)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM TacGia WHERE MaTacGia=@MaTacGia";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTacGia", maTacGia);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertTacGia(TacGia tacGia)
        {
            if (IsMaTacGiaExists(tacGia.MaTacGia))
            {
                MessageBox.Show("Mã tác giả '" + tacGia.MaTacGia + "' đã tồn tại.");
                
            }

            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "INSERT INTO TacGia (MaTacGia, TenTacGia, MoTa) VALUES (@MaTacGia, @TenTacGia, @MoTa)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTacGia", tacGia.MaTacGia);
                cmd.Parameters.AddWithValue("@TenTacGia", tacGia.TenTacGia);
                cmd.Parameters.AddWithValue("@MoTa", tacGia.MoTa);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            
            finally
            {
                conn.Close();
            }
            
        }
        public static List<TacGia> SelectTacGia()
        {
            List<TacGia> listTacGia = new List<TacGia>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT MaTacGia, TenTacGia, MoTa FROM TacGia";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TacGia tacGia = new TacGia
                    {
                        MaTacGia = reader["MaTacGia"].ToString(),
                        TenTacGia = reader["TenTacGia"].ToString(),
                        MoTa = reader["MoTa"].ToString()
                    };
                    listTacGia.Add(tacGia);
                }
                reader.Close();
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return listTacGia;
        }
        public static void DeleteTacGia(string maTacGia)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM TacGia WHERE MaTacGia=@MaTacGia";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTacGia", maTacGia);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static List<TacGia> SearchTacGia(string text)
        {
            List<TacGia> listTacGia = new List<TacGia>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "SELECT MaTacGia, TenTacGia, MoTa FROM TacGia WHERE MaTacGia LIKE @Text OR TenTacGia LIKE @Text";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Text", "%" + text + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TacGia tacGia = new TacGia
                    {
                        MaTacGia = reader["MaTacGia"].ToString(),
                        TenTacGia = reader["TenTacGia"].ToString(),
                        MoTa = reader["MoTa"].ToString()
                    };
                    listTacGia.Add(tacGia);
                }
                reader.Close();
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return listTacGia;
        }
    }
}
