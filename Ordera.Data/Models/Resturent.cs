﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Data.Models
{
    public class Resturent : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        [Required]
        public string Phone { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longtude { get; set; }
        public string Adderss { get; set; } 

        public List<Meal> Meals {  get; set; }
        public List<Order> Orders { get; set; }


    }
}
