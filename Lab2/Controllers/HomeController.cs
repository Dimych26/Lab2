using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab2.Models;
using Lab2.ViewModels.Home;
using Lab2.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Lab2.Controllers
{
    public class HomeController : Controller
    {
        IProductService service;
        IKitchen kitchen;
        UserManager<User> manager;
        public HomeController(IProductService service, IKitchen kitchen,UserManager<User> manager)
        {
            this.service = service;
            this.kitchen = kitchen;
            this.manager = manager;
        }
        public IActionResult Index(bool category)//bool Top,bool New)
        {
            IndexViewModel model; 

            if (category == false)
            {
             
                model = new IndexViewModel(service,kitchen);
                return View(model);
            }
            else
            {
                
                List<Dish> ViewDishes = new List<Dish>();
                List<Product> ViewProducts = new List<Product>();
                IEnumerable<Dish> dishes = kitchen.GetDishes().Result;
                IEnumerable<Product> products = service.GetProductAll().Result;
                foreach (var item in dishes)
                {
                    if (item.Top == category || item.New == category)
                    {
                        ViewDishes.Add(item);
                    }
                }
                foreach (var item in products)
                {
                    if (item.Top == category || item.New == category)
                    {
                        ViewProducts.Add(item);
                    }
                }
                model = new IndexViewModel
                {
                    Dishes = ViewDishes,
                    Products = ViewProducts
                };
                
                return View(model);
            }
           
        }

        public ActionResult basket(string ids)
        {
            if (ids == null || ids == "")
            {
                return View();
            }
            else
            {
                List<string> l = ids.Split(',').ToList();
                IEnumerable<Dish> AllDishes = kitchen.GetDishes().Result;
                    
                List<Dish> dishes = new List<Dish>();
                for (int i = 0; i < l.Count; i++)
                {
                    foreach (var item in AllDishes)
                    {
                        if (item.Id.ToString() == l[i])
                        {
                            dishes.Add(item);
                        }


                    }
                }
               
                return View(dishes);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UserPage()
        {
            return View();
        }
        public IActionResult AdPage()
        {
           
            return View();
        }

        public async Task<IList<User>> GetUserRoles()
        {
            return await manager.GetUsersInRoleAsync("admin");
        }
    }
}
