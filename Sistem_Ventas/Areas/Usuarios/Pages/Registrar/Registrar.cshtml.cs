using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistem_Ventas.Areas.Usuarios.Models;
using Sistem_Ventas.Library;

namespace Sistem_Ventas.Areas.Usuarios.Pages.Registrar
{
    public class RegistrarModel : PageModel
    {
        private ListObject listObject = new ListObject();
        //este objeto lo quitamos porqué vamos a acceder a travésdel objeto ListObject
        //private LUsuarios _usuarios;
        //Mostrar información del usuario 
        public RegistrarModel(RoleManager<IdentityRole> roleManager)
        {
            listObject._roleManager = roleManager;
            listObject._usuarios = new LUsuarios();
            listObject._usersRole = new UserRoles();
        }
        public void OnGet()
        {
            //_usuarios = new LUsuarios();
            //ViewData["Roles"] = _usuarios.userData(HttpContext);
            Input = new InputModel
            {
                //de esta manera obtenemos los roles con este metodo pasandole el 
                rolesLista = listObject._usersRole.getRoles(listObject._roleManager)
            };
        }
        //hay que enlazar las propiedades del InputModel de la vista con la parte backend 
        [BindProperty]
        public InputModel Input { get;set; }
        //aqui en esta clase InputModel vamos a escribir las propiedades que no contengan los mensajes personalizados
        public class InputModel : InputModelRegistrar
        {
            [Required]
            public string Role { get; set; }
            public IFormFile AvatarImage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //el nombre de la imagen contendra el email del usuario .png
                var imageName = Input.Email + ".png";
                //realizar un ejemplo
                var data = Input.AvatarImage;
                var roleLista = Input.Role;
                /* 
                 * aqui verificamos que rolesLista no este a null si lo esta le damos los valores que tiene,
                 * que tener es la aplicación. 
                */

                
            }
            catch (Exception ex)
            {
                throw;
            }
            return Page();
        }
    }
}