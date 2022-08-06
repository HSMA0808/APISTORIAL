var urlAPI = document.querySelector("#APIURL")
window.addEventListener("load", ()=>{
  var  urlbase = window.location.origin
  console.log(urlbase)
  console.log(localStorage.getItem("IDRecord"))
  var consulta_btnAgregar = document.querySelector("#btnNuevaConsulta")
  var analisis_btnAgregar = document.querySelector("#btnNuevoAnalisis")
  var operacion_btnAgregar = document.querySelector("#btnNuevaOperacion")
  var internamiento_btnAgregar = document.querySelector("#btnNuevoInternamiento")
  var emergencia_btnAgregar = document.querySelector("#btnNuevoIngresoEmergencia")

  var btnGuardarConsulta = document.querySelector("#Consultas_btnGuardar")
  var btnGuardarAnalisis = document.querySelector("#Analisis_btnGuardar")
  var btnGuardarOperacion = document.querySelector("#Operaciones_btnGuardar")
  var btnGuardarInternamiento = document.querySelector("#Internamientos_btnGuardar")
  var btnGuardarEmergencias = document.querySelector("#Emergencias_btnGuardar")

  var select_Consultas_dpDoctor = document.querySelector("#Consultas_dpDoctor")
  var select_Operaciones_dpDoctor = document.querySelector("#Operaciones_dpDoctor")
  var select_Operaciones_TipoOperacion = document.querySelector("#Operaciones_dpTipoOperacion")
  var select_Analisis_TipoAnalisis = document.querySelector("#Analisis_dpTipoAnalisis")
  var select_Analisis_Analisis = document.querySelector("#Analisis_dpAnalisis")

  select_Consultas_dpDoctor.addEventListener("change", ()=>{
    var txtNoIdentificacion = document.querySelector("#Consultas_txtNoIdentificacion")
    var txtEspecialidad = document.querySelector("#Consultas_txtEspecialidad")
    txtNoIdentificacion.value = select_Consultas_dpDoctor.value
    txtEspecialidad.value = select_Consultas_dpDoctor.options[select_Consultas_dpDoctor.selectedIndex].dataset.especialidad
  })
  select_Operaciones_dpDoctor.addEventListener("change", ()=>{
    var txtNoIdentificacion = document.querySelector("#Operaciones_txtNoIdentificacion")
    txtNoIdentificacion.value = select_Operaciones_dpDoctor.value
  })
  select_Operaciones_TipoOperacion.addEventListener("change", ()=>{
    var select_Operaciones_Operacion = document.querySelector("#Operaciones_dpOperacion")
    select_Operaciones_Operacion.selectedIndex = 0
    var i = 0
    var tipoOperacionId = select_Operaciones_TipoOperacion.options[select_Operaciones_TipoOperacion.selectedIndex].dataset.id
    for(i in select_Operaciones_Operacion.options)
    {
      var option = select_Operaciones_Operacion.options[i]
      if (tipoOperacionId == 0) {
        option.hidden=false;
      }
      else if(option.dataset.tipooperacioncode != tipoOperacionId.toString() && option.dataset.tipooperacioncode != 0)
      {
        option.hidden = true
      }
      else if(option.dataset.tipooperacioncode == tipoOperacionId.toString() && option.dataset.tipooperacioncode != 0)
      {
        option.hidden = false
      }
    }
  })
  select_Analisis_TipoAnalisis.addEventListener("change", ()=>{
    var select_Analisis_Analisis = document.querySelector("#Analisis_dpAnalisis")
    select_Analisis_Analisis.selectedIndex = 0
    var i = 0
    var tipoAnalisisId = select_Analisis_TipoAnalisis.options[select_Analisis_TipoAnalisis.selectedIndex].dataset.id
    for(i in select_Analisis_Analisis.options)
    {
      var option = select_Analisis_Analisis.options[i]
      if (tipoAnalisisId == 0) {
        option.hidden=false;
      }
      else if(option.dataset.tipoanalisisid != tipoAnalisisId.toString() && option.dataset.tipoanalisisid != 0)
      {
        option.hidden = true
      }
      else if(option.dataset.tipoanalisisid == tipoAnalisisId.toString() && option.dataset.tipoanalisisid != 0)
      {
        option.hidden = false
      }
    }
  })
  select_Analisis_Analisis.addEventListener("change", ()=>{
    var select_Analisis_Resultado = document.querySelector("#Analisis_dpResultados")
    if(select_Analisis_Analisis.options[select_Analisis_Analisis.selectedIndex].dataset.tiporesultado == 2)
    {
      select_Analisis_Resultado.disabled = true
    }
    else {
      select_Analisis_Resultado.disabled = false
    }
  })



  btnGuardarConsulta.addEventListener("click", ()=>{
      desactivarControles(btnGuardarConsulta)
      var select_Doctor = document.querySelector("#Consultas_dpDoctor")
      var txtNoIdentificacion = document.querySelector("#Consultas_txtNoIdentificacion")
      var txtEspecialidad = document.querySelector("#Consultas_txtEspecialidad")
      var txtObservaciones = document.querySelector("#Consultas_txtObservaciones")
      var txtIndicaciones = document.querySelector("#Consultas_txtIndicaciones")

      if(select_Doctor.value == 0 || txtNoIdentificacion.value.trim() == "" || txtEspecialidad.value.trim() == "" || txtObservaciones.value.trim() == "" || txtIndicaciones.value.trim() == "")
      {
        alert("Los siguientes campos son requeridos: 'Doctor', Observaciones, Indicaciones")
        activarControles(btnGuardarConsulta)
      }
      else {
        fecha = new Date().toLocaleDateString()
        var datos = {idRecord: localStorage.getItem("IDRecord"), medicalCenter_Token: "ASDJKHAUIOF", doctor_Identification: txtNoIdentificacion.value, specialtyCode: select_Doctor.options[select_Doctor.selectedIndex].dataset.codigoespecialidad, observations: txtObservaciones.value, indications: txtIndicaciones.value,  visitDate: fecha}
        console.log(datos)
        fetch(urlAPI.value + "Records/SetRecordVisit", {
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
      .then(response => response.json())
      .then(data => {
        if(data.responseCode != "00")
        {
          alert("Algo no ha salido bien, respuesta del API: " + data.message)
        }
        else {
          desactivarControles(select_Doctor, txtNoIdentificacion, txtEspecialidad, txtObservaciones, txtIndicaciones, btnGuardarConsulta)
          loadData();
          alert("Consulta guardada satisfactoriamente");
        }
      })
      }
  })
  btnGuardarAnalisis.addEventListener("click", ()=>{
      desactivarControles(btnGuardarAnalisis)
      var select_TipoAnalisis = document.querySelector("#Analisis_dpTipoAnalisis")
      var select_Analisis = document.querySelector("#Analisis_dpAnalisis")
      var select_Resultados = document.querySelector("#Analisis_dpResultados")
      var txtObservaciones = document.querySelector("#Analisis_txtObservaciones")
      var checkResultadosPublicos = document.querySelector("#checkPublicResults")

      if(select_TipoAnalisis.value.toString() == "0" || select_Analisis.value == 0 || (select_Resultados.value == 0 && txtObservaciones.value == ""))
      {
        alert("Los siguientes campos son requeridos: 'Tipo Analisis', 'Analisis', 'Resultados'")
        activarControles(btnGuardarAnalisis)
      }
      else {
        var fecha = new Date().toLocaleDateString()
        var datos = { idRecord: localStorage.getItem("IDRecord"), medicalCenter_Token:"ASDJKHAUIOF", analysisCode: select_Analisis.value, publicResults: false, resultCode: select_Resultados.value, resultsObservations: txtObservaciones.value, analysisDate: fecha}
        fetch(urlAPI.value + "Records/SetRecordAnalysis", {
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
      .then(response => response.json())
      .then(data => {
        if(data.responseCode != "00")
        {
          alert("Algo no ha salido bien, respuesta del API: " + data.message)
        }
        else {
          desactivarControles(btnGuardarAnalisis, select_TipoAnalisis, select_Analisis, select_Resultados, txtObservaciones, checkResultadosPublicos)
          loadData()
          alert("El analisis se ha registrado correctamente.")
        }
      })
      }
  })
  btnGuardarOperacion.addEventListener("click", ()=>{
      desactivarControles(btnGuardarOperacion)
      var select_Doctor = document.querySelector("#Operaciones_dpDoctor")
      var txtNoIdentificacion = document.querySelector("#Operaciones_txtNoIdentificacion")
      var txtFecha = document.querySelector("#Operaciones_txtFecha")
      var select_TipoOperacion = document.querySelector("#Operaciones_dpTipoOperacion")
      var select_Operacion = document.querySelector("#Operaciones_dpOperacion")
      if(select_Doctor.value == 0 || txtFecha.value == null || select_TipoOperacion.value.toString() == "0" || select_Operacion.value == 0)
      {
        alert("Los siguientes campos son requeridos: 'Doctor', 'Fecha', 'Tipo Operacion', 'Operacion'")
        activarControles(btnGuardarOperacion)
      }
      else {
          var datos = {idRecord: localStorage.getItem("IDRecord"), medicalCenter_Token: "MFHTWHAUIOF", doctor_Identification: txtNoIdentificacion.value, operationCode: select_Operacion.value, operationDate: txtFecha.value}
          fetch(urlAPI.value + "Records/SetRecordOperation", {
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
        .then(response => response.json())
        .then(data => {
          if(data.responseCode != "00")
          {
            alert("Algo no ha salido bien, respuesta del API: " + data.message)
          }
          else {
            desactivarControles(btnGuardarOperacion, select_Doctor, txtNoIdentificacion, txtFecha, select_TipoOperacion, select_Operacion)
            loadData()
            alert("La operacion se ha registrado correctamente.")

          }
        })
      }
  })
  btnGuardarInternamiento.addEventListener("click", ()=>{
      desactivarControles(btnGuardarInternamiento)
      var select_CentroMedico = document.querySelector("#Internamientos_dpCentroMedico")
      var txtFecha = document.querySelector("#Internamientos_txtFecha")
      var txtRazon = document.querySelector("#Internamientos_txtRazon")
      if(select_CentroMedico.value == 0 || txtFecha.value == null || txtRazon.value == "" )
      {
        alert("Los siguientes campos son requeridos: 'Centro Medico', 'Fecha', 'Razon'")
        activarControles(btnGuardarInternamiento)
      }
      else {
          var datos = {idRecord: localStorage.getItem("IDRecord"), medicalCenter_Token:"MFHTWHAUIOF", reasonInterment: txtRazon.value, intermentDate: txtFecha.value}
          fetch(urlAPI.value + "Records/SetRecordInterment", {
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
        .then(response => response.json())
        .then(data => {
          if(data.responseCode != "00")
          {
            alert("Algo no ha salido bien, respuesta del API: " + data.message)
          }
          else {
            desactivarControles(btnGuardarInternamiento, select_CentroMedico, txtFecha, txtRazon)
            loadData()
            alert("El internamiento se ha registrado correctamente.")
          }
        })
      }
  })
  btnGuardarEmergencias.addEventListener("click", ()=>{
    desactivarControles(btnGuardarEmergencias)
    var select_CentroMedico = document.querySelector("#Emergencias_dpCentroMedico")
    var txtFecha = document.querySelector("#Emergencias_txtFecha")
    var txtRazon = document.querySelector("#Emergencias_txtRazon")
      if(select_CentroMedico.value == 0 || txtFecha.value == null || txtRazon.value == "" )
      {
        alert("Los siguientes campos son requeridos: 'Centro Medico', 'Fecha', 'Razon'")
        activarControles(btnGuardarEmergencias)
      }
      else {
          var datos = {idRecord: localStorage.getItem("IDRecord"), medicalCenter_Token:"ASDJKHAUIOF", reasonInterment: txtRazon.value, entryDate: txtFecha.value}
          fetch(urlAPI.value + "Records/SetRecordEmergencyEntry", {
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
        .then(response => response.json())
        .then(data => {
          if(data.responseCode != "00")
          {
            alert("Algo no ha salido bien, respuesta del API: " + data.message)
          }
          else {
            desactivarControles(btnGuardarEmergencias)
            loadData()
            alert("La Emergencia ha sido registrada correctamente.")
          }
        })
      }
  })

  consulta_btnAgregar.addEventListener("click", ()=>{
    var selectDoctor = document.querySelector("#Consultas_dpDoctor")
    var txtNoIdentificacion = document.querySelector("#Consultas_txtNoIdentificacion")
    var txtEspecialidad = document.querySelector("#Consultas_txtEspecialidad")
    var txtObservaciones = document.querySelector("#Consultas_txtObservaciones")
    var txtIndicaciones = document.querySelector("#Consultas_txtIndicaciones")
    var btnGuardar = document.querySelector("#Consultas_btnGuardar")
    selectDoctor.selectedIndex = 0
    limpiarControles(txtNoIdentificacion, txtEspecialidad, txtObservaciones, txtIndicaciones)
    activarControles(selectDoctor, txtNoIdentificacion, txtEspecialidad, txtObservaciones, txtIndicaciones)
  })
  analisis_btnAgregar.addEventListener("click", ()=>{
    var selectTipoAnalisis = document.querySelector("#Analisis_dpTipoAnalisis")
    var selectAnalisis = document.querySelector("#Analisis_dpAnalisis")
    var selectResultados = document.querySelector("#Analisis_dpResultados")
    var txtObservaciones = document.querySelector("#Analisis_txtObservaciones")
    var checkPublicResults = document.querySelector("#checkPublicResults")
    limpiarControles(txtObservaciones, checkPublicResults)
    activarControles(selectTipoAnalisis, selectAnalisis, selectResultados, txtObservaciones, checkPublicResults)
  })
  operacion_btnAgregar.addEventListener("click", ()=>{
    var selectDoctor = document.querySelector("#Operaciones_dpDoctor")
    var txtNoIdentificacion = document.querySelector("#Operaciones_txtNoIdentificacion")
    var txtFecha = document.querySelector("#Operaciones_txtFecha")
    var selectTipoOperacion = document.querySelector("#Operaciones_dpTipoOperacion")
    var selectOperacion = document.querySelector("#Operaciones_dpOperacion")
    limpiarControles(txtNoIdentificacion, txtFecha)
    activarControles(txtNoIdentificacion, txtFecha)
  })
  internamiento_btnAgregar.addEventListener("click", ()=>{
    var selectCentroMedico = document.querySelector("#Internamientos_dpCentroMedico")
    var txtFecha = document.querySelector("#Internamientos_txtFecha")
    var txtRazon = document.querySelector("#Internamientos_txtRazon")
    limpiarControles(txtFecha, txtRazon)
    activarControles(txtFecha, txtRazon)
  })
  emergencia_btnAgregar.addEventListener("click", ()=>{
    var selectCentroMedico = document.querySelector("#Emergencias_dpCentroMedico")
    var txtFecha = document.querySelector("#Emergencias_txtFecha")
    var txtRazon = document.querySelector("#Emergencias_txtRazon")
    var btnGuardar = document.querySelector("#Emergencias_btnGuardar")
    limpiarControles(txtFecha, txtRazon)
    activarControles(selectCentroMedico, txtFecha, txtRazon, btnGuardar)
  })

  loadData()

  function loadData()
  {
    getRecordPaciente()
    getRecordConsulta()
    getRecordAnalisis()
    getRecordOperacion()
    getRecordInternamientos()
    getRecordEmergencias()
}


//Metodos para consumir API y consultar records
  function getRecordConsulta()
  {
    fetch(urlAPI.value + "Records/GetRecordVisit?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
      method: 'GET',
      mode: 'cors',
    })
    .then(response => response.json())
    .then(data=>{
      console.log(data)
      cargarTablaVisits(data)
    })
  }
  function getRecordAnalisis()
  {
    fetch(urlAPI.value + "Records/GetRecordAnalysis?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
      method: 'GET',
      mode: 'cors',
    })
    .then(response => response.json())
    .then(data=>{
      console.log(data)
      cargarTablaAnalisis(data)
    })
  }
  function getRecordOperacion()
  {
    fetch(urlAPI.value + "Records/GetRecordOperation?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
      method: 'GET',
      mode: 'cors',
    })
    .then(response => response.json())
    .then(data=>{
      console.log(data)
      cargarTablaOperaciones(data)
    })
  }
  function getRecordInternamientos()
  {
    fetch(urlAPI.value + "Records/GetRecordInterment?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
      method: 'GET',
      mode: 'cors',
    })
    .then(response => response.json())
    .then(data=>{
      console.log(data)
      cargarTablaInternamientos(data)
    })
  }
  function getRecordEmergencias()
  {
    fetch(urlAPI.value + "Records/GetRecordEmergencyEntry?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
      method: 'GET',
      mode: 'cors',
    })
    .then(response => response.json())
    .then(data=>{
      console.log(data)
      cargarTablaEmergencias(data)
    })
  }

//Metodos para cargar las tablas de los tabs
  function cargarTablaVisits(data)
  {
    if(data.recordsVisits.length > 0)
    {
      var tbody_Consultas = document.querySelector("#tbody_Consultas");
      tbody_Consultas.innerText = ""
      let i = 0;
      for(i=0;i<data.recordsVisits.length;i++)
      {
        var tr = document.createElement("tr");
        var tdId = document.createElement("td")
        var tdObservaciones = document.createElement("td")
        var tdIndicaciones = document.createElement("td")
        var tdDoctorNombre = document.createElement("td")
        var tdEspecialidad = document.createElement("td")
        var tdfecha = document.createElement("td")
        var tdButtonView = document.createElement("td")
         tdId.innerText = data.recordsVisits[i].idrecordVisits;
         tdObservaciones.innerText = data.recordsVisits[i].observaciones.substring(0,10) + "...";
         tdIndicaciones.innerText = data.recordsVisits[i].indicaciones.substring(0,10) + "...";
         tdDoctorNombre.innerText = data.recordsVisits[i].doctor_Nombre;
         tdEspecialidad.innerText = data.recordsVisits[i].especialidad_Medica;
         tdfecha.innerText = new Date(data.recordsVisits[i].fecha_Visita).toLocaleDateString();
         tdButtonView.innerHTML = '<td><button data-rVisitId = "'+data.recordsVisits[i].idrecordVisits+'" data-doctorNombre = "'+data.recordsVisits[i].doctor_Nombre+'" data-doctorNoIdentificacion = "'+data.recordsVisits[i].doctor_NoIdentificacion+'" data-especialidadMedica = "'+data.recordsVisits[i].especialidad_Medica+'" data-indicaciones = "'+data.recordsVisits[i].indicaciones+'" data-observaciones = "'+data.recordsVisits[i].observaciones+'" data-fechaVisita = "'+data.recordsVisits[i].fecha_Visita+'" class = "btn btn-secondary" data-bs-toggle="modal" data-bs-target="#ModalConsulta" onclick="mapearModalConsulta('+data.recordsVisits[i].idrecordVisits+')"> <i class ="fas fa-eye"></i> </button></td>'
         tr.append(tdId);
         tr.append(tdObservaciones);
         tr.append(tdIndicaciones);
         tr.append(tdDoctorNombre);
         tr.append(tdEspecialidad);
         tr.append(tdfecha);
         tr.append(tdButtonView);
         console.log(tr)
         tbody_Consultas.append(tr)
      }
    }
  }
  function cargarTablaAnalisis(data)
  {
    if(data.recordsAnalysis.length > 0)
    {
      var tbody_Analisis = document.querySelector("#tbody_Analisis");
      tbody_Analisis.innerText = ""
      let i = 0;
      for(i=0;i<data.recordsAnalysis.length;i++)
      {
        var tr = document.createElement("tr");
        var tdId = document.createElement("td")
        var tdAnalisis = document.createElement("td")
        var tdResultados = document.createElement("td")
        var tdFecha = document.createElement("td")
        var tdCentro = document.createElement("td")
        var tdButtonView = document.createElement("td")
         tdId.innerText = data.recordsAnalysis[i].idrecordAnalysis;
         tdAnalisis.innerText = data.recordsAnalysis[i].nombreAnalisis;
         if(data.recordsAnalysis[i].resultados != null)
         {
            tdResultados.innerText = data.recordsAnalysis[i].resultados;
         }
         else {
          tdResultados.innerText = "N/A";
         }
         tdFecha.innerText = new Date(data.recordsAnalysis[i].fechaAnalisis).toLocaleDateString();
         tdCentro.innerText = data.recordsAnalysis[i].createUser;
         tdButtonView.innerHTML = '<td><button data-rAnalysisId = "'+data.recordsAnalysis[i].idrecordAnalysis+'" data-analisisname = "'+data.recordsAnalysis[i].nombreAnalisis+'" data-codigoAnalisis = "'+data.recordsAnalysis[i].codigoAnalisis+'" data-resultado = "'+data.recordsAnalysis[i].resultados+'" data-observaciones = "'+data.recordsAnalysis[i].resultados_Observaciones+'" data-tipoAnalisis = "'+data.recordsAnalysis[i].tipoAnalisis+'" data-CodigoTipoAnalisis = "'+data.recordsAnalysis[i].codigo_TipoAnalisis+'"  class = "btn btn-secondary" onclick="mapearModalAnalisis('+data.recordsAnalysis[i].idrecordAnalysis+')" data-bs-toggle="modal" data-bs-target="#ModalAnalisis"> <i class ="fas fa-eye"></i> </button></td>'
         tr.append(tdId);
         tr.append(tdAnalisis);
         tr.append(tdResultados);
         tr.append(tdFecha);
         tr.append(tdCentro);
         tr.append(tdButtonView);
         console.log(tr)
         tbody_Analisis.append(tr)
        }
      }
  }
  function cargarTablaOperaciones(data)
  {
    if(data.recordsOperations.length > 0)
    {
      var tbody_Operaciones = document.querySelector("#tbody_Operaciones");
      tbody_Operaciones.innerText = ""
      let i = 0;
      for(i=0;i<data.recordsOperations.length;i++)
      {
        var tr = document.createElement("tr");
        var tdId = document.createElement("td")
        var tdOperacion = document.createElement("td")
        var tdFecha = document.createElement("td")
        var tdCentroMedico = document.createElement("td")
        var tdButtonView = document.createElement("td")
         tdId.innerText = data.recordsOperations[i].idrecordOperation;
         tdOperacion.innerText = data.recordsOperations[i].operacion;
         tdFecha.innerText = new Date(data.recordsOperations[i].fecha_Operacion).toLocaleDateString();
         tdCentroMedico.innerText = data.recordsOperations[i].centroMedico;
         tdButtonView.innerHTML = '<td><button data-rOperationId = "'+data.recordsOperations[i].idrecordOperation+'" data-doctorname = "'+data.recordsOperations[i].doctor_Nombre+'" data-doctorNoIdentificacion = "'+data.recordsOperations[i].doctor_Identificacion+'" data-fechaoperacion = "'+data.recordsOperations[i].fecha_Operacion+'" data-tipooperacion = "'+data.recordsOperations[i].tipoOperacion+'" data-CodigoTipoOperacion = "'+data.recordsOperations[i].codigo_TipoOperacion+'" data-codigooperacion = "'+data.recordsOperations[i].codigo_Operacion+'" data-operacion = "'+data.recordsOperations[i].operacion+'" data-centroMedico = "'+data.recordsOperations[i].centroMedico+'" data-idCentroMedico = "'+data.recordsOperations[i].idCentroMedico+'" onclick = "mapearModalOperacion('+data.recordsOperations[i].idrecordOperation+')" data-bs-toggle="modal" data-bs-target="#ModalOperacion" class = "btn btn-secondary"> <i class ="fas fa-eye"></i> </button></td>'
         tr.append(tdId);
         tr.append(tdOperacion);
         tr.append(tdCentroMedico);
         tr.append(tdFecha);
         tr.append(tdButtonView);
         console.log(tr)
         tbody_Operaciones.append(tr)
        }
      }
  }
  function cargarTablaInternamientos(data)
  {
    if(data.recordsInterments.length>0)
    {
      var tbody_Internamientos = document.querySelector("#tbody_Internamientos");
      tbody_Internamientos.innerText = ""
      let i = 0;
      for(i=0;i<data.recordsInterments.length;i++)
      {
        var tr = document.createElement("tr");
        var tdId = document.createElement("td")
        var tdRazon = document.createElement("td")
        var tdFecha = document.createElement("td")
        var tdCentroMedico = document.createElement("td")
        var tdButtonView = document.createElement("td")
         tdId.innerText = data.recordsInterments[i].idrecordInterment;
         tdRazon.innerText = data.recordsInterments[i].razon.substring(0,30) + "...";
         tdCentroMedico.innerText = data.recordsInterments[i].centroMedico;
         tdFecha.innerText = new Date(data.recordsInterments[i].fecha_Internamiento).toLocaleDateString();
         tdButtonView.innerHTML = '<td><button data-rIntermentId = "'+data.recordsInterments[i].idrecordInterment+'" data-idCentroMedico = "'+data.recordsInterments[i].idCentroMedico+'" data-centromedico = "'+data.recordsInterments[i].centroMedico+'" data-razon = "'+data.recordsInterments[i].razon+'" data-fecha = "'+data.recordsInterments[i].fecha_Internamiento+'" onclick = "mapearModalInternamientos('+data.recordsInterments[i].idrecordInterment+')"  data-bs-toggle="modal" data-bs-target="#ModalInternamiento" class = "btn btn-secondary"> <i class ="fas fa-eye"></i> </button></td>'
         tr.append(tdId);
         tr.append(tdRazon);
         tr.append(tdCentroMedico);
         tr.append(tdFecha);
         tr.append(tdButtonView);
         console.log(tr)
         tbody_Internamientos.append(tr)
        }
    }
  }
  function cargarTablaEmergencias(data)
  {
    if(data.recordsEmergencyEntries.length > 0)
    {
      var tbody_Emergencias = document.querySelector("#tbody_Emergencias");
      tbody_Emergencias.innerText = ""
      let i = 0;
      for(i=0;i<data.recordsEmergencyEntries.length;i++)
      {
        var tr = document.createElement("tr");
        var tdId = document.createElement("td")
        var tdRazon = document.createElement("td")
        var tdFecha = document.createElement("td")
        var tdCentroMedico = document.createElement("td")
        var tdButtonView = document.createElement("td")
         tdId.innerText = data.recordsEmergencyEntries[i].idRecordEmergencyEntries;
         tdRazon.innerText = data.recordsEmergencyEntries[i].razon.substring(0,30) + "...";
         tdCentroMedico.innerText = data.recordsEmergencyEntries[i].centroMedico;
         tdFecha.innerText = new Date(data.recordsEmergencyEntries[i].fecha_Entrada).toLocaleDateString();
         tdButtonView.innerHTML = '<td><button data-rEmergencyEntryId = "'+data.recordsEmergencyEntries[i].idRecordEmergencyEntries+'" data-idCentroMedico = "'+data.recordsEmergencyEntries[i].idCentroMedico+'" data-centromedico = "'+data.recordsEmergencyEntries[i].centroMedico+'" data-razon = "'+data.recordsEmergencyEntries[i].razon+'" data-fecha = "'+data.recordsEmergencyEntries[i].fecha_Entrada+'" onclick = "mapearModalEmergencias('+data.recordsEmergencyEntries[i].idRecordEmergencyEntries+')" data-bs-toggle="modal" data-bs-target="#ModalEmergencias" class = "btn btn-secondary"> <i class ="fas fa-eye"></i> </button></td>'
         tr.append(tdId);
         tr.append(tdRazon);
         tr.append(tdCentroMedico);
         tr.append(tdFecha);
         tr.append(tdButtonView);
         console.log(tr)
         tbody_Emergencias.append(tr)
        }
    }
  }

  //Metodo para mapear data del Paciente
  function mapearDataPaciente(data)
  {
    var txtNombre = document.querySelector("#txtNombre");
    var txtApellido = document.querySelector("#txtApellido");
    var dpTipoIdentificacion = document.querySelector("#dpTipoIdentificacion");
    var txtNoIdentificacion = document.querySelector("#txtNoIdentificacion");
    var txtTelefono = document.querySelector("#txtTelefono");
    var txtTelefono2 = document.querySelector("#txtTelefono2");
    var txtEmail = document.querySelector("#txtEmail");
    var txtDireccion = document.querySelector("#txtDireccion");
    var txtDireccion2 = document.querySelector("#txtDireccion2");
    txtNombre.value = data.nombre
    txtApellido.value = data.apellido
    //dpTipoIdentificacion.value = ""
    txtNoIdentificacion.value = data.identificacion
    txtTelefono.value = data.telefono
    txtTelefono2.value = data.telefono2
    txtEmail.value = data.email
    txtDireccion.innerText = data.direccion1
    txtDireccion2.innerText = data.direccion2
  }

  function getRecordPaciente()
  {
    fetch(urlAPI.value + "Records/GetRecord?MedicalCenterToken=MFHTWHAUIOF&Identification=" + localStorage.getItem("NoIdentificacion"),{
      method: 'GET',
      mode: 'cors',
    })
    .then(response => response.json())
    .then(data=>{
    mapearDataPaciente(data.paciente)
    })
  }


})

//Metodos para mapear los modals
function mapearModalConsulta(idRecordConsulta)
{
  var btnDataConsulta = document.querySelectorAll("[data-rvisitid='"+idRecordConsulta+"']")
  var selectDoctor = document.querySelector("#Consultas_dpDoctor")
  var txtNoIdentificacion = document.querySelector("#Consultas_txtNoIdentificacion")
  var txtEspecialidad = document.querySelector("#Consultas_txtEspecialidad")
  var txtObservaciones = document.querySelector("#Consultas_txtObservaciones")
  var txtIndicaciones = document.querySelector("#Consultas_txtIndicaciones")
  var btnGuardar = document.querySelector("#Consultas_btnGuardar")
  console.log(btnDataConsulta[0])
  console.log(btnDataConsulta[0].dataset.indicaciones)
  txtEspecialidad.value = btnDataConsulta[0].dataset.especialidadmedica
  txtNoIdentificacion.value = btnDataConsulta[0].dataset.doctornoidentificacion
  txtObservaciones.value = btnDataConsulta[0].dataset.observaciones
  txtIndicaciones.value = btnDataConsulta[0].dataset.indicaciones
  selectDoctor.selectedIndex = 1
  desactivarControles(selectDoctor, txtNoIdentificacion, txtEspecialidad, txtObservaciones, txtIndicaciones, btnGuardar)
}
function mapearModalAnalisis(idRecordAnalisis)
{
  var btnDataAnalisis = document.querySelectorAll("[data-ranalysisid='"+idRecordAnalisis+"']")
  var selectTipoAnalisis = document.querySelector("#Analisis_dpTipoAnalisis")
  var selectAnalisis = document.querySelector("#Analisis_dpAnalisis")
  var selectResultados = document.querySelector("#Analisis_dpResultados")
  var txtObservaciones = document.querySelector("#Analisis_txtObservaciones")
  var checkPublicResults = document.querySelector("#checkPublicResults")
  var btnGuardar = document.querySelector("#Analisis_btnGuardar")
  desactivarControles(selectTipoAnalisis, selectAnalisis, selectResultados, txtObservaciones, checkPublicResults, btnGuardar)
  console.log(btnDataAnalisis[0])
  console.log(btnDataAnalisis[0].dataset.indicaciones)
  txtObservaciones.innerText = btnDataAnalisis[0].dataset.observaciones
}
function mapearModalOperacion(idRecordOperacion)
{
  var btnDataOperacion = document.querySelectorAll("[data-roperationid='"+idRecordOperacion+"']")
  var selectDoctor = document.querySelector("#Operaciones_dpDoctor")
  var txtNoIdentificacion = document.querySelector("#Operaciones_txtNoIdentificacion")
  var txtFecha = document.querySelector("#Operaciones_txtFecha")
  var selectTipoOperacion = document.querySelector("#Operaciones_dpTipoOperacion")
  var selectOperacion = document.querySelector("#Operaciones_dpOperacion")
  var btnGuardar = document.querySelector("#Operaciones_btnGuardar")
  txtFecha.value = new Date(btnDataOperacion[0].dataset.fechaoperacion).toLocaleDateString()
  desactivarControles(selectDoctor, txtNoIdentificacion, txtFecha, selectTipoOperacion, selectOperacion, btnGuardar)
  console.log("Modal Operacion")
}
function mapearModalInternamientos(idRecordInternamiento)
{
  var btnDataInternamientos = document.querySelectorAll("[data-rIntermentId='"+idRecordInternamiento+"']")
  var selectCentroMedico = document.querySelector("#Internamientos_dpCentroMedico")
  var txtFecha = document.querySelector("#Internamientos_txtFecha")
  var txtRazon = document.querySelector("#Internamientos_txtRazon")
  var btnGuardar = document.querySelector("#Internamientos_btnGuardar")
  txtRazon.innerText = btnDataInternamientos[0].dataset.razon
  txtFecha.value = new Date(btnDataInternamientos[0].dataset.fecha).toLocaleDateString()
  desactivarControles(selectCentroMedico, txtFecha, txtRazon, btnGuardar)
}
function mapearModalEmergencias(idRecordEmergencia)
{
  var btnDataEmergencias = document.querySelectorAll("[data-remergencyentryid='"+idRecordEmergencia+"']")
  var selectCentroMedico = document.querySelector("#Emergencias_dpCentroMedico")
  var txtFecha = document.querySelector("#Emergencias_txtFecha")
  var txtRazon = document.querySelector("#Emergencias_txtRazon")
  var btnGuardar = document.querySelector("#Emergencias_btnGuardar")
  txtRazon.value = btnDataEmergencias[0].dataset.razon
  txtFecha.value = new Date(btnDataEmergencias[0].dataset.fecha).toLocaleDateString()
  desactivarControles(selectCentroMedico, txtFecha, txtRazon, btnGuardar)
}

function desactivarControles(...controles)
{
  let indice = 0;
  for(indice in controles)
  {
    controles[indice].disabled = true
  }
}

function activarControles(...controles)
{
  let indice = 0;
  for(indice in controles)
  {
    controles[indice].disabled = false
  }
}

function limpiarControles(...controles)
{
  let indice = 0;
  for(indice in controles)
  {
    controles[indice].value = null
  }
}
