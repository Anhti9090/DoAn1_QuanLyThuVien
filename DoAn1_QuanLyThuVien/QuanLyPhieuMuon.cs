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
    public partial class QuanLyPhieuMuon : Form
    {
        PhieuMuonBLL phieuMuonBLL = new PhieuMuonBLL();
        ChiTietMuonBLL chiTietMuonBLL = new ChiTietMuonBLL();

        public QuanLyPhieuMuon()
        {
            InitializeComponent();
            this.Load += QuanLyPhieuMuon_Load;
            this.dataGridView1.CellClick += dataGridView1_CellClick;
            this.buttonXoa.Click += buttonXoa_Click;
            this.buttonSua.Click += buttonSua_Click;
        }

        private void QuanLyPhieuMuon_Load(object sender, EventArgs e)
        {
            comboBoxTrangThai.Items.Clear();
            comboBoxTrangThai.Items.AddRange(new object[] { "", "Đang mượn", "Đã trả", "Quá hạn" });
            LoadData();
        }

        public void LoadData()
        {
            dataGridView1.DataSource = phieuMuonBLL.SelectPhieuMuon();
            FormatGrid();
        }

        private void FormatGrid()
        {
            if (dataGridView1.Columns.Count == 0) return;
            if (dataGridView1.Columns.Contains("MaPhieuMuon")) dataGridView1.Columns["MaPhieuMuon"].HeaderText = "Mã Phiếu";
            if (dataGridView1.Columns.Contains("MaDocGia")) dataGridView1.Columns["MaDocGia"].HeaderText = "Mã Độc Giả";
            if (dataGridView1.Columns.Contains("MaNhanVien")) dataGridView1.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
            if (dataGridView1.Columns.Contains("NgayMuon")) dataGridView1.Columns["NgayMuon"].HeaderText = "Ngày Mượn";
            if (dataGridView1.Columns.Contains("NgayTra")) dataGridView1.Columns["NgayTra"].HeaderText = "Ngày Hẹn Trả";
            if (dataGridView1.Columns.Contains("GhiChu")) dataGridView1.Columns["GhiChu"].HeaderText = "Ghi Chú";
            if (dataGridView1.Columns.Contains("TrangThai")) dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private string MaPhieuDangChon()
        {
            if (dataGridView1.CurrentRow == null) return null;
            var v = dataGridView1.CurrentRow.Cells["MaPhieuMuon"].Value;
            return v?.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // chỉ để chọn dòng
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void buttonThem_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) { }

        private void buttonChiTiet_Click(object sender, EventArgs e)
        {
            string ma = MaPhieuDangChon();
            if (string.IsNullOrEmpty(ma))
            {
                ThongBao.Show(this, "Vui lòng chọn phiếu mượn!", ThongBaoType.Cancel);
                return;
            }
            QuanLyChiTietPhieuMuon f = new QuanLyChiTietPhieuMuon(ma);
            f.Show();
        }

        private void buttonThem_Click_1(object sender, EventArgs e)
        {
            QuanLyThemSuaPhieuMuon f = new QuanLyThemSuaPhieuMuon();
            f.FormClosed += (s, ev) => LoadData();
            f.Show();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            string ma = MaPhieuDangChon();
            if (string.IsNullOrEmpty(ma))
            {
                ThongBao.Show(this, "Vui lòng chọn phiếu mượn!", ThongBaoType.Cancel);
                return;
            }
            QuanLyThemSuaPhieuMuon f = new QuanLyThemSuaPhieuMuon(ma);
            f.FormClosed += (s, ev) => LoadData();
            f.Show();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            string ma = MaPhieuDangChon();
            if (string.IsNullOrEmpty(ma))
            {
                ThongBao.Show(this, "Vui lòng chọn phiếu mượn!", ThongBaoType.Cancel);
                return;
            }
            // GIỮ NGUYÊN MessageBox YesNo (toast không xác nhận được)
            if (MessageBox.Show("Xoá phiếu " + ma + " và toàn bộ chi tiết?", "Xác nhận",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            chiTietMuonBLL.DeleteByMaPhieu(ma);
            phieuMuonBLL.DeletePhieuMuon(ma);
            ThongBao.Show(this, "Đã xoá phiếu " + ma, ThongBaoType.Success);
            LoadData();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            string maDG = textBoxDocGia.Text.Trim();
            string maPhieu = textBoxMaPhieu.Text.Trim();
            string trangThai = comboBoxTrangThai.SelectedItem?.ToString() ?? "";
            DateTime tu = dateTimePickerTuNgay.Value.Date;
            DateTime den = dateTimePickerDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            var list = phieuMuonBLL.SelectPhieuMuon();
            list = list.Where(p =>
                (string.IsNullOrEmpty(maDG) || (p.MaDocGia ?? "").IndexOf(maDG, StringComparison.OrdinalIgnoreCase) >= 0) &&
                (string.IsNullOrEmpty(maPhieu) || (p.MaPhieuMuon ?? "").IndexOf(maPhieu, StringComparison.OrdinalIgnoreCase) >= 0) &&
                (string.IsNullOrEmpty(trangThai) || (p.TrangThai ?? "").Equals(trangThai, StringComparison.OrdinalIgnoreCase)) &&
                (p.NgayMuon >= tu && p.NgayMuon <= den)
            ).ToList();

            dataGridView1.DataSource = list;
            FormatGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonXoa_Click_1(object sender, EventArgs e)
        {

        }
    }
}
