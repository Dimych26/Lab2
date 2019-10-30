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
        public void Create([FromBody]Product product)
        {
            if (User.Identity.Name == "Dima")
            {
                if (product != null)
                    service.Create(product);
            }
        }

        public IActionResult Create()
        {
            if (User.Identity.Name == "Dima")
                return View();
            return NotFound();
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

        
        [HttpPost]
        public void Edit(Product product)
        {
            if (User.Identity.Name == "Dima")
            {
                if (ModelState.IsValid)
                    service.Edit(product);
            }
        }

        
        public IActionResult Edit(int id)
        {
            if (User.Identity.Name == "Dima")
            {
                Product product = service.GetProduct(id).Result;
                if (product != null)
                    return View(product);
                return NotFound();
            }
            return NotFound();
        }

        public IActionResult ToKitchenController()
        {
            HttpContext.Session.Set("ListOfProducts", service.GetProductAll().Result);
         
            return RedirectToAction("Create", "Kitchen");
        }
       

    }
}