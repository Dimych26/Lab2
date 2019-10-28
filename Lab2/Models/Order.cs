using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Имя")]
        public string Name { get; set; }

        public DateTime OrderTime { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
        //public int Id { get; set; }

        //public DateTime Date { get; set; }

        //public Dish Dish { get; set; }

        //public string UserName { get; set; }

        //public Order(Dish dish)
        //{
        //    Date = DateTime.Now;
        //    Dish = dish;
        //}
    }
}
