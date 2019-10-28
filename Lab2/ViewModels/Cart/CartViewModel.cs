using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Models.Cart;

namespace Lab2.ViewModels.Cart
{
    public class CartViewModel
    {
        public Models.Cart.Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
