using Lab2.Data;
using Lab2.Interfaces;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public class StorageService : IStorageService
    {
        private ApplicationDbContext db;
        public StorageService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddProduct(int id,int count)
        {
            Product product =  db.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Count = count;
                db.Products.Update(product);
                db.SaveChanges();
            }

        }

        public void Edit(Product product)
        {

            db.Products.Update(product);
            db.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> products =  db.Products.ToList();
            return products;
        }

        public Product GetProduct(int? id)
        {
            Product product =  db.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }
    }
}
