using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Interfaces;
using Lab2.Models;
using Lab2.Models.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService orderService;
        private Cart cart;
        public OrderController(IOrderService orderService, Cart cart)
        {
            this.orderService = orderService;
            this.cart = cart;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            cart.Lines = cart.GetCartItems();

            if (cart.Lines.Count == 0)
            {
                ModelState.AddModelError("", "У вас нет товаров");
            }

            if(ModelState.IsValid)
            {
                orderService.MakeOrder(order);
                return RedirectToAction("Complete");
            }

            return View(order);
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }
    }
}