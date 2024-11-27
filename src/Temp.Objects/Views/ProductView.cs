using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temp.Objects.Models;

namespace Temp.Objects.Views
{
    public class ProductView : AView<Product>
    {
        public Int64 IdProduct { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public Int64 Price { get; set; }
    }
}
