using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sistem_Ventas.Areas.Principal.Controllers;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;

namespace Sistem_Ventas.Controllers
{
    public class HomeController : Controller
    {
        //campo serviceProvider
        // al poner el campo serviceProvider ya funciona la aplicación antes no me funcionaba porqué no ponia este campo
        //private SignInManager<IdentityUser> signInManager;
        //private UserManager<IdentityUser> userManager;
        //private RoleManager<IdentityRole> roleManager;
        //private ApplicationDbContext context;
        //IServiceProvider _serviceProvider;
        private LUsuarios _usuarios;
        private SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, 
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            //_serviceProvider = serviceProvider;
            //ejecutarTareaAsync();
            _signInManager = signInManager;
            _usuarios = new LUsuarios(userManager, signInManager, roleManager, context);
        }
        // aqui lo ponga como lo ponga nofunciona como me has sugerido en el video 8 del curso de sistema de ventas
        
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(PrincipalController.Index), "Principal");
            }
            else
            {
                return View();
            }
            //cuando cargamos la página con el Index() se ejecuta el método CreateRoles()
            //await CreateRoles(_serviceProvider);
            
        }
        
        //este método solo se va a ejecutar cuando realizamos peticiones por POST
        //este atributo indicamos que solo se ejecute por POST
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModels model)
        {
            if (ModelState.IsValid)
            {
                List<object[]> listObject = await _usuarios.userLogin(model.Input.Email, model.Input.Password);
                object[] objects = listObject[0];
                var _identityError = (IdentityError)objects[0];
                model.ErrorMessage = _identityError.Description;
                if (model.ErrorMessage.Equals("True"))
                {
                    //serializar la información del usuario 
                    var data = JsonConvert.SerializeObject(objects[1]);
                    //crear variables de sesión
                    HttpContext.Session.SetString("User", data);
                    return RedirectToAction(nameof(PrincipalController.Index), "Principal");
                }
                else
                {
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //metodo para crear los roles asincronico 
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            string mensaje;
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                //a la hora de crear los roles, quiero introducir SuperAdmin, Admin, Manager por zona, User
                // aqui quiero crear otros roles de usuarios para cuando acabe con el proyecto
                String[] rolesName = { "Admin", "User" };
                foreach (var item in rolesName)
                {
                    //esto nos sirve para verificar que cada rol existe en la tabla de roles 
                    //es un metodo asincronico que le indicamos que mientras no acabe este metodo, no puede acabar el CreateRoles
                    var roleExist = await roleManager.RoleExistsAsync(item);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(item));
                    }
                }
                var user = await userManager.FindByIdAsync("9c059b10-1155-4177-bfb0-ef31fdfe8767");
                var addRole = await userManager.AddToRoleAsync(user, "Admin");
                
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            

        }
        private async void ejecutarTareaAsync()
        {
            var data = await Tareas();
            String Tarea = "Ahora ejectaremos las demas lineas de código porque la tarea a finalizado";


        }
        //método que va a sincronizarse y que ejecutara una tarea
        private async Task<String> Tareas()
        {
            //Pausa de 5 segundos
            Thread.Sleep(5 * 1000);
            String tarea = "Tarea finalizada";
            return tarea;
        }
    }
}
