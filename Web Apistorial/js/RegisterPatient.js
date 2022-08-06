var urlAPI = document.querySelector("#APIURL")
var txtPrimerNombre = document.querySelector("#txtPrimerNombre")
var txtSegundoNombre = document.querySelector("#txtSegundoNombre")
var txtApellidos = document.querySelector("#txtApellido")
var select_Sexo = document.querySelector("#dpSexo")
var select_TipoIdentificacion = document.querySelector("#dpTipoIdentificacion")
var txtNumeroIdentificacion = document.querySelector("#txtNumeroIdentificacion")
var select_TipoSangre = document.querySelector("#dpTipoSangre")
var txtTelefono1 = document.querySelector("#txtTelefono1")
var txtTelefono2 = document.querySelector("#txtTelefono2")
var txtEmail = document.querySelector("#txtEmail")
var txtDireccion1 = document.querySelector("#txtDireccion1")
var txtDireccion2 = document.querySelector("#txtDireccion2")


var btnGuardar = document.querySelector("#btnGuardar")

btnGuardar.addEventListener("click", ()=>{
    if(txtPrimerNombre.value.trim() == "" /*|| txtApellidos.value.trim() == "" || select_TipoIdentificacion.value == 0 || select_TipoSangre.value == 0 || txtNumeroIdentificacion.value.trim() =="" || txtTelefono1.value.trim() == "" || txtEmail.value.trim() == "" || txtDireccion1.value.trim() || select_Sexo.value == 0*/)
    {
      alert("Los siguientes campos son requeridos: Primer Nombre, Apellidos, Sexo, Tipo de Identificacion, Numero de Identificacion, Telefono 1, Email, Direccion 1")
    }
    else {
      var datos = {idpatient: 1,  medicalCenter_Token: "ASDJKHAUIOF", primerNombre: txtPrimerNombre.value, segundoNombre: txtSegundoNombre.value, apellidos: txtApellidos.value, sexo: select_Sexo.value, codigo_TipoIdentificacion: select_TipoIdentificacion.value, numeroIdentificacion: txtNumeroIdentificacion.value, direccion1: txtDireccion1.value, direccion2: txtDireccion2.value, codigo_TipoSangre: select_TipoSangre.value, telefono1: txtTelefono1.value, telefono2: txtTelefono2.value, email: txtEmail.value}
      fetch(urlAPI.value + "Patient/RegisterPatient", {
      method: 'POST', // *GET, POST, PUT, DELETE, etc.
      mode: 'cors', // no-cors, *cors, same-origin
      //cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
      //credentials: 'same-origin', // include, *same-origin, omit
      headers: {
        'Content-Type': 'application/json'
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      //redirect: 'follow', // manual, *follow, error
      //referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
      body: JSON.stringify(datos) //JSON.parse('{"descripcion": "'+txtNombre.value+'", "rnc": "'+txtRNC.value+'", "tel1": "'+txtTelefono1.value+'", "tel2": "'+txtTelefono2.value+'", "email1": "'+txtEmail1.value+'", "email2": "'+txtEmail2.value+'", "nombreContacto": "'+txtNombreContactoCM.value+'", "referencia": "ApistorialWeb"}') // body data type must match "Content-Type" header
    })
    .then(response=>response.json())
    .then(data => {
      console.log(data)
      if(data.responseCode != "00")
      {
        alert("Algo no ha salido bien, respuesta del API: " + data.message)
      }
      else {
        limpiarCampos(txtPrimerNombre, txtSegundoNombre, txtApellidos, select_Sexo, select_TipoIdentificacion, txtNumeroIdentificacion, select_TipoSangre, txtTelefono1, txtTelefono2, txtEmail, txtDireccion1, txtDireccion2)
        alert("Paciente registrado satisfcatoriamente")
        window.location = window.location.origin + "/index.html"
      }
    })
  }
})

function limpiarCampos(...campos)
{
  let i = 0;
  for(i in campos)
  {
    campos[i].value = "";
  }
}
