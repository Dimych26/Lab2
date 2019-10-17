using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
    public interface IProductService
    {
        void Create(Product product);
        Task<Product> GetProduct(int? id);
        Task<IEnumerable<Product>> GetProductAll();
        void Edit(Product product);
    }
}
