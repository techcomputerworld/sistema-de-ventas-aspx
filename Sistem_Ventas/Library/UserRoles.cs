using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class UserRoles : ListObject
    {
        //private SelectListItem _userRoles;

        public UserRoles()
        {
            _userRoles = new List<SelectListItem>();
        }
        public async Task<List<SelectListItem>> GetRole (UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, string ID)
        {
            //aqui obtenemos el usuario con el ID
            var users = await userManager.FindByIdAsync(ID);
            //aquí obtenemos los roles de los usuarios de users
            var roles = await userManager.GetRolesAsync(users);
            if (roles.Count.Equals(0))
            {
                _userRoles.Add(new SelectListItem
                {
                    Value = "0",
                    Text = "No Role"
                });
            }
            else
            {
                var roleUser = roleManager.Roles.Where(m => m.Name.Equals(roles[0]));
                foreach (var Data in roleUser)
                {
                    _userRoles.Add(new SelectListItem
                    {
                        Value = Data.Id,
                        Text = Data.Name
                    });
                }
            }
            
            return _userRoles;
            /*
            no se puede convertir implicitamente el tipo 
            System.Collection.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>' en 
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            */
        }
        public List<SelectListItem> getRoles (RoleManager<IdentityRole> roleManager)
        {
            //Roles representa a la tabla Roles de la base de datos
            var roles = roleManager.Roles.ToList();
            roles.ForEach (item =>
             {
                 _userRoles.Add(new SelectListItem
                 {
                     Value = item.Id,
                     Text = item.Name
                 });
                 
             });
            return _userRoles;
        }
    }
}
