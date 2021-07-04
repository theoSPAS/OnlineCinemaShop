using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Domain.DomainModels;
using OnlineCinema.Domain.Identity;
using OnlineCinema.Services.Interface;

namespace OnlineCinema.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<OnlineCinemaTicketUser> userManager;

        public AdminController(IOrderService orderService, UserManager<OnlineCinemaTicketUser> userManager)
        {
            this._orderService = orderService;
            this.userManager = userManager;
        }

        [HttpGet("[action]")]
        public List<Order> GetOrders()
        {
            return this._orderService.GetOrders();
        }


        [HttpPost("[action]")]
        public Order GetDetailsForTicket(BaseEntity model)
        {
            return this._orderService.GetOrderDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportUsers(List<UserRegistrationDto> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var userExists = userManager.FindByEmailAsync(item.Email).Result;

                if (userExists == null)
                {
                    var user = new OnlineCinemaTicketUser
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserCart = new ShoppingCart()
                    };
                    var result = userManager.CreateAsync(user, item.Password).Result;

                 
                    if(item.Role == "Customer")
                    {
                         userManager.AddToRoleAsync(user, "Customer").Wait();
                    }
                    else if(item.Role == "Administrator")
                    {
                            userManager.AddToRoleAsync(user, "Administrator").Wait();
                    }
                    


                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return status;
        }

    }
}
