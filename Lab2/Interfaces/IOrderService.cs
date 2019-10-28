using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
   public interface IOrderService
    {
        void MakeOrder(Order order);
        Task<Order> GetDayOrder();
        IEnumerable<Order> GetMonthOrder();
        IEnumerable<Order> GetWeekOrder();
        Task<Order> GetOrder(int id);


    }
}
