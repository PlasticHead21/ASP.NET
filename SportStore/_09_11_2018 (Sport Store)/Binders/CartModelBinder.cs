using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _09_11_2018__Sport_Store_.Binders
{
    public class CartModelBinder: IModelBinder
    {
        private const string SESSION_KEY = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = (Cart)controllerContext.HttpContext.Session[SESSION_KEY];
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[SESSION_KEY] = cart;
            }
            return cart;
        }
    }
}