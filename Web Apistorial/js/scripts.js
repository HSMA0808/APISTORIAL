window.addEventListener("load", ()=>{
  var dpIdentificationType = document.querySelector("#dpIdentificationType")
  var inputCedula = document.querySelector("#exampleInputCedula");
  var btnConsultar = document.querySelector("#btnConsultar");

  btnConsultar.addEventListener("click", () =>{
      if(CedulaNull(inputCedula.value))
      {
        alert("El campo de texto No. Identificacion es requerido")
      }
      else {
          fetch("https://localhost:44320/Records/GetRecord?identification=" + inputCedula.value,
          {
              method: 'GET',
              mode: 'cors', // <---
          }).then(data => data.json())
            .then(response => {
              console.log(response)
            });
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
    console.log(btnConsultar);
  })
