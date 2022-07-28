window.addEventListener("load", ()=>{
  var  urlbase = window.location.origin
  console.log(urlbase)
  console.log(localStorage.getItem("IDRecord"))

  getRecordPaciente()
  getRecordConsulta()
  getRecordAnalisis()
  getRecordOperacion()
  getRecordInternamientos()
  getRecordEmergencias()

//Metodos para consumir API y consultar records
  function getRecordConsulta()
  {
    fetch("https://localhost:44320/Records/GetRecordVisit?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
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
    fetch("https://localhost:44320/Records/GetRecordAnalysis?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
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
    fetch("https://localhost:44320/Records/GetRecordOperation?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
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
    fetch("https://localhost:44320/Records/GetRecordInterment?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
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
    fetch("https://localhost:44320/Records/GetRecordEmergencyEntry?MedicalCenterToken=MFHTWHAUIOF&idRecord=" + localStorage.getItem("IDRecord"),{
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
         tdfecha.innerText = data.recordsVisits[i].fecha_Visita;
         tdButtonView.innerHTML = '<td><button data-rVisitId = "'+data.recordsVisits[i].idrecordVisits+'" class = "btn btn-secondary"> <i class ="fas fa-eye"></i> </button></td>'
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
         tdAnalisis.innerText = data.recordsAnalysis[i].analysisName;
         if(data.recordsAnalysis[i].result != null)
         {
            tdResultados.innerText = data.recordsAnalysis[i].result;
         }
         else {
          tdResultados.innerText = "N/A";
         }
         tdFecha.innerText = data.recordsAnalysis[i].analysisDate;
         tdCentro.innerText = data.recordsAnalysis[i].createUser;
         tdButtonView.innerHTML = '<td><button data-rAnalysisId = "'+data.recordsAnalysis[i].idrecordAnalysis+'" class = "btn btn-secondary"> <i class ="fas fa-eye"></i> </button></td>'
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
         tdFecha.innerText = data.recordsOperations[i].fecha_Operacion;
         tdCentroMedico.innerText = data.recordsOperations[i].centroMedico;
         tdButtonView.innerHTML = '<td><button data-rOperationId = "'+data.recordsOperations[i].idrecordOperation+'" class = "btn btn-secondary"> <i class ="fas fa-eye"></i> </button></td>'
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
         tdFecha.innerText = data.recordsInterments[i].fecha_Internamiento;
         tdButtonView.innerHTML = '<td><button data-rIntermentId = "'+data.recordsInterments[i].idrecordInterment+'" class = "btn btn-secondary"> <i class ="fas fa-eye"></i> </button></td>'
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
      var tbody_Internamientos = document.querySelector("#tbody_Emergencias");
      let i = 0;
      for(i=0;i<data.recordsEmergencyEntries.length;i++)
      {
        var tr = document.createElement("tr");
        var tdId = document.createElement("td")
        var tdRazon = document.createElement("td")
        var tdFecha = document.createElement("td")
        var tdCentroMedico = document.createElement("td")
        var tdButtonView = document.createElement("td")
         tdId.innerText = data.recordsEmergencyEntries[i].idrecordEmergencyEntries;
         tdRazon.innerText = data.recordsEmergencyEntries[i].razon.substring(0,30) + "...";
         tdCentroMedico.innerText = data.recordsEmergencyEntries[i].centroMedico;
         tdFecha.innerText = data.recordsEmergencyEntries[i].fecha_Entrada;
         tdButtonView.innerHTML = '<td><button data-rEmergencyEntryId = "'+data.recordsEmergencyEntries[i].idrecordEmergencyEntries+'" class = "btn btn-secondary"> <i class ="fas fa-eye"></i> </button></td>'
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
    fetch("https://localhost:44320/Records/GetRecord?MedicalCenterToken=MFHTWHAUIOF&Identification=" + localStorage.getItem("NoIdentificacion"),{
      method: 'GET',
      mode: 'cors',
    })
    .then(response => response.json())
    .then(data=>{
    mapearDataPaciente(data.paciente)
    })
  }

})
