﻿using LolipopSquare.Interface;
using LolipopSquare.Models;
using Microsoft.AspNetCore.Mvc;

namespace LolipopSquare.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult EditProductForm(int id)
        {
            var edit = _productService.GetProductById(id);
            return View(edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(ProductDetailVM detailVM)
        {
             _productService.UpdateProduct(detailVM);
            return RedirectToAction("Index");
        }
    }
}
