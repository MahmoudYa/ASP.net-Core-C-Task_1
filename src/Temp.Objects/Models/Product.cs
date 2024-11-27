
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Temp.Objects.Views;

namespace Temp.Objects.Models
{

    public class Product : AModel
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

        public static explicit operator Product(ProductView v)
        {
            throw new NotImplementedException();
        }
    }
}

