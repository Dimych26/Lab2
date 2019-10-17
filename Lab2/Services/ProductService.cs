using Lab2.Data;
using Lab2.Interfaces;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public class ProductService : IProductService
    {

       private ApplicationDbContext db;

        public ProductService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async void Create(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
        }

        public async void Edit(Product prod)
        {
            db.Products.Update(prod);
            await db.SaveChangesAsync();
        }

        public async Task<Product> GetProduct(int? id)
        {
            Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
            
        }

        public async Task<IEnumerable<Product>> GetProductAll()
        {
            IEnumerable<Product> products = await db.Products.ToListAsync();
            return products;
        }
    }
}
