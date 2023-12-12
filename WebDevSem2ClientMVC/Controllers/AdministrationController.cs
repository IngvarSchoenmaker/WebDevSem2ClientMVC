using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System.Data;
using System.Security.Claims;
using WebDevSem2ClientMVC.Areas.Identity.Data;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Controllers
{
    public class UserView
    {
        public ApplicationUser User { get; set; }
        // all roles from website
        public IEnumerable<string?> Roles { get; set; } = new List<string?>();
        // Roles of user
        public IEnumerable<string?> Claims { get; set; } = new List<string?>();
        public IEnumerable<string?> NewRoles { get; set; } = new List<string?>();
    }
    [Authorize(Roles = "Admin,Manager")]
    public class AdministrationController : Controller
    {
        private readonly ILogger _logger; 
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdministrationController(ILogger<AdministrationController> logger, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var users = _userManager.Users;
            _logger.LogInformation("This is the home page");
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) { return NotFound(); }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) { return NotFound(); }
            IEnumerable<string?> claims = await _userManager.GetRolesAsync(user);
            IEnumerable<string?> roles = _roleManager.Roles.Select(x => x.Name).ToList();
            return View(new UserView() { User = user, Roles = roles, Claims = claims });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserView userView)
        {
            _logger.LogInformation("edit as generated requested for {@userId}, {@DateTime}", id, DateTime.Now);
            if (id != userView.User.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    var roles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, roles);
                    await _userManager.AddToRolesAsync(user, userView.NewRoles);
                    await _userManager.UpdateAsync(user);

                }
                catch (Exception ex)
                {
                    _logger.LogError("Error in AdministationController with the edit call on {@DateTime} with error {@error}", DateTime.Now, ex);

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userView);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            try
            {
                await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AdministationController with the Delete call on {@DateTime} with error {@error}", DateTime.Now, ex);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> RemovePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            try
            {
                await _userManager.RemovePasswordAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AdministationController with the RemovePassword call on {@DateTime} with error {@error}", DateTime.Now, ex);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
