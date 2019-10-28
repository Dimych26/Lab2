using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int DishId { get; set; }

        public double Price { get; set; }

        public virtual Dish Dish { get; set; }

        public virtual Order Order  { get; set; }
    }
}
