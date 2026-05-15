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
    public partial class ThongKe : Form
    {
        private readonly PhieuMuonBLL phieuMuonBLL = new PhieuMuonBLL();
        private readonly SachBLL sachBLL = new SachBLL();
        private readonly DocGiaBLL docGiaBLL = new DocGiaBLL();
        private readonly NhaXuatBanBLL nhaXuatBanBLL = new NhaXuatBanBLL();
        private readonly ChiTietMuonBLL chiTietMuonBLL = new ChiTietMuonBLL();
        private readonly TacGiaBLL tacGiaBLL = new TacGiaBLL();
        private readonly TheLoaiBLL theLoaiBLL = new TheLoaiBLL();

        public ThongKe()
        {
            InitializeComponent();
            this.Load += ThongKe_Load;
            this.comboBoxLoaiThongKe.SelectedIndexChanged += FilterChanged;
            this.comboBoxTrangThai.SelectedIndexChanged += FilterChanged;
            this.dateTimePickerTuNgay.ValueChanged += FilterChanged;
            this.dateTimePickerDenNgay.ValueChanged += FilterChanged;
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            comboBoxLoaiThongKe.Items.Clear();
            comboBoxLoaiThongKe.Items.AddRange(new object[] { 
                "Nhà xuất bản", 
                "Tác giả", 
                "Thể loại sách",
                "Sách",
                "Phiếu mượn",
                "Độc giả"
            });
            comboBoxLoaiThongKe.SelectedIndex = 0;
            comboBoxLoaiThongKe.Enabled = true;

            comboBoxTrangThai.Items.Clear();
            comboBoxTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang mượn", "Đã trả", "Quá hạn" });
            comboBoxTrangThai.SelectedIndex = 0;

            dateTimePickerTuNgay.Value = DateTime.Now.AddMonths(-1);
            dateTimePickerDenNgay.Value = DateTime.Now;

            LoadQuickReport();
            LoadData();
        }

        private void LoadQuickReport()
        {
            try
            {
                var sachs = sachBLL.SelectSach() ?? new List<Sach>();
                var docGias = docGiaBLL.SelectDocGia() ?? new List<DocGia>();
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();

                labelTongSoSach.Text = sachs.Sum(s => s.TongSoLuong).ToString();
                labelTongDocGia.Text = docGias.Count.ToString();
                labelDangMuon.Text = phieus.Count(p => string.Equals(p.TrangThai, "Đang mượn", StringComparison.OrdinalIgnoreCase)).ToString();
                labelQuaHan.Text = phieus.Count(p => string.Equals(p.TrangThai, "Quá hạn", StringComparison.OrdinalIgnoreCase)
                                                  || (string.Equals(p.TrangThai, "Đang mượn", StringComparison.OrdinalIgnoreCase) && p.NgayTra < DateTime.Now)).ToString();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi tải báo cáo nhanh: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DateTime tuNgay = dateTimePickerTuNgay.Value.Date;
                DateTime denNgay = dateTimePickerDenNgay.Value.Date.AddDays(1).AddTicks(-1);
                string trangThai = comboBoxTrangThai.SelectedItem?.ToString() ?? "Tất cả";
                string loai = comboBoxLoaiThongKe.SelectedItem?.ToString() ?? "Nhà xuất bản";

                switch (loai)
                {
                    case "Nhà xuất bản":
                        LoadNhaXuatBanChiTiet(trangThai, tuNgay, denNgay);
                        break;
                    case "Tác giả":
                        LoadTacGiaChiTiet(trangThai, tuNgay, denNgay);
                        break;
                    case "Thể loại sách":
                        LoadTheLoaiChiTiet(trangThai, tuNgay, denNgay);
                        break;
                    case "Sách":
                        LoadSachChiTiet(trangThai, tuNgay, denNgay);
                        break;
                    case "Phiếu mượn":
                        LoadPhieuMuonChiTiet(trangThai, tuNgay, denNgay);
                        break;
                    case "Độc giả":
                        LoadDocGiaChiTiet(tuNgay, denNgay);
                        break;
                    default:
                        LoadNhaXuatBanChiTiet(trangThai, tuNgay, denNgay);
                        break;
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi tải dữ liệu thống kê: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void LoadNhaXuatBanChiTiet(string trangThai, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                var nhaXuatBans = nhaXuatBanBLL.SelectNhaXuatBan() ?? new List<NhaXuatBan>();
                var sachs = sachBLL.SelectSach() ?? new List<Sach>();
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();
                var chiTiets = chiTietMuonBLL.SelectChiTietMuon() ?? new List<ChiTietMuon>();

                // Lọc phiếu mượn theo ngày
                var phieusFilter = phieus.Where(p => p.NgayMuon >= tuNgay && p.NgayMuon <= denNgay).ToList();

                // Tạo danh sách chi tiết: Nhà xuất bản -> Sách -> Độc giả mượn
                var result = new List<dynamic>();

                foreach (var nxb in nhaXuatBans)
                {
                    var sachOfNXB = sachs.Where(s => s.MaNhaXuatBan == nxb.MaNhaXuatBan).ToList();

                    foreach (var sach in sachOfNXB)
                    {
                        // Tìm các lần mượn sách này
                        var chiTietsOfSach = chiTiets.Where(ct => ct.MaSach == sach.MaSach).ToList();

                        foreach (var chiTiet in chiTietsOfSach)
                        {
                            var phieu = phieusFilter.FirstOrDefault(p => p.MaPhieuMuon == chiTiet.MaPhieuMuon);

                            if (phieu != null)
                            {
                                // Lọc theo trạng thái
                                if (trangThai != "Tất cả" && !string.Equals(chiTiet.TrangThai, trangThai, StringComparison.OrdinalIgnoreCase))
                                    continue;

                                var docGia = docGiaBLL.SelectDocGia()?.FirstOrDefault(d => d.MaDocGia == phieu.MaDocGia);

                                result.Add(new
                                {
                                    NhaXuatBan = nxb.TenNhaXuatBan,
                                    TenSach = sach.TenSach,
                                    TenDocGia = docGia?.TenDocGia ?? "N/A",
                                    NgayMuon = phieu.NgayMuon.ToString("dd/MM/yyyy"),
                                    NgayTra = chiTiet.NgayTra.ToString("dd/MM/yyyy"),
                                    SoLuong = chiTiet.SoLuong,
                                    TrangThai = chiTiet.TrangThai
                                });
                            }
                        }
                    }
                }

                dataGridView1.DataSource = result;

                // Đặt header cho các cột
                if (dataGridView1.Columns.Count > 0)
                {
                    SetColumnHeader("NhaXuatBan", "Nhà Xuất Bản");
                    SetColumnHeader("TenSach", "Tên Sách");
                    SetColumnHeader("TenDocGia", "Độc Giả");
                    SetColumnHeader("NgayMuon", "Ngày Mượn");
                    SetColumnHeader("NgayTra", "Ngày Trả");
                    SetColumnHeader("SoLuong", "SL");
                    SetColumnHeader("TrangThai", "Trạng Thái");
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void LoadTacGiaChiTiet(string trangThai, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                var tacGias = tacGiaBLL.SelectTacGia() ?? new List<TacGia>();
                var sachs = sachBLL.SelectSach() ?? new List<Sach>();
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();
                var chiTiets = chiTietMuonBLL.SelectChiTietMuon() ?? new List<ChiTietMuon>();

                // Lọc phiếu mượn theo ngày
                var phieusFilter = phieus.Where(p => p.NgayMuon >= tuNgay && p.NgayMuon <= denNgay).ToList();

                // Tạo danh sách chi tiết: Tác giả -> Sách -> Độc giả mượn
                var result = new List<dynamic>();

                foreach (var tacGia in tacGias)
                {
                    var sachOfTacGia = sachs.Where(s => s.MaTacGia == tacGia.MaTacGia).ToList();

                    foreach (var sach in sachOfTacGia)
                    {
                        // Tìm các lần mượn sách này
                        var chiTietsOfSach = chiTiets.Where(ct => ct.MaSach == sach.MaSach).ToList();

                        foreach (var chiTiet in chiTietsOfSach)
                        {
                            var phieu = phieusFilter.FirstOrDefault(p => p.MaPhieuMuon == chiTiet.MaPhieuMuon);

                            if (phieu != null)
                            {
                                // Lọc theo trạng thái
                                if (trangThai != "Tất cả" && !string.Equals(chiTiet.TrangThai, trangThai, StringComparison.OrdinalIgnoreCase))
                                    continue;

                                var docGia = docGiaBLL.SelectDocGia()?.FirstOrDefault(d => d.MaDocGia == phieu.MaDocGia);

                                result.Add(new
                                {
                                    TacGia = tacGia.TenTacGia,
                                    TenSach = sach.TenSach,
                                    TenDocGia = docGia?.TenDocGia ?? "N/A",
                                    NgayMuon = phieu.NgayMuon.ToString("dd/MM/yyyy"),
                                    NgayTra = chiTiet.NgayTra.ToString("dd/MM/yyyy"),
                                    SoLuong = chiTiet.SoLuong,
                                    TrangThai = chiTiet.TrangThai
                                });
                            }
                        }
                    }
                }

                dataGridView1.DataSource = result;

                // Đặt header cho các cột
                if (dataGridView1.Columns.Count > 0)
                {
                    SetColumnHeader("TacGia", "Tác Giả");
                    SetColumnHeader("TenSach", "Tên Sách");
                    SetColumnHeader("TenDocGia", "Độc Giả");
                    SetColumnHeader("NgayMuon", "Ngày Mượn");
                    SetColumnHeader("NgayTra", "Ngày Trả");
                    SetColumnHeader("SoLuong", "SL");
                    SetColumnHeader("TrangThai", "Trạng Thái");
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void LoadTheLoaiChiTiet(string trangThai, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                var theLoais = theLoaiBLL.GetTheLoai() ?? new List<TheLoai>();
                var sachs = sachBLL.SelectSach() ?? new List<Sach>();
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();
                var chiTiets = chiTietMuonBLL.SelectChiTietMuon() ?? new List<ChiTietMuon>();

                // Lọc phiếu mượn theo ngày
                var phieusFilter = phieus.Where(p => p.NgayMuon >= tuNgay && p.NgayMuon <= denNgay).ToList();

                // Tạo danh sách chi tiết: Thể loại -> Sách -> Độc giả mượn
                var result = new List<dynamic>();

                foreach (var theLoai in theLoais)
                {
                    var sachOfTheLoai = sachs.Where(s => s.MaTheLoai == theLoai.MaTheLoai).ToList();

                    foreach (var sach in sachOfTheLoai)
                    {
                        // Tìm các lần mượn sách này
                        var chiTietsOfSach = chiTiets.Where(ct => ct.MaSach == sach.MaSach).ToList();

                        foreach (var chiTiet in chiTietsOfSach)
                        {
                            var phieu = phieusFilter.FirstOrDefault(p => p.MaPhieuMuon == chiTiet.MaPhieuMuon);

                            if (phieu != null)
                            {
                                // Lọc theo trạng thái
                                if (trangThai != "Tất cả" && !string.Equals(chiTiet.TrangThai, trangThai, StringComparison.OrdinalIgnoreCase))
                                    continue;

                                var docGia = docGiaBLL.SelectDocGia()?.FirstOrDefault(d => d.MaDocGia == phieu.MaDocGia);

                                result.Add(new
                                {
                                    TheLoai = theLoai.TenTheLoai,
                                    TenSach = sach.TenSach,
                                    TenDocGia = docGia?.TenDocGia ?? "N/A",
                                    NgayMuon = phieu.NgayMuon.ToString("dd/MM/yyyy"),
                                    NgayTra = chiTiet.NgayTra.ToString("dd/MM/yyyy"),
                                    SoLuong = chiTiet.SoLuong,
                                    TrangThai = chiTiet.TrangThai
                                });
                            }
                        }
                    }
                }

                dataGridView1.DataSource = result;

                // Đặt header cho các cột
                if (dataGridView1.Columns.Count > 0)
                {
                    SetColumnHeader("TheLoai", "Thể Loại");
                    SetColumnHeader("TenSach", "Tên Sách");
                    SetColumnHeader("TenDocGia", "Độc Giả");
                    SetColumnHeader("NgayMuon", "Ngày Mượn");
                    SetColumnHeader("NgayTra", "Ngày Trả");
                    SetColumnHeader("SoLuong", "SL");
                    SetColumnHeader("TrangThai", "Trạng Thái");
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void LoadSachChiTiet(string trangThai, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                var sachs = sachBLL.SelectSach() ?? new List<Sach>();
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();
                var chiTiets = chiTietMuonBLL.SelectChiTietMuon() ?? new List<ChiTietMuon>();

                var phieusFilter = phieus.Where(p => p.NgayMuon >= tuNgay && p.NgayMuon <= denNgay).ToList();

                var result = new List<dynamic>();

                foreach (var sach in sachs)
                {
                    var chiTietsOfSach = chiTiets.Where(ct => ct.MaSach == sach.MaSach).ToList();

                    foreach (var chiTiet in chiTietsOfSach)
                    {
                        var phieu = phieusFilter.FirstOrDefault(p => p.MaPhieuMuon == chiTiet.MaPhieuMuon);

                        if (phieu != null)
                        {
                            if (trangThai != "Tất cả" && !string.Equals(chiTiet.TrangThai, trangThai, StringComparison.OrdinalIgnoreCase))
                                continue;

                            var docGia = docGiaBLL.SelectDocGia()?.FirstOrDefault(d => d.MaDocGia == phieu.MaDocGia);

                            result.Add(new
                            {
                                TenSach = sach.TenSach,
                                TenDocGia = docGia?.TenDocGia ?? "N/A",
                                NgayMuon = phieu.NgayMuon.ToString("dd/MM/yyyy"),
                                NgayTra = chiTiet.NgayTra.ToString("dd/MM/yyyy"),
                                SoLuong = chiTiet.SoLuong,
                                TrangThai = chiTiet.TrangThai
                            });
                        }
                    }
                }

                dataGridView1.DataSource = result;

                if (dataGridView1.Columns.Count > 0)
                {
                    SetColumnHeader("TenSach", "Tên Sách");
                    SetColumnHeader("TenDocGia", "Độc Giả");
                    SetColumnHeader("NgayMuon", "Ngày Mượn");
                    SetColumnHeader("NgayTra", "Ngày Trả");
                    SetColumnHeader("SoLuong", "SL");
                    SetColumnHeader("TrangThai", "Trạng Thái");
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void LoadPhieuMuonChiTiet(string trangThai, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();
                var chiTiets = chiTietMuonBLL.SelectChiTietMuon() ?? new List<ChiTietMuon>();
                var sachs = sachBLL.SelectSach() ?? new List<Sach>();

                var phieusFilter = phieus.Where(p => p.NgayMuon >= tuNgay && p.NgayMuon <= denNgay).ToList();

                var result = new List<dynamic>();

                foreach (var phieu in phieusFilter)
                {
                    var chiTietsOfPhieu = chiTiets.Where(ct => ct.MaPhieuMuon == phieu.MaPhieuMuon).ToList();

                    foreach (var chiTiet in chiTietsOfPhieu)
                    {
                        if (trangThai != "Tất cả" && !string.Equals(chiTiet.TrangThai, trangThai, StringComparison.OrdinalIgnoreCase))
                            continue;

                        var docGia = docGiaBLL.SelectDocGia()?.FirstOrDefault(d => d.MaDocGia == phieu.MaDocGia);
                        var sach = sachs.FirstOrDefault(s => s.MaSach == chiTiet.MaSach);

                        result.Add(new
                        {
                            MaPhieu = phieu.MaPhieuMuon,
                            TenDocGia = docGia?.TenDocGia ?? "N/A",
                            TenSach = sach?.TenSach ?? "N/A",
                            NgayMuon = phieu.NgayMuon.ToString("dd/MM/yyyy"),
                            NgayTra = chiTiet.NgayTra.ToString("dd/MM/yyyy"),
                            SoLuong = chiTiet.SoLuong,
                            TrangThai = chiTiet.TrangThai
                        });
                    }
                }

                dataGridView1.DataSource = result;

                if (dataGridView1.Columns.Count > 0)
                {
                    SetColumnHeader("MaPhieu", "Mã Phiếu");
                    SetColumnHeader("TenDocGia", "Độc Giả");
                    SetColumnHeader("TenSach", "Tên Sách");
                    SetColumnHeader("NgayMuon", "Ngày Mượn");
                    SetColumnHeader("NgayTra", "Ngày Trả");
                    SetColumnHeader("SoLuong", "SL");
                    SetColumnHeader("TrangThai", "Trạng Thái");
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void LoadDocGiaChiTiet(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                var docGias = docGiaBLL.SelectDocGia() ?? new List<DocGia>();
                var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();
                var chiTiets = chiTietMuonBLL.SelectChiTietMuon() ?? new List<ChiTietMuon>();
                var sachs = sachBLL.SelectSach() ?? new List<Sach>();

                var phieusFilter = phieus.Where(p => p.NgayMuon >= tuNgay && p.NgayMuon <= denNgay).ToList();

                var result = new List<dynamic>();

                foreach (var docGia in docGias)
                {
                    var phieusOfDocGia = phieusFilter.Where(p => p.MaDocGia == docGia.MaDocGia).ToList();

                    foreach (var phieu in phieusOfDocGia)
                    {
                        var chiTietsOfPhieu = chiTiets.Where(ct => ct.MaPhieuMuon == phieu.MaPhieuMuon).ToList();

                        foreach (var chiTiet in chiTietsOfPhieu)
                        {
                            var sach = sachs.FirstOrDefault(s => s.MaSach == chiTiet.MaSach);

                            result.Add(new
                            {
                                TenDocGia = docGia.TenDocGia,
                                LopKhoa = docGia.Lop ?? "N/A",
                                TenSach = sach?.TenSach ?? "N/A",
                                NgayMuon = phieu.NgayMuon.ToString("dd/MM/yyyy"),
                                NgayTra = chiTiet.NgayTra.ToString("dd/MM/yyyy"),
                                SoLuong = chiTiet.SoLuong,
                                TrangThai = chiTiet.TrangThai
                            });
                        }
                    }
                }

                dataGridView1.DataSource = result;

                if (dataGridView1.Columns.Count > 0)
                {
                    SetColumnHeader("TenDocGia", "Tên Độc Giả");
                    SetColumnHeader("LopKhoa", "Lớp");
                    SetColumnHeader("TenSach", "Tên Sách");
                    SetColumnHeader("NgayMuon", "Ngày Mượn");
                    SetColumnHeader("NgayTra", "Ngày Trả");
                    SetColumnHeader("SoLuong", "SL");
                    SetColumnHeader("TrangThai", "Trạng Thái");
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void SetColumnHeader(string columnName, string headerText)
        {
            if (dataGridView1.Columns.Contains(columnName))
                dataGridView1.Columns[columnName].HeaderText = headerText;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
