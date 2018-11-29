using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sistem_Ventas.Controllers
{
    /*Dependiendo del atributo Route que se lo pondremos en la clase Controller podremos ejecutar nuestros controladores
     de la misma manera que estamos haciendo aquí */
    // no nos va a hacer falta por el método que hemos puesto app.UseStatusCodePagesWithReExecute
    //[Route("/Error")]
    public class ErrorController : Controller
    {
        //esta clase no tendra método de accion llamado Index
        //este método captura datos por Get por defecto porqué no le hemos especificado lo contrario
        //int? especificamos que es una variable entera y que permite valores tipo null
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404 || statusCode.Value == 500)
                {
                    ViewData["Error"] = statusCode.ToString();
                }
            }
            
            return View();
        }
    }
}