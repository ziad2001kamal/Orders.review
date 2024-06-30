using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.ViewModels
{
    public class ResturentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Phone { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longtude { get; set; }
        public string Adderss { get; set; }
    }
}
