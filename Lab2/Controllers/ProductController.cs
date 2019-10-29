using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Data;
using Lab2.Extensions;
using Lab2.Interfaces;
using Lab2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;

namespace Lab2.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        
        IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

       // [Authorize(Roles ="Admin")]
        [HttpPost]
        public  void Create(Product product)
        {
           
            if(product!=null)
             service.Create(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id != null)
            {
                Product product = await service.GetProduct(id);
                if (product != null)
                    return View(product);
            }
                return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductAll()
        {
            IEnumerable<Product> products = await service.GetProductAll();
                if (products != null)
                    return View(products);

            return NotFound();                       
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public void Edit(Product product)
        {
            if (ModelState.IsValid)
                service.Edit(product);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Product product = service.GetProduct(id).Result;
            if (product != null)
                return View(product);
            return NotFound();
        }

        public IActionResult ToKitchenController()
        {
            HttpContext.Session.Set("ListOfProducts", service.GetProductAll().Result);
            //TempData.Serialize("ListOfProducts", service.GetProductAll().Result);
            //TempData["ListOfProducts"] = service.GetProductAll();
           // TempData.Keep();
            return RedirectToAction("Create", "Kitchen");
        }
       

    }
}