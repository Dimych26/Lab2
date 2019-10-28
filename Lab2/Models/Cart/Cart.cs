using Lab2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models.Cart
{
    public class Cart
    {
        private readonly ApplicationDbContext context;
        public Cart(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string CartId { get; set; }
        private List<CartItem> lineCollection = new List<CartItem>();

        public void AddItem(Dish dish, int quantity)
        {
            CartItem line = lineCollection
                .Where(g => g.Dish.Id == dish.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartItem
                {
                    Dish = dish,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Dish dish)
        {
            lineCollection.RemoveAll(l => l.Dish.Id == dish.Id);
        }

        public double ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Dish.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public List<CartItem> Lines
        {
            get { return lineCollection; }
            set { lineCollection = value; }
        }

        public static Cart GetCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<ApplicationDbContext>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();//якщо не існує cartid

            session.SetString("CartId", shopCartId);

            return new Cart(context) { CartId = shopCartId };
        }

        public void AddToCart(Dish dish, int quantity)
        {
            context.CartItems.Add(new CartItem()
            {
                ShopCartId = CartId,
                Dish = dish
                

            });
            context.SaveChanges();
        }
        public List<CartItem> GetCartItems()
        {
            return context.CartItems.Where(c => c.ShopCartId == CartId).Include(s => s.Dish).ToList();
        }
    }


}

