using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistem_Ventas.Controllers;
using Sistem_Ventas.Library;

namespace Sistem_Ventas.Areas.Usuarios.Controllers
{
    [Authorize]
    [Area("Usuarios")]
    public class UsuariosController : Controller
    {
        private LUsuarios _usuarios;
        private SignInManager<IdentityUser> _signInManager;
        /* vamos a obtener el rol del usuario  de otra manera, que espero sea mejor que la anterior, porqué la anterior no 
         termina de convencerme a través del objeto User */
        public UsuariosController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _usuarios = new LUsuarios();
        }
        public IActionResult Index()
        {
            //quiero que se muestre el rol del usuario aquí también
            if (_signInManager.IsSignedIn(User))
            {
                /*
                var data = User.Claims.FirstOrDefault(u => u.Type.Equals("" +
                    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")).Value;
                */
                //ViewData["Roles"] = _usuarios.userData(HttpContext);
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        public async Task<IActionResult> SessionClose()
        {
            //eliminar toda la información del usuario que ha iniciado sesion y retornar a la vista 
            //de inicio de sesión.
            //remover la variable de sesión 
            HttpContext.Session.Remove("User");
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}