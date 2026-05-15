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
    public partial class QuanLyThemSuaPhieuMuon : Form
    {
        PhieuMuonBLL phieuMuonBLL = new PhieuMuonBLL();
        ChiTietMuonBLL chiTietMuonBLL = new ChiTietMuonBLL();
        DocGiaBLL docGiaBLL = new DocGiaBLL();
        NhanVienBLL nhanVienBLL = new NhanVienBLL();
        SachBLL sachBLL = new SachBLL();

        private List<ChiTietMuon> dsChiTiet = new List<ChiTietMuon>();
        private List<Sach> dsSach = new List<Sach>();
        private bool isEdit = false;
        private string maPhieuEdit = null;

        public QuanLyThemSuaPhieuMuon()
        {
            InitializeComponent();
            // remove these duplicates – they are already attached in InitializeComponent:
             this.Load += QuanLyThemSuaPhieuMuon_Load;
            // this.buttonLuu.Click += buttonLuu_Click;
            // this.buttonHuy.Click += buttonHuy_Click;

            this.buttonThoat.Click += (s, e) => this.Close();
            this.comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            // Thêm sự kiện tìm kiếm khi nhấn Enter
            this.textBoxTimKiem.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    buttonTimKiem_Click(s, e);
                    e.Handled = true;
                }
            };
        }

        public QuanLyThemSuaPhieuMuon(string maPhieu) : this()
        {
            isEdit = true;
            maPhieuEdit = maPhieu;
        }

        private void QuanLyThemSuaPhieuMuon_Load(object sender, EventArgs e)
        {
            // ComboBox độc giả
            comboBox2.DataSource = docGiaBLL.SelectDocGia();
            comboBox2.DisplayMember = "MaTenDocGia";  // Hiển thị "Mã - Tên"
            comboBox2.ValueMember = "MaDocGia";

            // ComboBox nhân viên
            comboBoxNhanVien.DataSource = nhanVienBLL.SelectNhanVien();
            comboBoxNhanVien.DisplayMember = "MaTenNhanVien";  // Hiển thị "Mã - Tên"
            comboBoxNhanVien.ValueMember = "MaNhanVien";

            // ComboBox sách (cache để tra cứu nhanh)
            dsSach = sachBLL.SelectSach();
            BindDataGridView2();

            if (isEdit && !string.IsNullOrEmpty(maPhieuEdit))
            {
                LoadEdit();
            }
            else
            {
                textBoxMaPM.Text = phieuMuonBLL.TaoMaPhieuMoi();
                textBoxMaPM.ReadOnly = true;
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now.AddDays(7);
            }

            BindGrid();
        }

        private void LoadEdit()
        {
            var pm = phieuMuonBLL.SelectPhieuMuon().FirstOrDefault(p => p.MaPhieuMuon == maPhieuEdit);
            if (pm == null) return;
            textBoxMaPM.Text = pm.MaPhieuMuon;
            textBoxMaPM.ReadOnly = true;
            comboBox2.SelectedValue = pm.MaDocGia;
            comboBoxNhanVien.SelectedValue = pm.MaNhanVien;
            dateTimePicker1.Value = pm.NgayMuon;
            dateTimePicker2.Value = pm.NgayTra == DateTime.MinValue ? DateTime.Now : pm.NgayTra;
            textBox1.Text = pm.GhiChu;
            dsChiTiet = chiTietMuonBLL.SelectByMaPhieu(maPhieuEdit);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dg = comboBox2.SelectedItem as DocGia;
            if (dg == null) return;
            textBoxHoTen.Text = dg.TenDocGia;
            textBoxDiaChi.Text = dg.DiaChi;
            textBoxSDT.Text = dg.SoDienThoai;
        }

        private void BindDataGridView2()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = phieuMuonBLL.GetSachView(dsSach);

            if (dataGridView2.Columns.Count == 0) return;
            dataGridView2.Columns["MaSach"].HeaderText = "Mã Sách";
            dataGridView2.Columns["TenSach"].HeaderText = "Tên Sách";
            dataGridView2.Columns["TenTheLoai"].HeaderText = "Thể Loại";
            dataGridView2.Columns["SoLuongCon"].HeaderText = "Tồn Kho";
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;
        }

        private void BindGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = phieuMuonBLL.GetChiTietView(dsChiTiet, dsSach);

            if (dataGridView1.Columns.Count == 0) return;
            dataGridView1.Columns["MaSach"].HeaderText = "Mã Sách";
            dataGridView1.Columns["TenSach"].HeaderText = "Tên Sách";
            dataGridView1.Columns["TenTheLoai"].HeaderText = "Thể Loại";
            dataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private void buttonThemDS_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void textBoxMaPM_TextChanged(object sender, EventArgs e) { }

        private void buttonThemDS_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                ThongBao.Show(this, "Vui lòng chọn sách!", ThongBaoType.Cancel);
                return;
            }

            var row = dataGridView2.SelectedRows[0];
            string maSach = row.Cells["MaSach"].Value?.ToString();
            if (string.IsNullOrEmpty(maSach)) return;

            var sach = dsSach.FirstOrDefault(s => s.MaSach == maSach);
            if (sach == null) return;

            int soLuong = (int)numericUpDownSoLuong.Value;

            if (sach.SoLuongCon <= 0)
            {
                ThongBao.Show(this, string.Format("Sách \"{0}\" đã hết!", sach.TenSach), ThongBaoType.Cancel);
                return;
            }

            var existing = dsChiTiet.FirstOrDefault(c => c.MaSach == maSach);
            if (existing != null)
            {
                if (existing.SoLuong + soLuong > sach.SoLuongCon)
                {
                    ThongBao.Show(this, string.Format("Sách \"{0}\" chỉ còn {1} cuốn!", sach.TenSach, sach.SoLuongCon), ThongBaoType.Cancel);
                    return;
                }
                existing.SoLuong += soLuong;
            }
            else
            {
                if (soLuong > sach.SoLuongCon)
                {
                    ThongBao.Show(this, string.Format("Sách \"{0}\" chỉ còn {1} cuốn!", sach.TenSach, sach.SoLuongCon), ThongBaoType.Cancel);
                    return;
                }
                dsChiTiet.Add(new ChiTietMuon
                {
                    MaPhieuMuon = textBoxMaPM.Text,
                    MaSach = maSach,
                    SoLuong = soLuong,
                    NgayTra = DateTime.MinValue,
                    TienPhat = 0,
                    TrangThai = "Chưa trả"
                });
            }
            numericUpDownSoLuong.Value = 1;
            BindGrid();
        }

        private void buttonLuu_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue == null)
            {
                ThongBao.Show(this, "Vui lòng chọn độc giả!", ThongBaoType.Cancel);
                return;
            }
            if (comboBoxNhanVien.SelectedValue == null)
            {
                ThongBao.Show(this, "Vui lòng chọn nhân viên!", ThongBaoType.Cancel);
                return;
            }
            if (dsChiTiet.Count == 0)
            {
                ThongBao.Show(this, "Phải có ít nhất 1 sách!", ThongBaoType.Cancel);
                return;
            }

            PhieuMuon pm = new PhieuMuon
            {
                MaPhieuMuon = textBoxMaPM.Text.Trim(),
                MaDocGia = comboBox2.SelectedValue.ToString(),
                MaNhanVien = comboBoxNhanVien.SelectedValue.ToString(),
                NgayMuon = dateTimePicker1.Value,
                NgayTra = dateTimePicker2.Value,
                GhiChu = textBox1.Text,
                TrangThai = "Đang mượn"
            };

            try
            {
                foreach (var ct in dsChiTiet)
                {
                    var s = dsSach.FirstOrDefault(x => x.MaSach == ct.MaSach);
                    if (s == null)
                    {
                        ThongBao.Show(this, "Không tìm thấy sách " + ct.MaSach, ThongBaoType.Error);
                        return;
                    }
                    int dangMuon = 0;
                    if (isEdit)
                    {
                        var ctCu = chiTietMuonBLL.SelectByMaPhieu(pm.MaPhieuMuon)
                                                 .FirstOrDefault(c => c.MaSach == ct.MaSach);
                        if (ctCu != null) dangMuon = ctCu.SoLuong;
                    }
                    if (ct.SoLuong - dangMuon > s.SoLuongCon)
                    {
                        ThongBao.Show(this,
                            string.Format("Sách \"{0}\" chỉ còn {1} cuốn, không đủ để mượn {2}!",
                                s.TenSach, s.SoLuongCon + dangMuon, ct.SoLuong),
                            ThongBaoType.Cancel);
                        return;
                    }
                }

                if (isEdit)
                {
                    // Hoàn lại tồn kho của các chi tiết cũ rồi xoá
                    var ctCuList = chiTietMuonBLL.SelectByMaPhieu(pm.MaPhieuMuon);
                    foreach (var ctCu in ctCuList)
                    {
                        if ((ctCu.TrangThai ?? "") == "Đã trả") continue; // đã hoàn rồi
                        var s = dsSach.FirstOrDefault(x => x.MaSach == ctCu.MaSach);
                        if (s != null)
                        {
                            s.SoLuongCon += ctCu.SoLuong;
                            if (s.SoLuongCon > s.TongSoLuong) s.SoLuongCon = s.TongSoLuong;
                            sachBLL.UpdateSach(s);
                        }
                    }
                    phieuMuonBLL.UpdatePhieuMuon(pm);
                    chiTietMuonBLL.DeleteByMaPhieu(pm.MaPhieuMuon);
                }
                else
                {
                    phieuMuonBLL.InsertPhieuMuon(pm);
                }

                foreach (var ct in dsChiTiet)
                {
                    ct.MaPhieuMuon = pm.MaPhieuMuon;
                    chiTietMuonBLL.InsertChiTietMuon(ct);

                    // Trừ tồn kho
                    var s = dsSach.FirstOrDefault(x => x.MaSach == ct.MaSach);
                    if (s != null)
                    {
                        s.SoLuongCon -= ct.SoLuong;
                        if (s.SoLuongCon < 0) s.SoLuongCon = 0;
                        sachBLL.UpdateSach(s);
                    }
                }
                ThongBao.Show(this, "Lưu thành công!", ThongBaoType.Success);
                this.Close();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void buttonHuy_Click(object sender, EventArgs e)
        {
            dsChiTiet.Clear();
            BindGrid();
            textBox1.Clear();
        }

        private void QuanLyThemSuaPhieuMuon_Load_1(object sender, EventArgs e)
        {

        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = textBoxTimKiem.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu không có từ khóa, hiển thị tất cả sách
                BindDataGridView2();
                return;
            }

            // Lọc danh sách sách theo từ khóa
            var sachFiltered = dsSach.Where(s => 
                s.MaSach.ToLower().Contains(keyword) || 
                s.TenSach.ToLower().Contains(keyword) ||
                (s.TenTacGia != null && s.TenTacGia.ToLower().Contains(keyword)) ||
                (s.TenTheLoai != null && s.TenTheLoai.ToLower().Contains(keyword))
            ).ToList();

            // Hiển thị kết quả tìm kiếm
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = phieuMuonBLL.GetSachView(sachFiltered);

            if (dataGridView2.Columns.Count == 0) return;
            dataGridView2.Columns["MaSach"].HeaderText = "Mã Sách";
            dataGridView2.Columns["TenSach"].HeaderText = "Tên Sách";
            dataGridView2.Columns["TenTheLoai"].HeaderText = "Thể Loại";
            dataGridView2.Columns["SoLuongCon"].HeaderText = "Tồn Kho";
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;

            if (sachFiltered.Count == 0)
            {
                ThongBao.Show(this, "Không tìm thấy sách nào!", ThongBaoType.Info);
            }
        }
    }
}
