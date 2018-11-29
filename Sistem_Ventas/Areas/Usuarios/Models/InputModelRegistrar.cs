using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Usuarios.Models
{
    public class InputModelRegistrar
    {
        [Required(ErrorMessage = "<font color='red'> El campo nombre es obligatorio </font>")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "<font color='red'> El campo apellido es obligatorio </font>")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "<font color='red'> El campo DNI es obligatorio </font>")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "<font color='red'> El campo de contraseña es obligatorio. </font>")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "<font color='red'> El campo correo electrónico es obligatorio. </font>")]
        [EmailAddress(ErrorMessage = "<font color='red'> El campo de correo electrónico no es una dirección de correo " +
            "electrónico válida.</font> ")]
        //[DataType(DataType.PhoneNumber)]
        public string Email { get; set; }

        [Required(ErrorMessage = "<font color='red'> El campo contraseña es obligatorio. </font>")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "<font color='red'> El numero de caracteres de {0} debe ser " +
            "al menos {2} ", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
