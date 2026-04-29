using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace DoAn1_QuanLyThuVien
{
    public partial class TrangChu : Form
    {
        private readonly PhieuMuonBLL phieuMuonBLL = new PhieuMuonBLL();
        private readonly SachBLL sachBLL = new SachBLL();
        private readonly DocGiaBLL docGiaBLL = new DocGiaBLL();

        public TrangChu()
        {
            InitializeComponent();
            this.Load += TrangChu_Load;
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            BuildOverviewPanel();
            LoadGrids();
        }

        private void BuildOverviewPanel()
        {
            try
            {
                var sachs = sachBLL.SelectSach() ?? new List<Sach>();
                var docGias = docGiaBLL.SelectDocGia() ?? new List<DocGia>();
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();

                int tongSach = sachs.Sum(s => s.TongSoLuong);
                int tongDocGia = docGias.Count;
                int dangMuon = phieus.Count(p => string.Equals(p.TrangThai, "Đang mượn", StringComparison.OrdinalIgnoreCase));
                int quaHan = phieus.Count(p => string.Equals(p.TrangThai, "Quá hạn", StringComparison.OrdinalIgnoreCase)
                                            || (string.Equals(p.TrangThai, "Đang mượn", StringComparison.OrdinalIgnoreCase) && p.NgayTra < DateTime.Now));

                tableLayoutPanel1.Controls.Clear();
                AddOverviewCell("Tổng số đầu sách", tongSach.ToString(), 0);
                AddOverviewCell("Tổng độc giả", tongDocGia.ToString(), 1);
                AddOverviewCell("Đang mượn", dangMuon.ToString(), 2);
                AddOverviewCell("Quá hạn", quaHan.ToString(), 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tổng quan: " + ex.Message);
            }
        }

        private void AddOverviewCell(string title, string value, int col)
        {
            var lblTitle = new Label
            {
                Text = title,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold)
            };
            var lblValue = new Label
            {
                Text = value,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular),
                ForeColor = Color.DarkBlue
            };
            tableLayoutPanel1.Controls.Add(lblTitle, col, 0);
            tableLayoutPanel1.Controls.Add(lblValue, col, 1);
        }

        private void LoadGrids()
        {
            try
            {
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();
                dataGridView1.DataSource = phieus
                    .OrderByDescending(p => p.NgayMuon)
                    .Take(20)
                    .ToList();
                FormatPhieuGrid();

                var sachs = sachBLL.SelectSach() ?? new List<Sach>();
                dataGridView2.DataSource = sachs
                    .OrderBy(s => s.SoLuongCon)
                    .Take(20)
                    .ToList();
                FormatSachGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu trang chủ: " + ex.Message);
            }
        }

        private void FormatPhieuGrid()
        {
            if (dataGridView1.Columns.Count == 0) return;
            if (dataGridView1.Columns.Contains("MaPhieuMuon")) dataGridView1.Columns["MaPhieuMuon"].HeaderText = "Mã Phiếu";
            if (dataGridView1.Columns.Contains("MaDocGia")) dataGridView1.Columns["MaDocGia"].HeaderText = "Mã Độc Giả";
            if (dataGridView1.Columns.Contains("MaNhanVien")) dataGridView1.Columns["MaNhanVien"].HeaderText = "Mã NV";
            if (dataGridView1.Columns.Contains("NgayMuon")) dataGridView1.Columns["NgayMuon"].HeaderText = "Ngày Mượn";
            if (dataGridView1.Columns.Contains("NgayTra")) dataGridView1.Columns["NgayTra"].HeaderText = "Hẹn Trả";
            if (dataGridView1.Columns.Contains("GhiChu")) dataGridView1.Columns["GhiChu"].Visible = false;
            if (dataGridView1.Columns.Contains("TrangThai")) dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private void FormatSachGrid()
        {
            if (dataGridView2.Columns.Count == 0) return;
            if (dataGridView2.Columns.Contains("MaSach")) dataGridView2.Columns["MaSach"].HeaderText = "Mã Sách";
            if (dataGridView2.Columns.Contains("TenSach")) dataGridView2.Columns["TenSach"].HeaderText = "Tên Sách";
            if (dataGridView2.Columns.Contains("MaTacGia")) dataGridView2.Columns["MaTacGia"].Visible = false;
            if (dataGridView2.Columns.Contains("MaTheLoai")) dataGridView2.Columns["MaTheLoai"].Visible = false;
            if (dataGridView2.Columns.Contains("NamXuatBan")) dataGridView2.Columns["NamXuatBan"].Visible = false;
            if (dataGridView2.Columns.Contains("NhaXuatBan")) dataGridView2.Columns["NhaXuatBan"].Visible = false;
            if (dataGridView2.Columns.Contains("TenTacGia")) dataGridView2.Columns["TenTacGia"].Visible = false;
            if (dataGridView2.Columns.Contains("TenTheLoai")) dataGridView2.Columns["TenTheLoai"].Visible = false;
            if (dataGridView2.Columns.Contains("TongSoLuong")) dataGridView2.Columns["TongSoLuong"].HeaderText = "Tổng SL";
            if (dataGridView2.Columns.Contains("SoLuongCon")) dataGridView2.Columns["SoLuongCon"].HeaderText = "Còn Lại";
        }

        private void TrangChu_Load_1(object sender, EventArgs e)
        {

        }
    }
}
