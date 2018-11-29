// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//este método usarlo en JS ES6 he usado Jquery es lo que usa el creador del curso pero adaptarlo a JS ES6
/* Código principal */
var principal = new Principal();

/* Código Usuarios */
var usuarios = new Usuarios();
//funcion que recibe como parametro evt 
var imageUser = (evt) => {
    usuarios.archivo(evt, "imageRegistrar");
}

$().ready(() => {
    let URLActual = window.location.pathname;
    principal.userLink(URLActual)
    //$('.sidenav').sidenav();
    M.AutoInit();

    if (URLActual == "/Usuarios/Registrar/Registrar" || URLActual == "/Usuarios/Registrar/Registrar/") {
        document.getElementById('files').addEventListener('change', imageUser, false);
    }
});

