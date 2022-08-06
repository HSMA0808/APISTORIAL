var urlAPI = document.querySelector("#APIURL")
var txtNombre = document.querySelector("#txtNombreCM")
var txtRNC = document.querySelector("#txtRNC")
var txtTelefono1 = document.querySelector("#txtTelefonoCM1")
var txtTelefono2 = document.querySelector("#txtTelefonoCM2")
var txtEmail1 = document.querySelector("#txtEmailCM1")
var txtEmail2 = document.querySelector("#txtEmailCM2")
var txtNombreContactoCM = document.querySelector("#txtNombreContactoCM")


var btnGuardar = document.querySelector("#btnGuardar")

btnGuardar.addEventListener("click", ()=>{
    if(txtNombre.value.trim() == "" || txtRNC.value.trim() == "" || txtTelefono1.value.trim() == "" || txtEmail1.value.trim() == "" || txtNombreContactoCM.value.trim() =="")
    {
      alert("Los siguientes campos son requeridos: 'Nombre del Centro Medico', 'RNC', 'Telefono 1', 'Email 1', 'Nombre Contacto'")
    }
    else {
      var datos = {descripcion: txtNombre.value, rnc: txtRNC.value, tel1: txtTelefono1.value, tel2: txtTelefono2.value, email1: txtEmail1.value, email2: txtEmail2.value, nombreContacto: txtNombreContactoCM.value, referencia: "ApistorialWeb"}
      fetch(urlAPI.value + "MedicalCenter/Register", {
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
        limpiarCampos(txtNombre, txtRNC, txtTelefono1, txtTelefono2, txtEmail1, txtEmail2, txtNombreContactoCM)
        alert("Centro Medico guardado satisfcatoriamente")
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
