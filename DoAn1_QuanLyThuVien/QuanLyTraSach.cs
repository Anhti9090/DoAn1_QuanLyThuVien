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
    public partial class QuanLyTraSach : Form
    {
        // Đơn giá phạt cho mỗi ngày trễ trên mỗi cuốn
        private const decimal TIEN_PHAT_MOI_NGAY = 5000m;

        PhieuMuonBLL phieuMuonBLL = new PhieuMuonBLL();
        ChiTietMuonBLL chiTietMuonBLL = new ChiTietMuonBLL();
        DocGiaBLL docGiaBLL = new DocGiaBLL();
        SachBLL sachBLL = new SachBLL();

        private PhieuMuon phieuHienTai = null;
        private DocGia docGiaHienTai = null;
        private List<ChiTietMuon> dsChiTiet = new List<ChiTietMuon>();
        private List<Sach> dsSach = new List<Sach>();

        public QuanLyTraSach()
        {
            InitializeComponent();
            this.Load += QuanLyTraSach_Load;
            this.buttonThoat.Click += (s, e) => this.Close();
        }

        private void QuanLyTraSach_Load(object sender, EventArgs e)
        {
            dsSach = sachBLL.SelectSach();
            dateTimePickerNgayTra.Value = DateTime.Now;
            ClearForm();
        }

        private void ClearForm()
        {
            phieuHienTai = null;
            docGiaHienTai = null;
            dsChiTiet = new List<ChiTietMuon>();

            textBoxMP.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            textBox10.Clear(); // Trạng thái
            textBox9.Clear();  // Số lượng sách
            textBox8.Clear();  // Đã trả
            textBox7.Clear();  // Chưa trả

            labelMDG.Text = "";
            labelTDG.Text = "";
            labelNS.Text = "";
            labelSDT.Text = "";

            dataGridView1.DataSource = null;
        }

        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            textBoxMaPhieu.Clear();
            ClearForm();
            dateTimePickerNgayTra.Value = DateTime.Now;
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            string ma = textBoxMaPhieu.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                ThongBao.Show(this, "Vui lòng nhập mã phiếu mượn!", ThongBaoType.Cancel);
                return;
            }

            phieuHienTai = phieuMuonBLL.SelectPhieuMuon().FirstOrDefault(p => p.MaPhieuMuon == ma);
            if (phieuHienTai == null)
            {
                ThongBao.Show(this, "Không tìm thấy phiếu mượn!", ThongBaoType.Error);
                ClearForm();
                return;
            }

            docGiaHienTai = docGiaBLL.SelectDocGia().FirstOrDefault(d => d.MaDocGia == phieuHienTai.MaDocGia);
            dsChiTiet = chiTietMuonBLL.SelectByMaPhieu(phieuHienTai.MaPhieuMuon);

            HienThiThongTinPhieu();
            HienThiThongTinDocGia();
            HienThiTomTat();
            BindGridChuaTra();
        }

        private void HienThiThongTinPhieu()
        {
            textBoxMP.Text = phieuHienTai.MaPhieuMuon;
            textBox2.Text = phieuHienTai.MaDocGia;
            textBox3.Text = docGiaHienTai != null ? docGiaHienTai.TenDocGia : "";
            textBox4.Text = phieuHienTai.NgayMuon.ToString("dd/MM/yyyy");
            textBox5.Text = phieuHienTai.NgayTra.ToString("dd/MM/yyyy");
        }

        private void HienThiThongTinDocGia()
        {
            if (docGiaHienTai == null)
            {
                labelMDG.Text = labelTDG.Text = labelNS.Text = labelSDT.Text = "";
                return;
            }
            labelMDG.Text = docGiaHienTai.MaDocGia;
            labelTDG.Text = docGiaHienTai.TenDocGia;
            labelNS.Text = docGiaHienTai.NgaySinh.ToString("dd/MM/yyyy");
            labelSDT.Text = docGiaHienTai.SoDienThoai;
        }

        private void HienThiTomTat()
        {
            textBox10.Text = phieuHienTai.TrangThai;
            int tongSL = dsChiTiet.Sum(c => c.SoLuong);
            int daTra = dsChiTiet.Where(c => (c.TrangThai ?? "") == "Đã trả").Sum(c => c.SoLuong);
            int chuaTra = tongSL - daTra;
            textBox9.Text = tongSL.ToString();
            textBox8.Text = daTra.ToString();
            textBox7.Text = chuaTra.ToString();
        }

        private void BindGridChuaTra()
        {
            DateTime hanTra = phieuHienTai.NgayTra;
            DateTime ngayTraTT = dateTimePickerNgayTra.Value.Date;
            int soNgayTre = (ngayTraTT - hanTra.Date).Days;
            if (soNgayTre < 0) soNgayTre = 0;

            var view = (from ct in dsChiTiet
                        where (ct.TrangThai ?? "") != "Đã trả"
                        join s in dsSach on ct.MaSach equals s.MaSach into gj
                        from s in gj.DefaultIfEmpty()
                        select new TraSach
                        {
                            Chon = false,
                            MaSach = ct.MaSach,
                            TenSach = s != null ? s.TenSach : "",
                            TenTacGia = s != null ? s.TenTacGia : "",
                            SoLuong = ct.SoLuong,
                            SoNgayTre = soNgayTre,
                            TienPhat = soNgayTre * ct.SoLuong * TIEN_PHAT_MOI_NGAY
                        }).ToList();

            dataGridView1.DataSource = view;
            if (dataGridView1.Columns.Count == 0) return;
            dataGridView1.Columns["Chon"].HeaderText = "Chọn";
            dataGridView1.Columns["MaSach"].HeaderText = "Mã Sách";
            dataGridView1.Columns["TenSach"].HeaderText = "Tên Sách";
            dataGridView1.Columns["TenTacGia"].HeaderText = "Tác Giả";
            dataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng";
            dataGridView1.Columns["SoNgayTre"].HeaderText = "Số Ngày Trễ";
            dataGridView1.Columns["TienPhat"].HeaderText = "Tiền Phạt";
            dataGridView1.Columns["TienPhat"].DefaultCellStyle.Format = "N0";
        }

        private void buttonTraSach_Click(object sender, EventArgs e)
        {
            if (phieuHienTai == null)
            {
                ThongBao.Show(this, "Vui lòng tìm phiếu mượn trước!", ThongBaoType.Cancel);
                return;
            }

            var rows = dataGridView1.DataSource as List<TraSach>;
            if (rows == null || rows.Count == 0)
            {
                ThongBao.Show(this, "Không có sách nào cần trả!", ThongBaoType.Cancel);
                return;
            }

            var chon = rows.Where(r => r.Chon).ToList();
            if (chon.Count == 0)
            {
                ThongBao.Show(this, "Vui lòng chọn ít nhất một sách để trả!", ThongBaoType.Cancel);
                return;
            }

            DateTime ngayTraTT = dateTimePickerNgayTra.Value.Date;
            if (ngayTraTT < phieuHienTai.NgayMuon.Date)
            {
                ThongBao.Show(this, "Ngày trả không được nhỏ hơn ngày mượn!", ThongBaoType.Cancel);
                return;
            }

            try
            {
                foreach (var r in chon)
                {
                    var ct = dsChiTiet.FirstOrDefault(c => c.MaSach == r.MaSach);
                    if (ct == null) continue;

                    ct.NgayTra = ngayTraTT;
                    ct.TienPhat = r.TienPhat;
                    ct.TrangThai = "Đã trả";
                    chiTietMuonBLL.UpdateChiTietMuon(ct);

                    // Cộng lại số lượng sách còn trong kho (chặn tràn TongSoLuong)
                    var sach = dsSach.FirstOrDefault(s => s.MaSach == ct.MaSach);
                    if (sach != null)
                    {
                        int moi = sach.SoLuongCon + ct.SoLuong;
                        if (moi > sach.TongSoLuong) moi = sach.TongSoLuong;
                        sach.SoLuongCon = moi;
                        sachBLL.UpdateSach(sach);
                    }
                }

                // Cập nhật trạng thái phiếu
                bool conChuaTra = dsChiTiet.Any(c => (c.TrangThai ?? "") != "Đã trả");
                phieuHienTai.TrangThai = conChuaTra ? "Đang mượn" : "Đã trả";
                phieuMuonBLL.UpdatePhieuMuon(phieuHienTai);

                ThongBao.Show(this, "Trả sách thành công!", ThongBaoType.Success);

                dsChiTiet = chiTietMuonBLL.SelectByMaPhieu(phieuHienTai.MaPhieuMuon);
                HienThiTomTat();
                BindGridChuaTra();
                textBox10.Text = phieuHienTai.TrangThai;
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi trả sách: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonInBienNhan_Click(object sender, EventArgs e)
        {
            if (phieuHienTai == null)
            {
                ThongBao.Show(this, "Chưa có phiếu để in!", ThongBaoType.Cancel);
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("===== BIÊN NHẬN TRẢ SÁCH =====");
            sb.AppendLine("Mã phiếu : " + phieuHienTai.MaPhieuMuon);
            sb.AppendLine("Độc giả  : " + (docGiaHienTai != null ? docGiaHienTai.TenDocGia : phieuHienTai.MaDocGia));
            sb.AppendLine("Ngày trả : " + dateTimePickerNgayTra.Value.ToString("dd/MM/yyyy"));
            sb.AppendLine("------------------------------");
            decimal tongPhat = 0;
            foreach (var ct in dsChiTiet.Where(c => (c.TrangThai ?? "") == "Đã trả"))
            {
                var s = dsSach.FirstOrDefault(x => x.MaSach == ct.MaSach);
                sb.AppendLine(string.Format("- {0} ({1}) x{2}  Phạt: {3:N0}",
                    s != null ? s.TenSach : ct.MaSach, ct.MaSach, ct.SoLuong, ct.TienPhat));
                tongPhat += ct.TienPhat;
            }
            sb.AppendLine("------------------------------");
            sb.AppendLine("Tổng tiền phạt: " + tongPhat.ToString("N0"));

            // GIỮ MessageBox vì cần hiển thị nội dung biên nhận dài
            MessageBox.Show(sb.ToString(), "Biên nhận");
        }

    }
}
