using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Domain.DomainModels;
using OnlineCinema.Domain.DTO;
using OnlineCinema.Domain.Identity;

namespace OnlineCinema.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<OnlineCinemaTicketUser> _userManager;

        public object UserRoleDTO { get; private set; }

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<OnlineCinemaTicketUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CinemaRole role)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);
            if(!roleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }
            return View();
        }

        public IActionResult List()
        {
            List<OnlineCinemaTicketUser> users = _userManager.Users.ToList();

            List<string> roles = _roleManager.Roles.Select(x => x.Name).ToList();

            RoleUserDTO dto = new RoleUserDTO
            {
                Users = users,
                Roles = roles
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> List(string User, string Role)
        {
            var existUser = _userManager.FindByEmailAsync(User);

           var  existRole = _roleManager.RoleExistsAsync(Role);

            if (existRole != null && existUser == null)
            {
                _userManager.AddToRoleAsync(existUser, existRole).Wait();
            }
        }
    }
}
