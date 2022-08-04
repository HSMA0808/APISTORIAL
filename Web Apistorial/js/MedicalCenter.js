window.addEventListener("load", ()=>{

  var btnBuscar = document.querySelector("#btnBuscar");
  btnBuscar.addEventListener("click", () => {
    var txtRnc = document.querySelector("#txtRncCentroMedico")
    var txtToken = document.querySelector("#txtTokenCentroMedico")
    if(txtRnc.value.trim() == "" || txtToken.value.trim() == "")
    {
      alert("Los campos 'Rnc' y 'Token' son requeridos");
    }
    else {
        fetch("https://localhost:44320/MedicalCenter/GetMedicalCenterStatus?RNC="+txtRnc.value+"&MedicalCenterToken=" + txtToken.value,{
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
            var div_DataView = document.querySelector("#div_DataView")
            var tbody_CentrosMedicos = document.querySelector("#tbody_CentrosMedicos")
            tbody_CentrosMedicos.innerHTML = ""
            var tr = document.createElement("tr");
            var td_RNC = document.createElement("td");
            var td_centroMedico = document.createElement("td");
            var td_nombreContacto = document.createElement("td");
            var td_telefono1 = document.createElement("td");
            var td_email1 = document.createElement("td");
            var td_estatus = document.createElement("td");
            td_RNC.innerText = data.centroMedico.rnc
            td_centroMedico.innerText = data.centroMedico.centroMedico
            td_nombreContacto.innerText = data.centroMedico.nombreContacto
            td_telefono1.innerText = data.centroMedico.telefono1
            td_email1.innerText = data.centroMedico.email1
            td_estatus.innerText = data.centroMedico.estatusCenterDescripcion
            tr.id = data.centroMedico.idCentroMedico
            tr.append(td_RNC)
            tr.append(td_centroMedico)
            tr.append(td_nombreContacto)
            tr.append(td_telefono1)
            tr.append(td_email1)
            tr.append(td_estatus)
            tbody_CentrosMedicos.append(tr)
            div_DataView.hidden = false;
          }
        })
    }
  })
})
