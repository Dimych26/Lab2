using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Product
    {
       
       public int Id { get; set; }

       [Required]
       public string Name { get; set; }

       public string Description { get; set; }

       public int Weight { get; set; }

       [Required]
       [Range(0, 100)]
       public double Proteins { get; set; }

       [Required]
       [Range(0, 100)]
       public double Fat { get; set; }

       [Required]
       [Range(0, 100)]
       public double Carbohydrates { get; set; }

       [Required]
       [Range(0,100)]
       public double Calories { get; set; }

       [ScaffoldColumn(false)]
       public int Count { get; set; }

       [ScaffoldColumn(false)]
       public double Price { get; set; }

       [ScaffoldColumn(false)]
       public bool Top { get; set; }

       [ScaffoldColumn(false)]
       public bool New { get; set; }
       
    }
}
