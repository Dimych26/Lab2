using Lab2.Extensions;
using Lab2.Models.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Lab2.Infrastructure
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ControllerContext ControllerContext;
        public CartModelBinder(IHttpContextAccessor _httpContextAccessor, ControllerContext ControllerContext)
        {
            this._httpContextAccessor = _httpContextAccessor;
            this.ControllerContext = ControllerContext;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Получить объект Cart из сеанса
           Task<Cart> cart = null;
            if (ControllerContext.HttpContext.Session!=null)//_httpContextAccessor.HttpContext.Session != null)
            {
                cart = Task.FromResult(ControllerContext.HttpContext.Session.Get<Cart>(sessionKey));//_httpContextAccessor.HttpContext.Session.Get<Cart>(sessionKey));

            }

            // Создать объект Cart если он не обнаружен в сеансе
            if (cart == null)
            {
                cart = new Task<Cart>(()=>CreateCart());
                if (ControllerContext.HttpContext.Session!=null)//_httpContextAccessor.HttpContext.Session != null)
                {
                    ControllerContext.HttpContext.Session.Set(sessionKey, cart); //_httpContextAccessor.HttpContext.Session.Set(sessionKey,cart);
                }
            }

            // Возвратить объект Cart
            return cart;
        }

        

        private Cart CreateCart()
        {
            return new Cart();
        }
    }
}
