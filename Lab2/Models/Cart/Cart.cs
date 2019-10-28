using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models.Cart
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Dish dish, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Dish.Id == dish.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Dish = dish,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Dish dish)
        {
            lineCollection.RemoveAll(l => l.Dish.Id == dish.Id);
        }

        public double ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Dish.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }


}

