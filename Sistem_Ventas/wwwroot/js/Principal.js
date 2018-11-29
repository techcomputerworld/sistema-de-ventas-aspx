class Principal {
    constructor() {

    }
    userLink(URLActual) {
        if (URLActual == "/Principal" || URLActual == "/Principal/") {
            document.getElementById("enlace1").classList.add('active');
        }
        if (URLActual == "/Usuarios" || URLActual == "/Usuarios/") {
            document.getElementById("enlace2").classList.add('active');
        }
        if (URLActual == "/Usuarios/Registrar/Registrar" || URLActual == "/Usuarios/Registrar/Registrar/") {
            document.getElementById("enlace2").classList.add('active');
        }
        /*
        if (URLActual == "/Usuarios/registrar/registrar" || URLActual == "/Usuarios/registrar/registrar/") {
            document.getElementById("enlace2").classList.add('active');
        */
    }
}