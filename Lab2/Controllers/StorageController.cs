using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Data;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    public class StorageController : Controller
    {
        ApplicationDbContext db;

        public StorageController(ApplicationDbContext db)
        {
            this.db = db;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost]
        public async void Create(int id,int count)
        {
            Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            product.Count = count;
            db.Products.Update(product);
            await db.SaveChangesAsync();
        }

        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Product> products = await db.Products.ToListAsync();
            if (products != null)
                return View(products);
            return NotFound();
        }

    }
}