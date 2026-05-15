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
            LoadOverdueGrid();
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

                // Clear and build visual tiles
                panelTiles.Controls.Clear();
                AddOverviewTile("TỔNG ĐẦU SÁCH", tongSach.ToString(), 0);
                AddOverviewTile("ĐỘC GIẢ HOẠT ĐỘNG", tongDocGia.ToString(), 1);
                AddOverviewTile("PHIẾU ĐANG MƯỢN", dangMuon.ToString(), 2);
                AddOverviewTile("PHIẾU QUÁ HẠN", quaHan.ToString(), 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tổng quan: " + ex.Message);
            }
        }
        private void AddOverviewTile(string title, string value, int index)
        {
            var panel = new Panel
            {
                Width = 260,
                Height = 90,
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Color accent;
            switch (index)
            {
                case 0: accent = Color.FromArgb(0, 123, 255); break; // blue
                case 1: accent = Color.FromArgb(40, 167, 69); break; // green
                case 2: accent = Color.FromArgb(255, 193, 7); break; // yellow
                case 3: accent = Color.FromArgb(220, 53, 69); break; // red
                default: accent = Color.Gray; break;
            }

            var pnlAccent = new Panel { Dock = DockStyle.Top, Height = 8, BackColor = accent };

            var lblTitle = new Label
            {
                Text = title,
                AutoSize = false,
                Height = 28,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
            };

            var lblValue = new Label
            {
                Text = value,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold),
                ForeColor = accent
            };

            panel.Controls.Add(lblValue);
            panel.Controls.Add(lblTitle);
            panel.Controls.Add(pnlAccent);

            panelTiles.Controls.Add(panel);
        }

        private void LoadOverdueGrid()
        {
            try
            {
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();
                var chiTiets = new ChiTietMuonBLL().SelectChiTietMuon() ?? new List<ChiTietMuon>();
                var sachs = sachBLL.SelectSach() ?? new List<Sach>();
                var docGias = docGiaBLL.SelectDocGia() ?? new List<DocGia>();

                var query = from ct in chiTiets
                            join p in phieus on ct.MaPhieuMuon equals p.MaPhieuMuon
                            where ct.NgayTra < DateTime.Now
                            select new
                            {
                                MaPhieu = p.MaPhieuMuon,
                                TenDocGia = docGias.FirstOrDefault(d => d.MaDocGia == p.MaDocGia)?.TenDocGia ?? "N/A",
                                Lop = docGias.FirstOrDefault(d => d.MaDocGia == p.MaDocGia)?.Lop ?? "N/A",
                                SoDienThoai = docGias.FirstOrDefault(d => d.MaDocGia == p.MaDocGia)?.SoDienThoai ?? "N/A",
                                TenSach = sachs.FirstOrDefault(s => s.MaSach == ct.MaSach)?.TenSach ?? "N/A",
                                NgayMuon = p.NgayMuon.ToString("dd/MM/yyyy"),
                                HanTra = ct.NgayTra.ToString("dd/MM/yyyy"),
                                SoNgayTre = (DateTime.Now.Date - ct.NgayTra.Date).Days
                            };

                var list = query.OrderByDescending(x => x.SoNgayTre).ToList();
                dataGridViewOverdue.DataSource = list;

                // Format columns
                if (dataGridViewOverdue.Columns.Count > 0)
                {
                    if (dataGridViewOverdue.Columns.Contains("MaPhieu")) dataGridViewOverdue.Columns["MaPhieu"].HeaderText = "Mã Phiếu";
                    if (dataGridViewOverdue.Columns.Contains("TenDocGia")) dataGridViewOverdue.Columns["TenDocGia"].HeaderText = "Tên Độc Giả";
                    if (dataGridViewOverdue.Columns.Contains("Lop")) dataGridViewOverdue.Columns["Lop"].HeaderText = "Lớp";
                    if (dataGridViewOverdue.Columns.Contains("SoDienThoai")) dataGridViewOverdue.Columns["SoDienThoai"].HeaderText = "SĐT";
                    if (dataGridViewOverdue.Columns.Contains("TenSach")) dataGridViewOverdue.Columns["TenSach"].HeaderText = "Tên Sách";
                    if (dataGridViewOverdue.Columns.Contains("NgayMuon")) dataGridViewOverdue.Columns["NgayMuon"].HeaderText = "Ngày Mượn";
                    if (dataGridViewOverdue.Columns.Contains("HanTra")) dataGridViewOverdue.Columns["HanTra"].HeaderText = "Hạn Trả";
                    if (dataGridViewOverdue.Columns.Contains("SoNgayTre")) dataGridViewOverdue.Columns["SoNgayTre"].HeaderText = "Số Ngày Trễ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu trang chủ: " + ex.Message);
            }
        }

        private void FormatPhieuGrid()
        {
            // No-op: legacy grid formatting removed. Using dataGridViewOverdue for main dashboard list.
        }

        private void FormatSachGrid()
        {
            // No-op: legacy grid formatting removed.
        }

        private void TrangChu_Load_1(object sender, EventArgs e)
        {

        }

        private void labelOverdue_Click(object sender, EventArgs e)
        {

        }
    }
}
