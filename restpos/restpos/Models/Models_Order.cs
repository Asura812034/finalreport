using System;
using System.Collections.Generic;

namespace SimplePOS.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderTime { get; set; }
        public Order(int id, List<CartItem> items, decimal total)
        {
            OrderId = id;
            Items = items;
            Total = total;
            OrderTime = DateTime.Now;
        }
    }
}