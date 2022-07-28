window.addEventListener("load", ()=>{
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
          fetch("https://localhost:44320/Records/GetRecord?MedicalCenterToken=MFHTWHAUIOF&identification=" + inputCedula.value,
          {
              method: 'GET',
              mode: 'cors', // <---
          }).then(data => data.json())
            .then(response => {
              console.log(response)
              if(response.responseCode != "00")
              {
                alert("Ha ocurrido un error, respuesta del API: " + response.message)
                spinner.classList.add("d-none")
              }
              else {
                localStorage.setItem("IDRecord", response.record.idRecord)
                localStorage.setItem("NoIdentificacion", response.paciente.identificacion)
                spinner.classList.add("d-none")
                window.location = window.location.origin + window.location.pathname + "PatientRecord.html"
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
