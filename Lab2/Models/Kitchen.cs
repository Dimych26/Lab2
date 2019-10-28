using Lab2.Data;
using Lab2.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Lab2.Models
{
    public class Kitchen:IKitchen
    {
        private ApplicationDbContext db;
        IProductService productService;

        public Kitchen(ApplicationDbContext db,IProductService productService)
        {
            this.db = db;
            this.productService = productService;
        }
        public Storage Storage => new Storage(productService);

        [Authorize(Roles ="Admin")]
        public string CreateDish(List<Product> products,string Name)
        {

            string response = "";
            foreach(var p in products)
            {
                if (!Storage.Products.Contains(p) || Storage.Products.Find(m => m.Name == p.Name).Count == 0) 
                {
                    response += p.Name + ", ";
                }           
            }

            //добав проверку пользователя, і в залежності від ролі, додавай або ні блюдо в базу
            if (string.IsNullOrEmpty(response))
            {
                Dish dish = new Dish
                {
                    Name = Name
                };
                foreach (var item in products)
                {
                    dish.Products.Add(item);
                }
                dish.Price=dish.Products.Sum(p => p.Price) * 1.5;
                dish.Calories = dish.Products.Sum(p => p.Calories);
                dish.Weight = dish.Products.Sum(p => p.Weight);
                db.Dishes.Add(dish);
                db.SaveChanges();
                response = "Блюдо добавлено";
            }
            else
            {
                response= string.Join("Извините, этих продуктов нет в наличии:", response);
            }
            return response;
        }

        [Authorize(Roles ="Admin")]
        public void EditDish(Dish dish)
        {
            db.Dishes.Update(dish);
            db.SaveChanges();
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

        public IEnumerable<Dish> GetNewDishes()
        {
            List<Dish> dishes = GetDishes().Result.ToList();
           for(int i=0;i<dishes.Count;i++)// foreach(var item in dishes)
            {
                if(!dishes[i].New)
                {
                    dishes.Remove(dishes[i]);
                }
            }
            return dishes;
        }

        public IEnumerable<Dish> GetTopDishes()
        {
            List<Dish> dishes = GetDishes().Result.ToList();
            for (int i = 0; i < dishes.Count; i++)// foreach(var item in dishes)
            {
                if (!dishes[i].Top)
                {
                    dishes.Remove(dishes[i]);
                }
            }
            return dishes;
        }
    }
}
