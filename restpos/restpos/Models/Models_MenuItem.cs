
// 請將 MenuItem 類別加上 Category 屬性與建構子
namespace SimplePOS.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public MenuCategory Category { get; set; }

        public MenuItem(string name, decimal price, MenuCategory category)
        {
            Name = name;
            Price = price;
            Category = category;
        }

        public override string ToString() => $"{Name} - ${Price}";
    }
}
