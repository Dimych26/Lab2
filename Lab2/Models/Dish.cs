﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
     
        public bool Top { get; set; }
        public bool New { get; set; }
        public List<Product> Products;
        public Dish()
        {
            Products = new List<Product>();
        }

        public double Price { get; set; }//=> Products.Sum(p => p.Price)*1.5;
        public double Calories { get; set; } //=> Products.Sum(p => p.Calories);
        public int Weight { get; set; } //=> Products.Sum(p => p.Weight);
    }
}
