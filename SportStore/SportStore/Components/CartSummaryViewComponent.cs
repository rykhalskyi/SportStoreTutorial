﻿using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        Cart _cart;

        public CartSummaryViewComponent(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
