using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Orders.Data.Models
{
    public class User : IdentityUser
    {
        [Required]

        public string Fullname { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longtude { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDelete { get; set; }
        public User() {
            CreatedAt = DateTime.Now;

        }

    }
}
