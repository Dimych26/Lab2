using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models.Cart
{
    public class CartItem
    {
        public int Id { get; set; }
        public Dish Dish { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ShopCartId { get; set; }
    }
}
