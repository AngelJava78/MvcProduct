using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MvcProduct.Models;
using MvcProduct.Models.Data;
using MvcProduct.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcProduct.Controllers
{
    public class ProductsController : Controller
    {
        private IRepository<Product> repository;
        private readonly IConfiguration configuration;
        public ProductsController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repository = new ProductRepository(this.configuration);

        }
        public IActionResult Index(string productFamily, string productName)
        {
            var familyRepository = new FamilyGetRepository(configuration);
            var binRepository = new BinGetRepository(configuration);
            var families = familyRepository.GetAll();
            var bines = binRepository.GetAll();
            var products = repository.GetAll();
            var queryFamilies = families.OrderBy(f => f.Name).Select(f => f.Name).Distinct();
            var queryBines = bines.OrderBy(b => b.Name).Select(b => b.Name).Distinct();
            if (!string.IsNullOrWhiteSpace(productName))
            {
                products = products.Where(p => p.Name.Contains(productName)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(productFamily))
            {
                products = products.Where(f => f.Family == productFamily).ToList();
            }
            var model = new ProductFamilyViewModel
            {
                Bines = new SelectList(queryBines.ToList()),
                Families = new SelectList(queryFamilies.ToList()),
                Products = products,
                ProductName = productName,
                ProductFamily = productFamily
            };
            return View(model);
        }

        public IActionResult Create()
        {
            var familyRepository = new FamilyGetRepository(configuration);
            var binRepository = new BinGetRepository(configuration);
            var families = familyRepository.GetAll();
            var bines = binRepository.GetAll();
            var queryFamilies = families.OrderBy(f => f.Name).Select(f => f.Name).Distinct();
            var queryBines = bines.OrderBy(b => b.Name).Select(b => b.Name).Distinct();

            var model = new NewProductModel
            {
                Bines = new SelectList(queryBines.ToList()),
                Families = new SelectList(queryFamilies.ToList()),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Product,Families,Bines")] NewProductModel newProduct)
        {
            if (ModelState.IsValid)
            {
                //var product = new Product
                //{
                //    Id = newProduct.Id,
                //    Name = newProduct.Name,
                //    Description = newProduct.Description,
                //    IsActive = newProduct.IsActive,
                //    ReleaseDate = newProduct.ReleaseDate,
                //    Bin = newProduct.ProductBin,
                //    Family = newProduct.ProductFamily
                //};
                //repository.Add(product);
                repository.Add(newProduct.Product);
                return RedirectToAction(nameof(Index));

            }
            return View(newProduct);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = repository.GetById(id.Value);
            if(product== null)
            {
                return NotFound();
            }
            return View(product);

        }
    }
}
