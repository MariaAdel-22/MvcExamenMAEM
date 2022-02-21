using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcExamenMAEM.Extensions;
using PracticaCore2MAEM.Models;
using PracticaCore2MAEM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PracticaCore2MAEM.Controllers
{
    public class ManageController : Controller
    {
        private RepositoryExamen repo;

        public ManageController(RepositoryExamen repo)
        {

            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string psswd)
        {

            Usuario usu = this.repo.FindUsuario(username,psswd);

            if (usu != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                Claim ClName = new Claim(ClaimTypes.Name, username);
                Claim ClPss = new Claim(ClaimTypes.NameIdentifier, psswd);
        
                identity.AddClaim(ClName);
                identity.AddClaim(ClPss);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                string id = TempData["id"].ToString();

                HttpContext.Session.SetObject("Usuario",usu);

                return RedirectToAction(action, controller, new { id = id });
            }
            else
            {
                return RedirectToAction("Login", "Manage");
            }
        }

        public IActionResult CerrarSesion() {

            HttpContext.Session.Remove("Usuario");

            return View("Login");
        }
    }
}
