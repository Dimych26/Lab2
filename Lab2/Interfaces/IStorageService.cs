using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
   public interface IStorageService
    {
        void AddProduct(int id,int count);
        Product GetProduct(int? id);
        IEnumerable<Product> GetAllProducts();
        void Edit(Product product);
    }
}
