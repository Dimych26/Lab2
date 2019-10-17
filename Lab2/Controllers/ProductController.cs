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
    public class ProductController : Controller
    {
        //ApplicationDbContext db;
        //public IActionResult Index()
        //{
        //    return View();
        //}
        IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpPost]
        public  void Create(Product product)
        {
             service.Create(product);
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
            service.Edit(product);
        }
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if(id!=null)
        //    {
        //        Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
        //        if(product!=null)
        //        {
        //            return View(product);
        //        }
        //    }
        //    return NotFound();
        //}


    }
}