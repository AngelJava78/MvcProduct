using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcProduct.Models.Entities
{
    public class Product
    {

        public int Id { get; set; }
        [Required, DataType(DataType.Text), StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText), StringLength(200)]
        public string Description { get; set; }
        [Required, DataType(DataType.Text), StringLength(20)]
        public string Family { get; set; }
        [Required, DataType(DataType.Text), StringLength(8)]
        public string Bin { get; set; }
        [Required, Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }
    }
}
