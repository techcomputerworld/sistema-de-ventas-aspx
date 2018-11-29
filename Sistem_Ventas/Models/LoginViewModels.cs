using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Sistem_Ventas.Models
{
    public class LoginViewModels
    {
        //este atributo sirve para poder vincular las propiedades de la clase Input 
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            //invocaremos una propiedad 
            [Required(ErrorMessage ="<font color='red'> El campo de correo electrónico es obligatorio. </font>")]
            [EmailAddress(ErrorMessage ="<font color='red'> El correo electrónico no es una dirección de correo " +
                "electrónico válida.</font> ")]
            public string Email { get; set; }

            [Required(ErrorMessage = "<font color='red'> El campo de contraseña es obligatorio. </font>")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "<font color='red'>El númerode caracteres del {0} debe ser al menos {2}")]
            public string Password { get; set; }
        }
    }
}
