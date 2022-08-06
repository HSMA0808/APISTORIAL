window.addEventListener("load", ()=>{

  var urlAPI = document.querySelector("#APIURL")
  localStorage.setItem("userId", "Admin")
  localStorage.setItem("Password", "Admin2012.")
  var dpIdentificationType = document.querySelector("#dpIdentificationType")
  var inputCedula = document.querySelector("#exampleInputCedula");
  var btnConsultar = document.querySelector("#btnConsultar");

  btnConsultar.addEventListener("click", () =>{
      var spinner = document.querySelector("#sp_btnConsultar");
      spinner.classList.remove("d-none")
      localStorage.removeItem("idRecord")
      localStorage.removeItem("NoIdentificacion")
      if(CedulaNull(inputCedula.value))
      {
        alert("El campo de texto No. Identificacion es requerido")
        spinner.classList.add("d-none")
      }
      else {
        try {
          fetch(urlAPI.value + "Records/GetRecord?MedicalCenterToken=MFHTWHAUIOF&identification=" + inputCedula.value,
          {
              method: 'GET',
              mode: 'cors', // <---
          }).then(data => data.json())
            .then(response => {
              console.log(response)
              if(response.responseCode != "00" && response.message == "Se intento consultar la nueva cedula en el padron electoral pero no hubo conexion.")
              {
                alert("El paciente no fue encontrado")
                window.location = window.location.origin + "/RegisterPatient.html"
              }
              else if(response.responseCode != "00")
              {
                alert("Ha ocurrido un error, respuesta del API: " + response.message)
                spinner.classList.add("d-none")
              }
              else {
                localStorage.setItem("IDRecord", response.record.idRecord)
                localStorage.setItem("NoIdentificacion", response.paciente.identificacion)
                spinner.classList.add("d-none")
                window.location = window.location.origin + "/PatientRecord.html"
              }
            });
        } catch (e) {
          alert("Ha ocurrido un error, tratando de conectar con el API: " + e)
          spinner.classList.add("d-none")
        }
      }
    })

    function CedulaNull(cedula)
    {
      var valorNull = false;
      if(cedula == null || cedula.trim() == "")
      {
        valorNull = true
      }
      return valorNull
    }

    function PasaporteValido(pasaporte)
    {
      var valorNull = false;
      if(pasaporte == null || pasaporte.trim() == "")
      {
        valorNull = true
      }
      return valorNull
    }
  })
