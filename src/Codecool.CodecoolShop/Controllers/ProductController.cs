using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Codecool.CodecoolShop.Helpers;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());
        }

        
        
        
        public IActionResult Index(int categoryNumber = 1)
        {
            var products = ProductService.GetProductsForCategory(categoryNumber);
            
            //product category
            var list = ProductCategoryDaoMemory.GetInstance().GetAll().ToList();
            ViewData["ListCategory"] = list;
            
            // product suppliers
            var listOfSuppliers = SupplierDaoMemory.GetInstance().GetAll().ToList();
            ViewData["listOfSuppliers"] = listOfSuppliers;
            if (SessionHelper.GetObjectFromJson<List<LineItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<LineItem>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.product.DefaultPrice * item.Quantity);
                ViewBag.Quantity = cart.Sum(item => item.Quantity);
            }
            else
            {
                ViewBag.cart = null;
                ViewBag.total = 0;
                ViewBag.Quantity = 0;
            }

            return View(products.ToList());
        }
  
        [HttpGet]
        public IActionResult Index(string supplier, string category = "1")
        {
            
            var CategoryNr = int.Parse(category);
            //product category
            var list = ProductCategoryDaoMemory.GetInstance().GetAll().ToList();
            ViewData["ListCategory"] = list;
            // product suppliers
            var listOfSuppliers = SupplierDaoMemory.GetInstance().GetAll().ToList();
            ViewData["listOfSuppliers"] = listOfSuppliers;
            if (SessionHelper.GetObjectFromJson<List<LineItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<LineItem>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.product.DefaultPrice * item.Quantity);
                ViewBag.Quantity = cart.Sum(item => item.Quantity);
            }
            else
            {
                ViewBag.cart = null;
                ViewBag.total = 0;
                ViewBag.Quantity = 0;
            }
            if (supplier != null)
            {
                var SupplierNr = int.Parse(supplier);
                var products = ProductService.GetProductsForSupplier(SupplierNr);
                return View(products.ToList());
            }
            else
            {
                var products = ProductService.GetProductsForCategory(CategoryNr);
                return View(products.ToList());
            }
            
        }
        
        public IActionResult Buy(int id)
        {
            //var id = int.Parse(stringid);
            if (SessionHelper.GetObjectFromJson<List<LineItem>>(HttpContext.Session, "cart") == null)
            {
                List<LineItem> cart = new List<LineItem>();
                cart.Add(new LineItem { product = ProductService.GetProduct(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<LineItem> cart = SessionHelper.GetObjectFromJson<List<LineItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new LineItem { product = ProductService.GetProduct(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("cart")]
        public IActionResult Cart()
        {
            var cart = SessionHelper.GetObjectFromJson<List<LineItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.product.DefaultPrice * item.Quantity);
            ViewBag.Quantity = cart.Sum(item => item.Quantity);
            return View("cart");
        }

        public IActionResult Quantity()
        {
            var stringValues = Request.Form;
            ViewBag.q = stringValues["q"][1];


            return View("Index1");
        }


        private int isExist(int id)
        {
            List<LineItem> cart = SessionHelper.GetObjectFromJson<List<LineItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
