using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class User:IdentityUser
    {
       
        [Display(Name = "Год рождения")]
        public int Year { get; set; }

        
        [Display(Name = "Вес")]
        public int Weight { get; set; }

        
        [Display(Name = "Рост")]
        public int Height { get; set; }

       // public float Calories { get; set; }
    }
}
