using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Extensions;
using Lab2.Interfaces;
using Lab2.Models;
using Lab2.Models.Cart;
using Lab2.ViewModels.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class CartController : Controller
    {

        private IKitchen service;
        //private IOrderProcessor orderProcessor;

        public CartController(IKitchen service)
        {
            this.service = service;
            
        }

        public IActionResult Index(Cart cart)
        {

            CartViewModel model = new CartViewModel
            {
                Cart = cart,
                ReturnUrl = ""
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult AddToCart(Cart cart,int dishId, string returnUrl)
        {
            Dish dish = service.GetDish(dishId).Result;
            
            Cart newcart;
            if (dish != null)
            {
                cart.AddItem(dish, 1);
                newcart = cart;
            }

            return RedirectToAction("Index",  new {  });

        }
        [HttpPost]
        public IActionResult RemoveFromCart(Cart cart,int dishId, string returnUrl)
        {
            Dish dish = service.GetDish(dishId).Result;

            if (dish != null)
            {
                cart.RemoveLine(dish);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.Set("Cart", cart);
            }
            return cart;
        }
        //[HttpPost]
        //public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        //{
        //    if (cart.Lines.Count() == 0)
        //    {
        //        ModelState.AddModelError("", "Извините, ваша корзина пуста");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        orderProcessor.ProcessOrder(cart, shippingDetails);
        //        cart.ClearCart();
        //        return View("Completed");
        //    }
        //    else
        //        return View(shippingDetails);
        //}

        //public PartialViewResult Summary(Cart cart)
        //{
        //    return PartialView(cart);
        //}

        //public ViewResult Checkout()
        //{
        //    return View(new ShippingDetails());
        //}
    }
}