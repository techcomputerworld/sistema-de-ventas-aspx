using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistem_Ventas.Data;
using Sistem_Ventas.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Sistem_Ventas.Library
{
    public class LUsuarios : ListObject
    {
        //Método constructor que haremos una sobrecarga del método con varios constructores
        public LUsuarios()
        {

        }
        public LUsuarios(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _usersRole = new UserRoles();
        }
        public LUsuarios(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _usersRole = new UserRoles();
        }
        internal async Task<List<object[]>> userLogin(string email, string password)
        {
            try
            {
                //este es el proceso de Login de la aplicación realmente
                var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
                //programar que por ejemplo en 5 inicios de sesion no validos se bloquee la cuenta y se desbloquee  en x tiempo.
                if (result.Succeeded)
                {
                    /*lo que hacemos es una consulta con el email que le  estamos pasando con el metodo Equals que igual a 
                        al dato alcenado en la tabla en el campo email. Si se cumple la condición almacenaremos el usuario en
                        appUser
                     */
                    var appUser = _userManager.Users.Where(u => u.Email.Equals(email)).ToList();
                    //método _usersRole.GetRole que acabamos de crear en nuestra propia clase UserRoles
                    //comprobar depurando 
                    _userRoles = await _usersRole.GetRole(_userManager, _roleManager, appUser[0].Id);
                    //usaremos UserRoles
                    _userData = new UserData
                    {
                        Id = appUser[0].Id,
                        Role = _userRoles[0].Text,
                        UserName = appUser[0].UserName
                    };
                    code = "0";
                    description = result.Succeeded.ToString();

                }
                else
                {
                    code = "1";
                    //quiero mostrar el código de error
                    description = "Correo o contraseña inválidos";
                }
            }
            catch (Exception ex)
            {
                code = "-1";
                description = ex.Message;
            }
            _identityError = new IdentityError
            {
                Code = code,
                Description = description
            };
            object[] data = { _identityError, _userData };
            dataList.Add(data);
            return dataList;
        }
        //Método para obtener el rol del usuario, más adelante obtendremos mas datos
        public String userData(HttpContext HttpContext)
        {
            String role = null;
            String user = HttpContext.Session.GetString("User");
            if (user != null)
            {
                //va a deseializar el dato en tipo string y lo va a convertir en UserData
                UserData dataItem = JsonConvert.DeserializeObject<UserData>(user.ToString());
                role = dataItem.Role;

            }
            else
            {
                role = "No data";
            }
            return role;
        }
    } //fin de la clase Usuarios
    
    
} //fin del namespace
