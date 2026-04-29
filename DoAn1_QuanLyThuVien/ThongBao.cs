using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAn1_QuanLyThuVien
{
    public enum ThongBaoType
    {
        Success,
        Error,
        Cancel,
        Info
    }

    public partial class ThongBao : Form
    {
        private Timer _timer;
        private Form _owner;

        public ThongBao()
        {
            InitializeComponent();
        }

        private ThongBao(Form owner, string message, ThongBaoType type, int durationMs)
        {
            InitializeComponent();

            _owner = owner;

            // Cấu hình form toast
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            TopMost = true;
            Opacity = 0.95;

            // Đặt nội dung & màu theo loại thông báo
            labelMessage.Text = message;
            ApplyType(type);

            // Định vị ban đầu ở góc trên-phải owner
            UpdateLocation();

            // Bám theo owner khi nó di chuyển/đổi kích thước/đóng
            if (_owner != null)
            {
                _owner.LocationChanged += Owner_Changed;
                _owner.SizeChanged += Owner_Changed;
                _owner.FormClosed += Owner_Closed;
            }

            // Timer tự đóng
            _timer = new Timer { Interval = durationMs };
            _timer.Tick += (s, e) =>
            {
                _timer.Stop();
                Close();
            };
            _timer.Start();
        }

        private void ApplyType(ThongBaoType type)
        {
            switch (type)
            {
                case ThongBaoType.Success:
                    panelColor.BackColor = Color.FromArgb(46, 204, 113);
                    labelType.Text = "Thành công";
                    pictureBoxImage.Image = Properties.Resources.icons8_success_32;
                    break;
                case ThongBaoType.Error:
                    panelColor.BackColor = Color.FromArgb(231, 76, 60);
                    labelType.Text = "Lỗi";
                    pictureBoxImage.Image = Properties.Resources.icons8_error_32;
                    break;
                case ThongBaoType.Cancel:
                    panelColor.BackColor = Color.FromArgb(241, 196, 15);
                    labelType.Text = "Cảnh báo";
                    pictureBoxImage.Image = Properties.Resources.icons8_cancel_32;
                    break;
                case ThongBaoType.Info:
                    panelColor.BackColor = Color.FromArgb(52, 152, 219);
                    labelType.Text = "Thông báo";
                    pictureBoxImage.Image = Properties.Resources.icons8_info_32;
                    break;
            }
        }

        private void Owner_Changed(object sender, EventArgs e) => UpdateLocation();
        private void Owner_Closed(object sender, FormClosedEventArgs e) => Close();

        private void UpdateLocation()
        {
            if (_owner == null || _owner.IsDisposed) return;

            const int margin = 10;

            // Lấy toạ độ MÀN HÌNH của góc trên-phải vùng client của owner.
            // Cách này đúng cho cả form top-level lẫn MDI child.
            Point topRightScreen = _owner.PointToScreen(new Point(_owner.ClientSize.Width, 0));

            int x = topRightScreen.X - Width - margin;
            int y = topRightScreen.Y + margin;

            Location = new Point(x, y);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_owner != null)
            {
                _owner.LocationChanged -= Owner_Changed;
                _owner.SizeChanged -= Owner_Changed;
                _owner.FormClosed -= Owner_Closed;
            }
            _timer?.Dispose();
            base.OnFormClosed(e);
        }

        // Không lấy focus khỏi form chính
        protected override bool ShowWithoutActivation => true;
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOOLWINDOW = 0x80;
                const int WS_EX_NOACTIVATE = 0x08000000;
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW | WS_EX_NOACTIVATE;
                return cp;
            }
        }

        private void ThongBao_Load(object sender, EventArgs e)
        {
        }

        // ===== API tiện dùng =====
        public static void Show(Form owner, string message,
                                ThongBaoType type = ThongBaoType.Success,
                                int durationMs = 3000)
        {
            var t = new ThongBao(owner, message, type, durationMs);
            t.Show(owner);
        }
    }
}
