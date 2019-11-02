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

        public void AddToCart(Dish dish)
        {
            var item = context.CartItems.Where(c => c.ShopCartId == CartId).Include(s => s.Dish).ToList();

            
            if (!item.Exists(c => c.Dish.Equals(dish)))
            {
                context.CartItems.Add(new CartItem()
                {
                    ShopCartId = CartId,
                    Dish = dish,
                    Quantity = 1,
                    Price=(int)dish.Price
                });
                context.SaveChanges();
            }
            else
            {
                item.ForEach(i => i.Quantity++);
                item.ForEach(i => context.Update(i));
                context.SaveChanges();
            }

        }
        public List<CartItem> GetCartItems()
        {
            return context.CartItems.Where(c => c.ShopCartId == CartId).Include(s => s.Dish).ToList();
        }

        public void RemoveAllFromCart()
        {
            var some = context.CartItems.Where(c => c.ShopCartId == CartId).Include(s => s.Dish).ToList();

            for (int i = 0; i < some.Count(); i++)
            {
                context.CartItems.Remove(some[i]);
            }
            context.SaveChanges();
            
        }

        public void RemoveLine(Dish dish)
        {
            var some = GetCartItems();
           
            if(some.Exists(c => c.Dish.Equals(dish)))
            {
                for(int i=0;i<some.Count;i++)
                {
                    if(some[i].Dish.Equals(dish))
                    {
                        context.CartItems.Remove(some[i]);
                    }
                }
             
            }
            
            context.SaveChanges();
        }
    }


}

