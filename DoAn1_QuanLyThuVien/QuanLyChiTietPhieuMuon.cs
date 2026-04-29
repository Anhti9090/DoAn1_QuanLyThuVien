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
    public partial class QuanLyChiTietPhieuMuon : Form
    {
        PhieuMuonBLL phieuMuonBLL = new PhieuMuonBLL();
        ChiTietMuonBLL chiTietMuonBLL = new ChiTietMuonBLL();
        DocGiaBLL docGiaBLL = new DocGiaBLL();

        private string maPhieu;

        public QuanLyChiTietPhieuMuon()
        {
            InitializeComponent();
            this.buttonDong.Click += (s, e) => this.Close();
        }

        public QuanLyChiTietPhieuMuon(string maPhieu) : this()
        {
            this.maPhieu = maPhieu;
            this.Load += QuanLyChiTietPhieuMuon_Load;
        }

        private void QuanLyChiTietPhieuMuon_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maPhieu)) return;

            var pm = phieuMuonBLL.SelectPhieuMuon().FirstOrDefault(p => p.MaPhieuMuon == maPhieu);
            if (pm == null)
            {
                ThongBao.Show(this, "Không tìm thấy phiếu mượn!", ThongBaoType.Error);
                return;
            }

            // Thông tin phiếu
            textBoxMaPM.Text = pm.MaPhieuMuon;
            textBoxMaPM.ReadOnly = true;
            dateTimePicker1.Value = pm.NgayMuon;
            dateTimePicker2.Value = pm.NgayTra == DateTime.MinValue ? DateTime.Now : pm.NgayTra;
            textBox1.Text = pm.TrangThai;
            dateTimePicker4.Value = pm.NgayMuon;

            // Thông tin độc giả
            var dg = docGiaBLL.SelectDocGia().FirstOrDefault(d => d.MaDocGia == pm.MaDocGia);
            if (dg != null)
            {
                labelMDG.Text = dg.MaDocGia;
                labelTDG.Text = dg.TenDocGia;
                labelNS.Text = dg.NgaySinh.ToString("dd/MM/yyyy");
                labelSDT.Text = dg.SoDienThoai;
                labelE.Text = dg.Email;
                labelNDK.Text = dg.NgayLapThe.ToString("dd/MM/yyyy");
                labelTT.Text = dg.TrangThai;
                textBoxDC.Text = dg.DiaChi;
            }

            // Chi tiết
            var dsCT = chiTietMuonBLL.SelectByMaPhieu(maPhieu);
            dataGridView1.DataSource = dsCT;
            if (dataGridView1.Columns.Contains("MaPhieuMuon")) dataGridView1.Columns["MaPhieuMuon"].Visible = false;
            if (dataGridView1.Columns.Contains("MaSach")) dataGridView1.Columns["MaSach"].HeaderText = "Mã Sách";
            if (dataGridView1.Columns.Contains("SoLuong")) dataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng";
            if (dataGridView1.Columns.Contains("NgayTra")) dataGridView1.Columns["NgayTra"].HeaderText = "Ngày Trả";
            if (dataGridView1.Columns.Contains("TienPhat")) dataGridView1.Columns["TienPhat"].HeaderText = "Tiền Phạt";
            if (dataGridView1.Columns.Contains("TrangThai")) dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";

            // Tóm tắt
            int tongDauSach = dsCT.Select(c => c.MaSach).Distinct().Count();
            int tongSL = dsCT.Sum(c => c.SoLuong);
            int daTra = dsCT.Where(c => (c.TrangThai ?? "") == "Đã trả").Sum(c => c.SoLuong);
            int chuaTra = tongSL - daTra;
            decimal tongPhat = dsCT.Sum(c => c.TienPhat);

            textBoxTongSoDauSach.Text = tongDauSach.ToString();
            textBoxTongSoLongSach.Text = tongSL.ToString();
            textBoxSoSachDaTra.Text = daTra.ToString();
            textBoxSoSachChuaTra.Text = chuaTra.ToString();
            textBoxTongTienPhat.Text = tongPhat.ToString("N0");
        }

        private void label11_Click(object sender, EventArgs e) { }

        private void buttonDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
