window.addEventListener("load", ()=>{

})
var urlAPI = document.querySelector("#APIURL")
loadData()
var btnBuscar = document.querySelector("#btnBuscar");
var btnNuevoPaciente = document.querySelector("#btnNuevoPaciente");

btnNuevoPaciente.addEventListener("click", () => {
  window.location = window.location.origin + "/RegisterPatient.html"
})
function loadData() {
      fetch(urlAPI.value + "Patient/GetPatients?MedicalCenter_Token=MFHTWHAUIOF",{
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
          console.log(data)
          var tbody_Pacientes = document.querySelector("#tbody_Pacientes")
          tbody_Pacientes.innerHTML = ""
          let i = 0;
          for(i in data.pacientes)
          {
            var div_DataView = document.querySelector("#div_DataView")
            var tr = document.createElement("tr");
            var td_Nombre = document.createElement("td");
            var td_TipoIdentificacion = document.createElement("td");
            var td_NumeroIdentificacion = document.createElement("td");
            var td_telefono1 = document.createElement("td");
            var td_TipoSangre = document.createElement("td");
            var td_Email = document.createElement("td");
            td_Nombre.innerText = data.pacientes[i].primerNombre + " " + data.pacientes[i].segundoNombre + " " + data.pacientes[i].apellidos
            td_TipoIdentificacion.innerText = data.pacientes[i].tipoIdentificacion
            td_NumeroIdentificacion.innerText = data.pacientes[i].numeroIdentificacion
            td_telefono1.innerText = data.pacientes[i].telefono1
            td_TipoSangre.innerText = data.pacientes[i].tipoSangre
            td_Email.innerText = data.pacientes[i].email
            tr.id = data.pacientes[i].idCentroMedico
            tr.append(td_Nombre)
            tr.append(td_TipoIdentificacion)
            tr.append(td_NumeroIdentificacion)
            tr.append(td_telefono1)
            tr.append(td_TipoSangre)
            tr.append(td_Email)
            tbody_Pacientes.append(tr)
            div_DataView.hidden = false;
          }
        }
      })
}
