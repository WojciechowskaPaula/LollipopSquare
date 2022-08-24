﻿using LolipopSquare.Interface;
using LolipopSquare.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

namespace LolipopSquare.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly SignInManager<IdentityUser> _signInManager;
        public ShoppingCartController(IShoppingCartService shoppingCartService, SignInManager<IdentityUser> signInManager)
        {
            _shoppingCartService = shoppingCartService;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult AddItem(int productId, int quantity = 1)
        {
            List<ShoppingCartItem> listOfItems = new List<ShoppingCartItem>();
            var products = HttpContext.Session.GetString("product");
            if(products != null)
            {
                var newProduct = _shoppingCartService.GetProduct(productId);
               listOfItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(products);
                foreach (var item in listOfItems)
                {
                    if(newProduct.ProductId == item.ProductId)
                    {
                        item.Quantity = item.Quantity + quantity;
                    }
                }
                if (!listOfItems.Exists(x=>x.ProductId == newProduct.ProductId))
                {
                    newProduct.Quantity = quantity;
                    listOfItems.Add(newProduct);
                }
            }
            else
            {
                var product = _shoppingCartService.GetProduct(productId);
                product.Quantity = quantity;
                listOfItems.Add(product);
            }
            
            
            string serializeList = JsonSerializer.Serialize(listOfItems);
            HttpContext.Session.SetString("product", serializeList);
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult AddItemForm(int productId, int quantity = 1)
        {
            List<ShoppingCartItem> listOfItems = new List<ShoppingCartItem>();
            var products = HttpContext.Session.GetString("product");
            if (products != null)
            {
                var newProduct = _shoppingCartService.GetProduct(productId);
                listOfItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(products);
                foreach (var item in listOfItems)
                {
                    if (newProduct.ProductId == item.ProductId)
                    {
                        item.Quantity = item.Quantity + quantity;
                    }
                }
                if (!listOfItems.Exists(x => x.ProductId == newProduct.ProductId))
                {
                    newProduct.Quantity = quantity;
                    listOfItems.Add(newProduct);
                }
            }
            else
            {
                var product = _shoppingCartService.GetProduct(productId);
                product.Quantity = quantity;
                listOfItems.Add(product);
            }


            string serializeList = JsonSerializer.Serialize(listOfItems);
            HttpContext.Session.SetString("product", serializeList);
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetShoppingCartItems()
        {
            var products = HttpContext.Session.GetString("product");
            if (products == null)
            {
                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                return View(shoppingCartItems);
            }
            else
            {
                var shoppingCartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(products);
                return View(shoppingCartItems);
            }
        }

        [HttpPost]
        public IActionResult DeleteProductFromCart(int id)
        {
            var productsToRemoveFromCart = HttpContext.Session.GetString("product");
            var listOfProducts = JsonSerializer.Deserialize<List<ShoppingCartItem>>(productsToRemoveFromCart);
            listOfProducts.RemoveAll(x => x.ProductId == id);
            var listOfProductsAsJson = JsonSerializer.Serialize(listOfProducts);
            HttpContext.Session.SetString("product", listOfProductsAsJson);
            return RedirectToAction("GetShoppingCartItems");
        }
    }
}
