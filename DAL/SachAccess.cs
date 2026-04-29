using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class SachAccess
    {
        public static List<Sach> SelectSach()
        {
            List<Sach> list = new List<Sach>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = @"
                    SELECT 
                        s.MaSach, s.TenSach, s.MaTacGia, s.MaTheLoai, 
                        s.NamXuatBan, s.NhaXuatBan, s.TongSoLuong, s.SoLuongCon,
                        tg.TenTacGia, tl.TenTheLoai
                    FROM Sach s
                    LEFT JOIN TacGia tg ON s.MaTacGia = tg.MaTacGia
                    LEFT JOIN TheLoai tl ON s.MaTheLoai = tl.MaTheLoai";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Sach sach = new Sach
                    {
                        MaSach = reader["MaSach"].ToString(),
                        TenSach = reader["TenSach"].ToString(),
                        MaTacGia = reader["MaTacGia"].ToString(),
                        MaTheLoai = reader["MaTheLoai"].ToString(),
                        NamXuatBan = reader["NamXuatBan"].ToString(),
                        NhaXuatBan = reader["NhaXuatBan"].ToString(),
                        TongSoLuong = Convert.ToInt32(reader["TongSoLuong"]),
                        SoLuongCon = Convert.ToInt32(reader["SoLuongCon"]),
                        TenTacGia = reader["TenTacGia"].ToString(),
                        TenTheLoai = reader["TenTheLoai"].ToString()
                    };
                    list.Add(sach);
                }
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
        public static void InsertSach(Sach sach)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "INSERT INTO Sach (MaSach, TenSach, MaTacGia, MaTheLoai, NamXuatBan, NhaXuatBan, TongSoLuong, SoLuongCon) VALUES (@MaSach, @TenSach, @MaTacGia, @MaTheLoai, @NamXuatBan, @NhaXuatBan, @TongSoLuong, @SoLuongCon)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@MaSach", sach.MaSach);
                command.Parameters.AddWithValue("@TenSach", sach.TenSach);
                command.Parameters.AddWithValue("@MaTacGia", sach.MaTacGia);
                command.Parameters.AddWithValue("@MaTheLoai", sach.MaTheLoai);
                command.Parameters.AddWithValue("@NamXuatBan", sach.NamXuatBan);
                command.Parameters.AddWithValue("@NhaXuatBan", sach.NhaXuatBan);
                command.Parameters.AddWithValue("@TongSoLuong", sach.TongSoLuong);
                command.Parameters.AddWithValue("@SoLuongCon", sach.SoLuongCon);
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void UpdateSach(Sach sach)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "UPDATE Sach SET TenSach=@TenSach, MaTacGia=@MaTacGia, MaTheLoai=@MaTheLoai, NamXuatBan=@NamXuatBan, NhaXuatBan=@NhaXuatBan, TongSoLuong=@TongSoLuong, SoLuongCon=@SoLuongCon WHERE MaSach=@MaSach";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@MaSach", sach.MaSach);
                command.Parameters.AddWithValue("@TenSach", sach.TenSach);
                command.Parameters.AddWithValue("@MaTacGia", sach.MaTacGia);
                command.Parameters.AddWithValue("@MaTheLoai", sach.MaTheLoai);
                command.Parameters.AddWithValue("@NamXuatBan", sach.NamXuatBan);
                command.Parameters.AddWithValue("@NhaXuatBan", sach.NhaXuatBan);
                command.Parameters.AddWithValue("@TongSoLuong", sach.TongSoLuong);
                command.Parameters.AddWithValue("@SoLuongCon", sach.SoLuongCon);
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DeleteSach(string maSach)
        {
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = "DELETE FROM Sach WHERE MaSach=@MaSach";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@MaSach", maSach);
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static List<Sach> SearchSach(string keyword)
        {
            List<Sach> list = new List<Sach>();
            SqlConnection conn = SqlConnectionData.Connect();
            try
            {
                conn.Open();
                string query = @"
                    SELECT 
                        s.MaSach, s.TenSach, s.MaTacGia, s.MaTheLoai, 
                        s.NamXuatBan, s.NhaXuatBan, s.TongSoLuong, s.SoLuongCon,
                        tg.TenTacGia, tl.TenTheLoai
                    FROM Sach s
                    LEFT JOIN TacGia tg ON s.MaTacGia = tg.MaTacGia
                    LEFT JOIN TheLoai tl ON s.MaTheLoai = tl.MaTheLoai
                    WHERE s.MaSach LIKE @keyword OR s.TenSach LIKE @keyword";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Sach sach = new Sach
                    {
                        MaSach = reader["MaSach"].ToString(),
                        TenSach = reader["TenSach"].ToString(),
                        MaTacGia = reader["MaTacGia"].ToString(),
                        MaTheLoai = reader["MaTheLoai"].ToString(),
                        NamXuatBan = reader["NamXuatBan"].ToString(),
                        NhaXuatBan = reader["NhaXuatBan"].ToString(),
                        TongSoLuong = Convert.ToInt32(reader["TongSoLuong"]),
                        SoLuongCon = Convert.ToInt32(reader["SoLuongCon"]),
                        TenTacGia = reader["TenTacGia"].ToString(),
                        TenTheLoai = reader["TenTheLoai"].ToString()
                    };
                    list.Add(sach);
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
