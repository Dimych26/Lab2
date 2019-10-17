using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
    public interface IKitchen
    {
        string CreateDish(Product[] products);       
        void EditDish(Dish dish);
        Task<Dish> GetDish(int? id);
        Task<IEnumerable<Dish>> GetDishes();
    }
}
