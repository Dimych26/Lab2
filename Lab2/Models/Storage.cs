using Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Storage
    {
        IProductService productService;
        public List<Product> Products;
        public Storage(IProductService productService)
        {
            this.productService = productService;
            Products = productService.GetProductAll().Result.ToList();
        }
    }
}
