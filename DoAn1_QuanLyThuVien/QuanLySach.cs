using System;
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
    public partial class QuanLySach : Form
    {
        Sach sach = new Sach();
        SachBLL sachBLL = new SachBLL();
        TacGiaBLL tacGiaBLL = new TacGiaBLL();
        TheLoaiBLL theLoaiBLL = new TheLoaiBLL();
        NhaXuatBanBLL nhaXuatBanBLL = new NhaXuatBanBLL();
        public QuanLySach()
        {
            InitializeComponent();

            // Cho phép nhập số lớn hơn mặc định (100)
            numericUpDownTongSL.Maximum = int.MaxValue;
            numericUpDownSLCon.Maximum = int.MaxValue;

            // Khi đổi Tổng số lượng -> Số lượng còn hiện lên ngay
            numericUpDownTongSL.ValueChanged += numericUpDownTongSL_ValueChanged;
        }

        private void numericUpDownTongSL_ValueChanged(object sender, EventArgs e)
        {
            // Đồng bộ: Số lượng còn = Tổng số lượng khi đang nhập mới
            numericUpDownSLCon.Value = numericUpDownTongSL.Value;
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                sach.MaSach = textBoxMaSach.Text.Trim();
                if (string.IsNullOrWhiteSpace(sach.MaSach))
                {
                    ThongBao.Show(this, "Mã sách không được để trống", ThongBaoType.Error);
                    textBoxMaSach.Focus();
                    return;
                }

                sach.TenSach = textBoxTenSach.Text.Trim();
                if (string.IsNullOrWhiteSpace(sach.TenSach))
                {
                    ThongBao.Show(this, "Tên sách không được để trống", ThongBaoType.Error);
                    textBoxTenSach.Focus();
                    return;
                }

                if (comboBoxMaTG.SelectedValue == null)
                {
                    ThongBao.Show(this, "Vui lòng chọn tác giả", ThongBaoType.Error);
                    comboBoxMaTG.Focus();
                    return;
                }

                if (comboBoxMaTheLoai.SelectedValue == null)
                {
                    ThongBao.Show(this, "Vui lòng chọn thể loại", ThongBaoType.Error);
                    comboBoxMaTheLoai.Focus();
                    return;
                }

                if (comboBoxNhXB.SelectedValue == null)
                {
                    ThongBao.Show(this, "Vui lòng chọn nhà xuất bản", ThongBaoType.Error);
                    comboBoxNhXB.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxNamXB.Text))
                {
                    ThongBao.Show(this, "Năm xuất bản không được để trống", ThongBaoType.Error);
                    textBoxNamXB.Focus();
                    return;
                }

                if (!int.TryParse(textBoxNamXB.Text, out int namXB) || namXB < 1000 || namXB > DateTime.Now.Year)
                {
                    ThongBao.Show(this, "Năm xuất bản không hợp lệ", ThongBaoType.Error);
                    textBoxNamXB.Focus();
                    return;
                }

                if (numericUpDownTongSL.Value <= 0)
                {
                    ThongBao.Show(this, "Tổng số lượng phải lớn hơn 0", ThongBaoType.Error);
                    numericUpDownTongSL.Focus();
                    return;
                }

                if (numericUpDownSLCon.Value > numericUpDownTongSL.Value)
                {
                    ThongBao.Show(this, "Số lượng còn không được lớn hơn tổng số lượng", ThongBaoType.Error);
                    numericUpDownSLCon.Focus();
                    return;
                }

                sach.MaTacGia = comboBoxMaTG.SelectedValue.ToString();
                sach.MaTheLoai = comboBoxMaTheLoai.SelectedValue.ToString();
                sach.MaNhaXuatBan = comboBoxNhXB.SelectedValue.ToString();
                sach.NamXuatBan = namXB;
                sach.TongSoLuong = (int)numericUpDownTongSL.Value;
                sach.SoLuongCon = (int)numericUpDownSLCon.Value;

                sachBLL.InsertSach(sach);
                ThongBao.Show(this, "Thêm sách thành công", ThongBaoType.Success);
                LoadData();
                ClearData();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi thêm sách: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                sach.MaSach = textBoxMaSach.Text.Trim();
                if (string.IsNullOrWhiteSpace(sach.MaSach))
                {
                    ThongBao.Show(this, "Vui lòng chọn sách cần cập nhật", ThongBaoType.Error);
                    return;
                }

                sach.TenSach = textBoxTenSach.Text.Trim();
                if (string.IsNullOrWhiteSpace(sach.TenSach))
                {
                    ThongBao.Show(this, "Tên sách không được để trống", ThongBaoType.Error);
                    textBoxTenSach.Focus();
                    return;
                }

                if (comboBoxMaTG.SelectedValue == null)
                {
                    ThongBao.Show(this, "Vui lòng chọn tác giả", ThongBaoType.Error);
                    comboBoxMaTG.Focus();
                    return;
                }

                if (comboBoxMaTheLoai.SelectedValue == null)
                {
                    ThongBao.Show(this, "Vui lòng chọn thể loại", ThongBaoType.Error);
                    comboBoxMaTheLoai.Focus();
                    return;
                }

                if (comboBoxNhXB.SelectedValue == null)
                {
                    ThongBao.Show(this, "Vui lòng chọn nhà xuất bản", ThongBaoType.Error);
                    comboBoxNhXB.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxNamXB.Text))
                {
                    ThongBao.Show(this, "Năm xuất bản không được để trống", ThongBaoType.Error);
                    textBoxNamXB.Focus();
                    return;
                }

                if (!int.TryParse(textBoxNamXB.Text, out int namXB) || namXB < 1000 || namXB > DateTime.Now.Year)
                {
                    ThongBao.Show(this, "Năm xuất bản không hợp lệ", ThongBaoType.Error);
                    textBoxNamXB.Focus();
                    return;
                }

                if (numericUpDownTongSL.Value <= 0)
                {
                    ThongBao.Show(this, "Tổng số lượng phải lớn hơn 0", ThongBaoType.Error);
                    numericUpDownTongSL.Focus();
                    return;
                }

                if (numericUpDownSLCon.Value > numericUpDownTongSL.Value)
                {
                    ThongBao.Show(this, "Số lượng còn không được lớn hơn tổng số lượng", ThongBaoType.Error);
                    numericUpDownSLCon.Focus();
                    return;
                }

                sach.MaTacGia = comboBoxMaTG.SelectedValue.ToString();
                sach.MaTheLoai = comboBoxMaTheLoai.SelectedValue.ToString();
                sach.MaNhaXuatBan = comboBoxNhXB.SelectedValue.ToString();
                sach.NamXuatBan = namXB;
                sach.TongSoLuong = (int)numericUpDownTongSL.Value;
                sach.SoLuongCon = (int)numericUpDownSLCon.Value;

                sachBLL.UpdateSach(sach);
                ThongBao.Show(this, "Cập nhật sách thành công", ThongBaoType.Success);
                LoadData();
                ClearData();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi cập nhật sách: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sach.MaSach = textBoxMaSach.Text.Trim();
                if (string.IsNullOrWhiteSpace(sach.MaSach))
                {
                    ThongBao.Show(this, "Vui lòng chọn sách cần xóa", ThongBaoType.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa sách này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    sachBLL.DeleteSach(sach.MaSach);
                    ThongBao.Show(this, "Xóa sách thành công", ThongBaoType.Success);
                    LoadData();
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi xóa sách: " + ex.Message, ThongBaoType.Error);
            }
        }
        public void LoadData()
        {
            comboBoxMaTG.DataSource = tacGiaBLL.SelectTacGia();
            comboBoxMaTG.DisplayMember = "TenTacGia";
            comboBoxMaTG.ValueMember = "MaTacGia";

            comboBoxMaTheLoai.DataSource = theLoaiBLL.GetTheLoai();
            comboBoxMaTheLoai.DisplayMember = "TenTheLoai";
            comboBoxMaTheLoai.ValueMember = "MaTheLoai";

            comboBoxNhXB.DataSource = nhaXuatBanBLL.SelectNhaXuatBan();
            comboBoxNhXB.DisplayMember = "TenNhaXuatBan";
            comboBoxNhXB.ValueMember = "MaNhaXuatBan";

            dataGridView1.DataSource = sachBLL.SelectSach();
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["MaSach"].HeaderText = "Mã Sách";
                dataGridView1.Columns["TenSach"].HeaderText = "Tên Sách";

                // Ẩn cột mã, hiện tên
                dataGridView1.Columns["MaTacGia"].Visible = false;
                dataGridView1.Columns["MaTheLoai"].Visible = false;
                if (dataGridView1.Columns.Contains("MaNhaXuatBan"))
                    dataGridView1.Columns["MaNhaXuatBan"].Visible = false;

                dataGridView1.Columns["TenTacGia"].HeaderText = "Tác Giả";
                dataGridView1.Columns["TenTheLoai"].HeaderText = "Thể Loại";

                dataGridView1.Columns["NamXuatBan"].HeaderText = "Năm Xuất Bản";
                if (dataGridView1.Columns.Contains("NhaXuatBan"))
                    dataGridView1.Columns["NhaXuatBan"].HeaderText = "Nhà Xuất Bản";
                if (dataGridView1.Columns.Contains("TenNhaXuatBan"))
                    dataGridView1.Columns["TenNhaXuatBan"].HeaderText = "Nhà Xuất Bản";

                dataGridView1.Columns["TongSoLuong"].HeaderText = "Tổng Số Lượng";
                dataGridView1.Columns["SoLuongCon"].HeaderText = "Số Lượng Còn";

                // Sắp xếp thứ tự cột (tuỳ chọn)
                dataGridView1.Columns["MaSach"].DisplayIndex = 0;
                dataGridView1.Columns["TenSach"].DisplayIndex = 1;
                dataGridView1.Columns["TenTacGia"].DisplayIndex = 2;
                dataGridView1.Columns["TenTheLoai"].DisplayIndex = 3;
                dataGridView1.Columns["NamXuatBan"].DisplayIndex = 4;
                if (dataGridView1.Columns.Contains("NhaXuatBan"))
                    dataGridView1.Columns["NhaXuatBan"].DisplayIndex = 5;
                if (dataGridView1.Columns.Contains("TenNhaXuatBan"))
                    dataGridView1.Columns["TenNhaXuatBan"].DisplayIndex = 5;
                dataGridView1.Columns["TongSoLuong"].DisplayIndex = 6;
                dataGridView1.Columns["SoLuongCon"].DisplayIndex = 7;
            }
        }
        public void ClearData()
        {
            textBoxMaSach.Clear();
            textBoxTenSach.Clear();
            comboBoxMaTG.SelectedIndex = -1;
            comboBoxMaTheLoai.SelectedIndex = -1;
            comboBoxNhXB.SelectedIndex = -1;
            textBoxNamXB.Clear();
            numericUpDownTongSL.Value = 0;
            numericUpDownSLCon.Value = 0;
            textBoxMaSach.ReadOnly = false;
            textBoxMaSach.Focus();
        }
        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearData();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = textBoxTimKiem.Text.Trim();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    LoadData();
                    return;
                }

                List<Sach> filteredList = sachBLL.SearchSach(searchText, sachBLL.SelectSach());
                dataGridView1.DataSource = filteredList;

                if (filteredList.Count == 0)
                {
                    ThongBao.Show(this, "Không tìm thấy sách nào", ThongBaoType.Info);
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi tìm kiếm: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void QuanLySach_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    textBoxMaSach.Text = row.Cells["MaSach"].Value?.ToString() ?? "";
                    textBoxTenSach.Text = row.Cells["TenSach"].Value?.ToString() ?? "";

                    // Lấy MÃ từ cột ẩn để ComboBox có thể lưu đúng khi Update/Delete
                    if (row.Cells["MaTacGia"].Value != null)
                        comboBoxMaTG.SelectedValue = row.Cells["MaTacGia"].Value.ToString();

                    if (row.Cells["MaTheLoai"].Value != null)
                        comboBoxMaTheLoai.SelectedValue = row.Cells["MaTheLoai"].Value.ToString();

                    // Xử lý cả MaNhaXuatBan hoặc NhaXuatBan
                    if (dataGridView1.Columns.Contains("MaNhaXuatBan") && row.Cells["MaNhaXuatBan"].Value != null)
                        comboBoxNhXB.SelectedValue = row.Cells["MaNhaXuatBan"].Value.ToString();
                    else if (dataGridView1.Columns.Contains("NhaXuatBan") && row.Cells["NhaXuatBan"].Value != null)
                        comboBoxNhXB.SelectedValue = row.Cells["NhaXuatBan"].Value.ToString();

                    textBoxNamXB.Text = row.Cells["NamXuatBan"].Value?.ToString() ?? "";

                    if (row.Cells["TongSoLuong"].Value != null)
                        numericUpDownTongSL.Value = Convert.ToInt32(row.Cells["TongSoLuong"].Value);

                    if (row.Cells["SoLuongCon"].Value != null)
                        numericUpDownSLCon.Value = Convert.ToInt32(row.Cells["SoLuongCon"].Value);

                    textBoxMaSach.ReadOnly = true; // Mã sách không được sửa
                }
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, "Lỗi khi chọn dòng: " + ex.Message, ThongBaoType.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}