using System;
using System.Drawing;
using System.Windows.Forms;
using SimplePOS.Models;

namespace SimplePOS
{
    public partial class CartForm : Form
    {
        private ListBox listBoxCart;
        private Button btnRemove;
        private Button btnClear;

        public CartForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "購物車";
            this.ClientSize = new System.Drawing.Size(350, 300);
            this.BackColor = ColorTranslator.FromHtml("#FFF3E0");

            Label lblTitle = new Label()
            {
                Text = "🛒 購物車內容",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.OrangeRed,
                AutoSize = false,
                Height = 40,
                Width = 300,
                Top = 10,
                Left = 20
            };

            listBoxCart = new ListBox() { Top = 50, Left = 20, Width = 280, Height = 180, Font = new Font("Segoe UI", 12) };
            RefreshCart();

            btnRemove = new Button() { Text = "移除選取", Top = 240, Left = 20, Width = 100, BackColor = Color.OrangeRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += BtnRemove_Click;

            btnClear = new Button() { Text = "清空購物車", Top = 240, Left = 140, Width = 120, BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += BtnClear_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(listBoxCart);
            this.Controls.Add(btnRemove);
            this.Controls.Add(btnClear);
        }

        private void RefreshCart()
        {
            if (listBoxCart == null) return;
            listBoxCart.Items.Clear();
            foreach (var item in OrderForm.Cart)
            {
                listBoxCart.Items.Add($"{item.Item.Name} x {item.Quantity} = ${item.Total}");
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (listBoxCart.SelectedIndex >= 0)
            {
                OrderForm.Cart.RemoveAt(listBoxCart.SelectedIndex);
                RefreshCart();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            OrderForm.Cart.Clear();
            RefreshCart();
        }
    }
}