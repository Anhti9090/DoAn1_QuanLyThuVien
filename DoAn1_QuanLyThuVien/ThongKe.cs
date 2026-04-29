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
            label9.Visible = false;

            comboBoxLoaiThongKe.Items.Clear();
            comboBoxLoaiThongKe.Items.AddRange(new object[] { "Phiếu mượn", "Sách" });
            comboBoxLoaiThongKe.SelectedIndex = 0;

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
                string loai = comboBoxLoaiThongKe.SelectedItem?.ToString() ?? "Phiếu mượn";
                string trangThai = comboBoxTrangThai.SelectedItem?.ToString() ?? "Tất cả";
                DateTime tuNgay = dateTimePickerTuNgay.Value.Date;
                DateTime denNgay = dateTimePickerDenNgay.Value.Date.AddDays(1).AddTicks(-1);

                if (loai == "Phiếu mượn")
                {
                    var phieus = phieuMuonBLL.SelectPhieuMuon() ?? new List<PhieuMuon>();
                    var data = phieus.Where(p => p.NgayMuon >= tuNgay && p.NgayMuon <= denNgay);
                    if (trangThai != "Tất cả")
                        data = data.Where(p => string.Equals(p.TrangThai, trangThai, StringComparison.OrdinalIgnoreCase));
                    dataGridView1.DataSource = data.ToList();
                }
                else
                {
                    dataGridView1.DataSource = sachBLL.SelectSach();
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi tải dữ liệu thống kê: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
