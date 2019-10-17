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
       
       public int Count { get; set; }
       
       public double Price { get; set; }

       public bool Top { get; set; }

       public bool New { get; set; }
       
    }
}
