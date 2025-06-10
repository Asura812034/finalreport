using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SimplePOS.Models;

namespace SimplePOS
{
    public partial class CheckoutForm : Form
    {
        private Label lblTotal;
        private Button btnCheckout;
        private ComboBox cbPayType;

        public static List<Order> Orders = new List<Order>();
        private static int OrderCounter = 1;

        public CheckoutForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "結帳";
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.BackColor = ColorTranslator.FromHtml("#FFF3E0");

            Label lblTitle = new Label()
            {
                Text = "💵 結帳",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.SeaGreen,
                AutoSize = false,
                Height = 40,
                Width = 200,
                Top = 10,
                Left = 30
            };

            lblTotal = new Label() { Top = 60, Left = 30, Width = 200, Font = new Font("Segoe UI", 12, FontStyle.Bold) };

            cbPayType = new ComboBox() { Top = 90, Left = 30, Width = 120, Font = new Font("Segoe UI", 12) };
            cbPayType.Items.AddRange(new string[] { "現金", "信用卡" });
            cbPayType.SelectedIndex = 0;

            btnCheckout = new Button() { Text = "結帳", Top = 130, Left = 30, Width = 100, Height = 35, BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.Click += BtnCheckout_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblTotal);
            this.Controls.Add(cbPayType);
            this.Controls.Add(btnCheckout);

            decimal total = OrderForm.Cart.Sum(i => i.Total);
            lblTotal.Text = $"總金額：${total}";
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (OrderForm.Cart.Count == 0)
            {
                MessageBox.Show("購物車是空的！");
                return;
            }
            var order = new Order(OrderCounter++, new List<CartItem>(OrderForm.Cart), OrderForm.Cart.Sum(i => i.Total));
            Orders.Add(order);
            OrderForm.Cart.Clear();
            MessageBox.Show($"結帳成功！\n訂單編號：{order.OrderId}\n總金額：${order.Total}", "付款成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}