using Microsoft.AspNetCore.Mvc;
using MyVet.Web.Helpers;
using MyVet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;

        public AccountController(IUserHelper userHelper)
        {
            this._userHelper = userHelper;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Sobrecarga al metodo login (Post), se sobrecarga cambiando el/los parametros
        //hace que el login funcione al presionar el boton
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //aqui vemos que el modelo es valido(que ingresen lo que se requiere)
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "User or password not valid.");
            }
            //si el password no es valido devolvemos el modelo con lo digitado
            //asi no tienen que volver a poner el email
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home"); //vista Index del controlador Home
        }
    }
}
