namespace SimplePOS.Models
{
    public class CartItem
    {
        public MenuItem Item { get; set; }
        public int Quantity { get; set; }
        public CartItem(MenuItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
        public decimal Total => Item.Price * Quantity;
    }
}