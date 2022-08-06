window.addEventListener("load", ()=>{

})
var urlAPI = document.querySelector("#APIURL")
var btnBuscar = document.querySelector("#btnBuscar");
btnBuscar.addEventListener("click", () => {
  var select_Estatus = document.querySelector("#select_Estatus")
  if(select_Estatus.value != 0 && select_Estatus.value != 6 && select_Estatus.value != 7)
  {
    alert("El valor asignado al dropdown es incorrecto: " + select_Estatus.value);
  }
  else {
      fetch(urlAPI.value + "MedicalCenter/GetmMedicalCenterList?ApistorialKey=TestKey&Estatus=" + select_Estatus.value,{
        method: 'GET',
        mode: 'cors',
      })
      .then(response => response.json())
      .then(data => {
        if(data.responseCode != "00")
        {
          alert("Respuesta del API: " + data.message)
        }
        else {
          var tbody_CentrosMedicos = document.querySelector("#tbody_CentrosMedicos")
          tbody_CentrosMedicos.innerHTML = ""
          let i = 0;
          for(i in data.centrosMedicos)
          {
            var div_DataView = document.querySelector("#div_DataView")
            var tr = document.createElement("tr");
            var td_RNC = document.createElement("td");
            var td_centroMedico = document.createElement("td");
            var td_nombreContacto = document.createElement("td");
            var td_telefono1 = document.createElement("td");
            var td_email1 = document.createElement("td");
            var td_estatus = document.createElement("td");
            td_RNC.innerText = data.centrosMedicos[i].rnc
            td_centroMedico.innerText = data.centrosMedicos[i].centroMedico
            td_nombreContacto.innerText = data.centrosMedicos[i].nombreContacto
            td_telefono1.innerText = data.centrosMedicos[i].telefono1
            td_email1.innerText = data.centrosMedicos[i].email1
            td_estatus.innerText = data.centrosMedicos[i].estatusCenterDescripcion
            tr.id = data.centrosMedicos[i].idCentroMedico
            tr.append(td_RNC)
            tr.append(td_centroMedico)
            tr.append(td_nombreContacto)
            tr.append(td_telefono1)
            tr.append(td_email1)
            tr.append(td_estatus)
            tbody_CentrosMedicos.append(tr)
            div_DataView.hidden = false;
          }
        }
      })
  }
})
