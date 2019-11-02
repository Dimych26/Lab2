using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Data;
using Lab2.Interfaces;
using Lab2.Models;
using Lab2.ViewModels.Kitchen;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab2.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;

namespace Lab2.Controllers
{
    [Authorize]
    public class KitchenController : Controller
    {
        IKitchen kitchen;
        IProductService productService;
        public KitchenController(IKitchen kitchen, IProductService productService)
        {
            this.kitchen = kitchen;
            this.productService = productService;
        }
        
       // [Authorize(Roles="Admin")]
        [HttpPost]
        public string Create(int[] model, string Name)
        {
            if (User.Identity.Name == "Dima")
            {
                string response = "";
                List<Product> products = new List<Product>();
                foreach (var item in model)
                {
                    products.Add(productService.GetProduct(item).Result);
                }

                response = kitchen.CreateDish(products, Name);
                return response;
            }
            return "У вас нет прав";
        }

       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            if (User.Identity.Name == "Dima")
            {
                MultiSelectList list = new MultiSelectList(HttpContext.Session.Get<List<Product>>("ListOfProducts"), "Id", "Name");

                ViewBag.SelectList = list;

                return View();
            }
            return NotFound();
        }

       

       // [Authorize(Roles = "Admin")]
        public IActionResult CreateDish()
        {
            if (User.Identity.Name=="Dima")
                return RedirectToAction("ToKitchenController", "Product");
            return NotFound();
        }

        [Authorize(Roles ="admin")]//IT`IS WOOOOOOOOOOOOOOOOOOOORK DON`T TOUCH
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Dish> dishes = await kitchen.GetDishes(); //await db.Dishes.ToListAsync();
            if(dishes!=null)
                return View(dishes);
            return NotFound();
        }
        [Authorize(Roles = "admin")]
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

       // [Authorize(Roles = "Admin")]
        public void EditDish(Dish dish)
        {
            if (User.Identity.Name == "Dima")
                kitchen.EditDish(dish);
            
        }

      //  [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            if (User.Identity.Name == "Dima")
            {
                Product product = productService.GetProduct(id).Result;
                if (product != null)
                    return View(product);
                return NotFound();
            }
            return NotFound();
        }

        public IActionResult GetNewDishes()
        {
            IEnumerable<Dish> dishes = kitchen.GetNewDishes();
            if (dishes != null)
                return View(dishes);
            return NotFound();
        }

        public IActionResult GetTopDishes()
        {
            IEnumerable<Dish> dishes = kitchen.GetTopDishes();
            if (dishes != null)
                return View(dishes);
            return NotFound();
        }
    }
}