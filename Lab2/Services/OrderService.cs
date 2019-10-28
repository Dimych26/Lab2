using Lab2.Data;
using Lab2.Interfaces;
using Lab2.Models;
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
        public OrderService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<Order> GetDayOrder()
        {
            Order order =  await db.Orders.FirstOrDefaultAsync(o => o.Date == DateTime.Now);
            return order;
        }

        public  IEnumerable<Order> GetMonthOrder()
        {
            IEnumerable<Order> orders = db.Orders.Where(o => o.Date.Month == DateTime.Now.Month);
            return orders;
        }

        public IEnumerable<Order> GetWeekOrder()
        {
            int DaysOfWeek = 7;
            IEnumerable<Order> orders = db.Orders.Where(o => DateTime.Now.Day - o.Date.Day <= DaysOfWeek);
            return orders;
        }

        public async void MakeOrder(Order o)
        {
            Dish dish = await db.Dishes.FirstOrDefaultAsync(d=>d.Id == o.Dish.Id);

            if (dish != null)
            {
                Order order = new Order
                {
                    Date = DateTime.Now,
                    Dish = o.Dish,
                    UserName = o.UserName
                };

                db.Orders.Add(order);
            }

        }

        public async Task<Order> GetOrder(int id)
        {
            Order order = await db.Orders.FirstOrDefaultAsync(or => or.Id == id);
            return order;
        }
    }
}
