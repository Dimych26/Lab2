﻿using Lab2.Interfaces;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.ViewModels.Home
{
    public class IndexViewModel
    {
        IProductService service;
        IKitchen kitchen;
        public IndexViewModel(IProductService service,IKitchen kitchen)
        {
            this.service = service;
            this.kitchen = kitchen;
        }
        public IEnumerable<Product> Products => service.GetProductAll().Result; //=> new List<Product>();
        public IEnumerable<Dish> Dishes => kitchen.GetDishes().Result;//new List<Dish>();
    }
}
