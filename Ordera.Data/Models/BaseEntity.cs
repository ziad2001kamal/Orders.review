﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Data.Models
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? CreatedBy  { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDelete { get; set; }

        public BaseEntity() { 
        CreatedAt = DateTime.Now;
        }

    }
}
