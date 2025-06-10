using System;
using System.Drawing;
using System.Windows.Forms;
using SimplePOS.Models;

namespace SimplePOS
{
    public partial class OrderHistoryForm : Form
    {
        private ListBox listBoxOrders;
        private ListBox listBoxItems;

        public OrderHistoryForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "訂單查詢";
            this.ClientSize = new System.Drawing.Size(500, 320);
            this.BackColor = ColorTranslator.FromHtml("#f8f8f8");

            Label lblTitle = new Label()
            {
                Text = "📋 訂單查詢",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.MediumSlateBlue,
                AutoSize = false,
                Height = 40,
                Width = 200,
                Top = 10,
                Left = 20
            };

            listBoxOrders = new ListBox() { Top = 50, Left = 20, Width = 200, Height = 220, Font = new Font("Segoe UI", 12) };
            listBoxItems = new ListBox() { Top = 50, Left = 250, Width = 200, Height = 220, Font = new Font("Segoe UI", 12) };

            foreach (var order in CheckoutForm.Orders)
            {
                listBoxOrders.Items.Add($"訂單{order.OrderId} - ${order.Total} - {order.OrderTime:HH:mm:ss}");
            }
            listBoxOrders.SelectedIndexChanged += ListBoxOrders_SelectedIndexChanged;

            this.Controls.Add(lblTitle);
            this.Controls.Add(listBoxOrders);
            this.Controls.Add(listBoxItems);
        }

        private void ListBoxOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxItems.Items.Clear();
            if (listBoxOrders.SelectedIndex >= 0)
            {
                var order = CheckoutForm.Orders[listBoxOrders.SelectedIndex];
                foreach (var item in order.Items)
                {
                    listBoxItems.Items.Add($"{item.Item.Name} x {item.Quantity} = ${item.Total}");
                }
            }
        }
    }
}