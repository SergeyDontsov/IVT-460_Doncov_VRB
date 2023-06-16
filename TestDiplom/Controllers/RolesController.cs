using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TestDiplom.Models;
using Microsoft.AspNetCore.Authorization;
using TestDiplom.Areas.Identity.Data;

using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;

namespace TestDiplom.Controllers
{

    public class RoleEdit
    {
        public IdentityRole? Role { get; set; }
        public IEnumerable<TestAuth>? Members { get; set; }
        public IEnumerable<TestAuth>? NonMembers { get; set; }
    }
    public class RolesController : Controller
    {
        private static readonly ILogger<HomeController> _logger;
        private TestAuth context;

        RoleManager<IdentityRole> _roleManager;

        UserManager<IdentityUser> userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> _userManager)
        {
            _roleManager = roleManager;
            userManager = _userManager;

        }
        
        public IActionResult UserRole() => View(_roleManager.Roles.ToList());


        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("UserList");
        }

        public IActionResult UserList() => View(userManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            IdentityUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                await userManager.UpdateAsync(user);
                return View(model);
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            IdentityUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await userManager.AddToRolesAsync(user, addedRoles);
                await userManager.UpdateSecurityStampAsync(user);
                await userManager.RemoveFromRolesAsync(user, removedRoles);
                await userManager.UpdateSecurityStampAsync(user);
                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}

