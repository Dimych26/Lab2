using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab2.Models;
using Lab2.ViewModels.Home;
using Lab2.Interfaces;

namespace Lab2.Controllers
{
    public class HomeController : Controller
    {
        IProductService service;
        IKitchen kitchen;
        public HomeController(IProductService service, IKitchen kitchen)
        {
            this.service = service;
            this.kitchen = kitchen;
        }
        public IActionResult Index(bool Top,bool New)
        {
            IndexViewModel model = new IndexViewModel(service,kitchen);

            //if (category == false )
            //{
            //    IEnumerable<TovarDTO> tovarDTOs = it.GetAll();
            //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            //    var users = mapper.Map<IEnumerable<TovarDTO>, List<TovarViewModel>>(tovarDTOs);
            //    return View(users);
            //}
            //else
            //{
            //    IEnumerable<TovarDTO> tovarDTOs = it.GetAll();
            //    List<TovarDTO> dTOs = new List<TovarDTO>();
            //    foreach (var item in tovarDTOs)
            //    {
            //        if (item.Top == category || item.New == category)
            //        {
            //            dTOs.Add(item);
            //        }
            //    }
            //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            //    var users = mapper.Map<List<TovarDTO>, List<TovarViewModel>>(dTOs);
            //    return View(users);
            //}
            return View(model);
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
    }
}
