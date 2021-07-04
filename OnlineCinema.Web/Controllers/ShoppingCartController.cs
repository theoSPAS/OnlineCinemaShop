using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain.DomainModels;
using OnlineCinema.Domain.DTO;
using OnlineCinema.Domain.Identity;
using OnlineCinema.Repository;
using OnlineCinema.Services.Interface;
using Stripe;

namespace OnlineCinema.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult Index()
        {
            var userId = User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._shoppingCartService.GetShoppingCartInfo(userId));
        }

        public IActionResult RemoveFromCart(Guid id)
        {
            var userId = User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.DeleteTicketFromShoppingCart(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        public IActionResult Pay(string stripeEmail, string stripeToken) { 
        
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.GetShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "Online Tickets Payment",
                Currency = "$",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var result = this.Order();

                if (result)
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
                else
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        private Boolean Order()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.OrderNow(userId);

            return result;
        }
      
    }
}
