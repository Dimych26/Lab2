using Lab2.Data;
using Lab2.Interfaces;
using Lab2.Models;
using Lab2.Models.Cart;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext db;
        private Cart cart;
        public OrderService(ApplicationDbContext db,Cart cart)
        {
            this.db = db;
            this.cart = cart;
        }
        public async Task<Order> GetDayOrder()
        {
            Order order =  await db.Orders.FirstOrDefaultAsync(o => o.OrderTime == DateTime.Now);
            return order;
        }

        public  IEnumerable<Order> GetMonthOrder()
        {
            IEnumerable<Order> orders = db.Orders.Where(o => o.OrderTime.Month == DateTime.Now.Month);
            return orders;
        }

        public IEnumerable<Order> GetWeekOrder()
        {
            int DaysOfWeek = 7;
            IEnumerable<Order> orders = db.Orders.Where(o => DateTime.Now.Day - o.OrderTime.Day <= DaysOfWeek);
            return orders;
        }

        public  void MakeOrder(Order o)
        {
            o.OrderTime = DateTime.Now;
            db.Orders.Add(o);
            
            var items = cart.Lines;

            foreach(var el in items)
            {
                var orderDetail = new OrderDetails()
                {
                    DishId = el.Dish.Id,
                    OrderId = o.Id,
                    Price = el.Dish.Price
                };
                db.OrderDetails.Add(orderDetail);

            }
            db.SaveChanges();
            //Dish dish = await db.Dishes.FirstOrDefaultAsync(d=>d.Id == o.Dish.Id);

            //if (dish != null)
            //{
            //    Order order = new Order
            //    {
            //        Date = DateTime.Now,
            //        Dish = o.Dish,
            //        UserName = o.UserName
            //    };

            //    db.Orders.Add(order);
            //}

        }

        public async Task<Order> GetOrder(int id)
        {
            Order order = await db.Orders.FirstOrDefaultAsync(or => or.Id == id);
            return order;
        }
    }
}
