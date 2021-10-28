using Microsoft.AspNetCore.Mvc.Rendering;
using MvcProduct.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcProduct.Models
{
    public class ProductFamilyViewModel
    {
        public List<Product> Products { get; set; }
        public SelectList Families { get; set; }
        public SelectList Bines { get; set; }
        public string ProductFamily { get; set; }
        public string ProductName { get; set; }
    }
}
