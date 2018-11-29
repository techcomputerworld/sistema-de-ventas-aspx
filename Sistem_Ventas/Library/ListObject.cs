using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistem_Ventas.Data;
using Sistem_Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class ListObject
    {
        public String description, code;

        public UserRoles _usersRole;
        public UserData _userData;
        public LUsuarios _usuarios;
        public IdentityError _identityError;
        public ApplicationDbContext _context;

        public List<SelectListItem> _userRoles;

        public RoleManager<IdentityRole> _roleManager;
        public UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> _signInManager;

        public List<object[]> dataList = new List<object[]>();
    }
}
