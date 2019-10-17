using Lab2.Data;
using Lab2.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Kitchen:IKitchen
    {
        public Storage Storage { get; set; }

        private ApplicationDbContext db;

        public Kitchen(ApplicationDbContext db)
        {
            this.db = db;
        }
        public string CreateDish(Product[] products)
        {
            string response = "";
            foreach(var p in products)
            {
                if (!Storage.Products.Contains(p) || Storage.Products.Find(m => m.Name == p.Name).Count == 0) 
                {
                    response += p.Name + ", ";
                }           
            }

            if(string.IsNullOrEmpty(response))
            {
                db.Dishes.Add(new Dish
                {
                    Products = products.ToList(),
                });
                db.SaveChanges();
                response = "Блюдо добавлено";
               
            }
            else
            {
                response= string.Join("Извините, этих продуктов нет в наличии:", response);
            }
            return response;
        }

      
        public async void EditDish(Dish dish)
        {
            db.Dishes.Update(dish);
            await db.SaveChangesAsync();
        }

        

        public async Task<Dish> GetDish(int? id)
        {
            Dish dish = await db.Dishes.FirstOrDefaultAsync(d => d.Id == id);
            return dish;
        }

        public async Task<IEnumerable<Dish>> GetDishes()
        {
            IEnumerable<Dish> dishes = await db.Dishes.ToListAsync();
            return dishes;
        }

      


    }
}
