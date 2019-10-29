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
        private Cart cart;
        //private IOrderProcessor orderProcessor;

        public CartController(IKitchen service,Cart cart)
        {
            this.service = service;
            this.cart = cart;
            
        }

        public IActionResult Index()
        {
            //var car = TempData.Get<Cart>("cart");
            var items = cart.GetCartItems();
            cart.Lines = items;
            CartViewModel model = new CartViewModel
            {
                Cart = cart,
               
            };
            return View(model);
        }




        
        public RedirectToActionResult AddToCart(int dishId)
        {
            Dish dish = service.GetDish(dishId).Result;
            
           // Cart newcart;
            if (dish != null)
            {
                cart.AddToCart(dish, 1);
               
            }

            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult RemoveFromCart(int dishId)
        {
            Dish dish = service.GetDish(dishId).Result;

            if (dish != null)
            {
                cart.RemoveLine(dish);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveAllFromCart()
        {
            cart.Clear();
            return RedirectToAction("Index", "Home");
        }

        public Cart GetCart()
        {
            Cart cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
               // cart = new Cart();
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