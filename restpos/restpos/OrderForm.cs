using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SimplePOS.Models;

namespace SimplePOS
{
    public enum MenuCategory
    {
        套餐,
        主菜,
        點心,
        飲料
    }

    public partial class OrderForm : Form
    {
        public static List<MenuItem> Menu = new List<MenuItem>
        {
            new MenuItem("漢堡+雞塊+可樂", 200, MenuCategory.套餐),
            new MenuItem("炸機+薯條+雪碧", 200, MenuCategory.套餐),
            new MenuItem("大亨堡+蛋塔+奶茶", 200, MenuCategory.套餐),
            new MenuItem("漢堡", 80, MenuCategory.主菜),
            new MenuItem("炸雞", 70, MenuCategory.主菜),
            new MenuItem("大亨堡", 60, MenuCategory.主菜),
            new MenuItem("薯條", 40, MenuCategory.點心),
            new MenuItem("沙拉", 30, MenuCategory.點心),
            new MenuItem("雞塊", 50, MenuCategory.點心),
            new MenuItem("蛋塔", 50, MenuCategory.點心),
            new MenuItem("冰淇淋", 30, MenuCategory.點心),
            new MenuItem("紅茶", 30, MenuCategory.飲料),
            new MenuItem("奶茶", 30, MenuCategory.飲料),
            new MenuItem("可樂", 30, MenuCategory.飲料),
            new MenuItem("雪碧", 30, MenuCategory.飲料),
            new MenuItem("檸檬紅茶", 30, MenuCategory.飲料),
        };
        public static List<CartItem> Cart = new List<CartItem>();

        private ComboBox cmbCategory;
        private readonly string[] drinkNames = new string[]
{
    "紅茶", "奶茶", "可樂", "雪碧", "檸檬紅茶"
};
        private ListBox listBoxMenu;
        private NumericUpDown nudQty;
        private Button btnAddToCart;
        private ComboBox cmbIce; // 冰塊選項

        public OrderForm()
        {
            InitializeComponent();

        }


        private void InitializeComponent()
        {
            this.Text = "點餐";
            this.ClientSize = new System.Drawing.Size(370, 340);
            this.BackColor = ColorTranslator.FromHtml("#FFF3E0");

            Label lblTitle = new Label()
            {
                Text = "🍔 請選擇餐點",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.OrangeRed,
                AutoSize = false,
                Height = 40,
                Width = 300,
                Top = 10,
                Left = 20
            };


            cmbCategory = new ComboBox()
            {
                Top = 50,
                Left = 20,
                Width = 120,
                Font = new Font("Segoe UI", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategory.Items.AddRange(Enum.GetNames(typeof(MenuCategory)));
            cmbCategory.SelectedIndex = 0;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;

            listBoxMenu = new ListBox() { Top = 90, Left = 20, Width = 200, Height = 140, Font = new Font("Segoe UI", 12) };
            listBoxMenu.SelectedIndexChanged += ListBoxMenu_SelectedIndexChanged;

            nudQty = new NumericUpDown() { Top = 240, Left = 20, Width = 60, Minimum = 1, Maximum = 50, Value = 1, Font = new Font("Segoe UI", 12) };
            btnAddToCart = new Button() { Text = "加入購物車", Top = 235, Left = 100, Width = 120, Height = 35, BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnAddToCart.FlatAppearance.BorderSize = 0;
            btnAddToCart.Click += BtnAddToCart_Click;

            cmbIce = new ComboBox()
            {
                Top = 200,
                Left = 230,
                Width = 100,
                Height = 30,
                Font = new Font("Segoe UI", 12),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Visible = false
            };
            cmbIce.Items.AddRange(new string[] { "正常冰", "少冰", "去冰" });
            cmbIce.SelectedIndex = 0;

            this.Controls.Add(lblTitle);
            this.Controls.Add(cmbCategory);
            this.Controls.Add(listBoxMenu);
            this.Controls.Add(nudQty);
            this.Controls.Add(btnAddToCart);
            this.Controls.Add(cmbIce);

            LoadMenuByCategory();
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMenuByCategory();
        }

        private void LoadMenuByCategory()
        {
            listBoxMenu.Items.Clear();
            if (Enum.TryParse<MenuCategory>(cmbCategory.SelectedItem?.ToString(), out var category))
            {
                foreach (var item in Menu.Where(m => m.Category == category))
                {
                    listBoxMenu.Items.Add(item);
                }
            }
            cmbIce.Visible = false;
            cmbIce.SelectedIndex = 0;
        }

        private void ListBoxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxMenu.SelectedItem is MenuItem menuItem &&
                (menuItem.Category == MenuCategory.飲料 || menuItem.Category == MenuCategory.套餐))
            {
                cmbIce.Visible = true;
                cmbIce.SelectedIndex = 0;
            }
            else
            {
                cmbIce.Visible = false;
                cmbIce.SelectedIndex = 0;
            }
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (listBoxMenu.SelectedItem is MenuItem menuItem)
            {
                int qty = (int)nudQty.Value;
                string itemName = menuItem.Name;

                // 飲料或套餐加註冰塊選項
                if ((menuItem.Category == MenuCategory.飲料 || menuItem.Category == MenuCategory.套餐) && cmbIce.Visible)
                {
                    string ice = cmbIce.SelectedItem?.ToString();
                    if (ice == "少冰" || ice == "去冰")
                    {
                        itemName += $"({ice})";
                    }
                }

                var exist = Cart.Find(c => c.Item.Name == itemName);
                if (exist != null)
                {
                    exist.Quantity += qty;
                }
                else
                {
                    Cart.Add(new CartItem(new MenuItem(itemName, menuItem.Price, menuItem.Category), qty));
                }
                MessageBox.Show("已加入購物車！");
            }
            else
            {
                MessageBox.Show("請選擇菜單項目！");
            }
        }

    }
}
