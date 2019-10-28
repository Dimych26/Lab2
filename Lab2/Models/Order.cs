using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Dish Dish { get; set; }

        public string UserName { get; set; }

        //public Order(Dish dish)
        //{
        //    Date = DateTime.Now;
        //    Dish = dish;
        //}
    }
}
