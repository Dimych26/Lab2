using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Data;
using Lab2.Interfaces;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    public class KitchenController : Controller
    {
        //ApplicationDbContext db;
        IKitchen kitchen; 
        public KitchenController(IKitchen kitchen)//ApplicationDbContext db)
        {
            this.kitchen = kitchen;
        }
        
        //public IActionResult Index()
        //{
        //    return View();
        //}
        
        public string Create(Product[] products)
        {
            string response=kitchen.CreateDish(products);
            return response;
        }

        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Dish> dishes = await kitchen.GetDishes(); //await db.Dishes.ToListAsync();
            if(dishes!=null)
                return View(dishes);
            return NotFound();
        }

        public async Task<IActionResult> GetDish(int? id)
        {
            if (id != null)
            {
                Dish dish = await kitchen.GetDish(id);
                if (dish != null)
                    return View(dish);
            }
            return NotFound();
        }

        public void EditDish(Dish dish)
        {
             kitchen.EditDish(dish);
        }
    }
}