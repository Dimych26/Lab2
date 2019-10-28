using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Data;
using Lab2.Interfaces;
using Lab2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    [Authorize(Roles ="Admin")]
    public class StorageController : Controller
    {
        ApplicationDbContext db;
        IStorageService service;
        public StorageController(ApplicationDbContext db,IStorageService service)
        {
            this.db = db;
            this.service = service;
        }

        [HttpPost]
        public  void Create(int id,int count)
        {
            service.AddProduct(id, count);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = service.GetAllProducts();
            if (products != null)
                return View(products);
            return NotFound();
        }

        public IActionResult Get(int id)
        {
            Product product = service.GetProduct(id);
            if (product != null)
                return View(product);
            return NotFound();
        }

        public void Edit(Product product)
        {
            if(ModelState.IsValid)
                service.Edit(product);
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

    }
}