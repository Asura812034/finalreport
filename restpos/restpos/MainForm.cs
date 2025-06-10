using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimplePOS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOrder_Click(object sender, EventArgs e) => new OrderForm().ShowDialog();
        private void btnCart_Click(object sender, EventArgs e) => new CartForm().ShowDialog();
        private void btnCheckout_Click(object sender, EventArgs e) => new CheckoutForm().ShowDialog();
        private void btnOrderHistory_Click(object sender, EventArgs e) => new OrderHistoryForm().ShowDialog();

        private void InitializeComponent()
        {
            this.Text = "🍔 餐廳點餐系統";
            this.ClientSize = new Size(400, 500);
            this.BackColor = ColorTranslator.FromHtml("#f8f8f8");
            this.Font = new Font("Segoe UI", 12);

            // 標題
            Label lblTitle = new Label()
            {
                Text = "🍽️ 歡迎使用餐廳點餐系統",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.OrangeRed,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(0, 20, 0, 0)
            };

            // 按鈕樣式
            Button NewButton(string text, int top)
            {
                var btn = new Button()
                {
                    Text = text,
                    Top = top,
                    Left = 110,
                    Width = 180,
                    Height = 50,
                    BackColor = Color.Orange,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.DarkOrange;
                btn.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 20, 20));
                return btn;
            }

            var btnOrder = NewButton("點餐", 210);
            var btnCart = NewButton("購物車", 270);
            var btnCheckout = NewButton("結帳", 330);
            var btnOrderHistory = NewButton("訂單查詢", 390);

            btnOrder.Click += btnOrder_Click;
            btnCart.Click += btnCart_Click;
            btnCheckout.Click += btnCheckout_Click;
            btnOrderHistory.Click += btnOrderHistory_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnOrder);
            this.Controls.Add(btnCart);
            this.Controls.Add(btnCheckout);
            this.Controls.Add(btnOrderHistory);
        }

        // 圓角按鈕用
        internal class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            public static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
            );
        }
    }
}