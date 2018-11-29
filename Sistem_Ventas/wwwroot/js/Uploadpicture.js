class Uploadpicture {
    constructor() {

    }
    //método archivo parametros evt es el evento de nuestro input file y id de la foto o archivo 
    archivo(evt, id) {
        //almacenaremos el archivo en el objeto files
        let files = evt.target.files; // Filelist Object
        let f = files[0];
        //solamente me devolvera true si es un archivo tipo imagen
        if (f.type.match('image.*')) {

            let reader = new FileReader();
            reader.onload = ((theFile) => {
                return (e) => {
                    document.getElementById(id).innerHTML = ['<img class="responsive-img " src="',
                        e.target.result, '" title="', escape(theFile.name), '"/>'].join('');
                }
            })(f);
            reader.readAsDataURL(f);
        }
    }
}