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
            comboBox2.DisplayMember = "TenDocGia";
            comboBox2.ValueMember = "MaDocGia";

            // ComboBox nhân viên
            comboBoxNhanVien.DataSource = nhanVienBLL.SelectNhanVien();
            comboBoxNhanVien.DisplayMember = "TenNhanVien";
            comboBoxNhanVien.ValueMember = "MaNhanVien";

            // ComboBox sách (cache để tra cứu nhanh)
            dsSach = sachBLL.SelectSach();
            comboBoxSach.DataSource = dsSach;
            comboBoxSach.DisplayMember = "TenSach";
            comboBoxSach.ValueMember = "MaSach";

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

        private void BindGrid()
        {
            // Join với danh sách sách để hiển thị thêm Tên sách / Tác giả / Thể loại
            var view = (from ct in dsChiTiet
                        join s in dsSach on ct.MaSach equals s.MaSach into gj
                        from s in gj.DefaultIfEmpty()
                        select new
                        {
                            ct.MaSach,
                            TenSach = s != null ? s.TenSach : "",
                            TenTacGia = s != null ? s.TenTacGia : "",
                            TenTheLoai = s != null ? s.TenTheLoai : "",
                            ct.SoLuong,
                            ct.TrangThai
                        }).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = view;

            if (dataGridView1.Columns.Count == 0) return;
            dataGridView1.Columns["MaSach"].HeaderText = "Mã Sách";
            dataGridView1.Columns["TenSach"].HeaderText = "Tên Sách";
            dataGridView1.Columns["TenTacGia"].HeaderText = "Tác Giả";
            dataGridView1.Columns["TenTheLoai"].HeaderText = "Thể Loại";
            dataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private void buttonThemDS_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void textBoxMaPM_TextChanged(object sender, EventArgs e) { }

        private void buttonThemDS_Click_1(object sender, EventArgs e)
        {
            if (comboBoxSach.SelectedValue == null)
            {
                ThongBao.Show(this, "Vui lòng chọn sách!", ThongBaoType.Cancel);
                return;
            }
            string maSach = comboBoxSach.SelectedValue.ToString();
            int sl = (int)numericUpDownSoLuong.Value;
            if (sl <= 0)
            {
                ThongBao.Show(this, "Số lượng phải > 0!", ThongBaoType.Cancel);
                return;
            }
            var existing = dsChiTiet.FirstOrDefault(c => c.MaSach == maSach);
            if (existing != null)
            {
                existing.SoLuong += sl;
            }
            else
            {
                dsChiTiet.Add(new ChiTietMuon
                {
                    MaPhieuMuon = textBoxMaPM.Text,
                    MaSach = maSach,
                    SoLuong = sl,
                    NgayTra = DateTime.MinValue,
                    TienPhat = 0,
                    TrangThai = "Chưa trả"
                });
            }
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
            numericUpDownSoLuong.Value = 1;
        }

        private void QuanLyThemSuaPhieuMuon_Load_1(object sender, EventArgs e)
        {

        }
    }
}
