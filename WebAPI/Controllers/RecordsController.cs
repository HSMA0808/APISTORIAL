using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using WebAPI.Models;
using WebAPI.ResponseObjects;
using Microsoft.AspNetCore.Cors;
using WebAPI.RequestObjects;

namespace WebAPI.Controllers
{ 
    [ApiController]
    [EnableCors("MyPolicy")]
    [Route("[controller]/[action]")]
  
    public class RecordsController : ControllerBase
    {
        [HttpGet(Name = "GetRecord")]
        public IActionResult GetRecord(string MedicalCenterToken, string Identification = "")
            {
                IActionResult response;  
                bool NewRecord = false;
                try
                {
                    Record oRecord = new Record();
                    var medicalCenter = new MedicalCenter();
                if (Identification.Trim() == string.Empty)
                {
                    response =  BadRequest(new
                    {
                        ResponseCode = "99",
                        Message = "El parametro <Identification> no puede ser enviado sin data"
                    });
                }
                else if (medicalCenter.MedicalCenterValid(MedicalCenterToken) == false)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El Token suministrado no es valido" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    var Response_rv = new Response_RecordVisit();
                    var oRecordVisits = new List<RecordVisit>();
                    var response_RvList = new List<Response_RecordVisit>();
                    var idPatient = 0;
                    var patientList = db.Patients.Where(x => x.IdentificationNumber == Identification).ToList();
                    if (patientList.Count() <= 0)
                    {
                        NewRecord = true;
                        //Logica para crear el paciente consultando el padron
                        response = Ok(new
                        {
                            ResponseCode = "99",
                            Message = "Se intento consultar la nueva cedula en el padron electoral pero no hubo conexion.",
                            Record = oRecord
                        });
                    }
                    else
                    {
                        idPatient = patientList[0].Idpatient;
                        oRecord = oRecord.ReviewRecord(patientList[0].IdentificationNumber, MedicalCenterToken);
                        response = Ok(new
                        {
                            ResponseCode = "00",
                            Message = "Success",
                            Record = new
                            {
                                IDRecord = oRecord.Idrecord,
                                IDPatient = oRecord.Idpatient,
                                NewRecord = NewRecord,
                                Create_User = oRecord.CreateUser,
                                Create_Date = oRecord.CreateDate
                            },

                            Paciente = new
                            {
                                Nombre = patientList[0].FirstName,
                                Apellido = patientList[0].LastName,
                                Identificacion = patientList[0].IdentificationNumber,
                                Telefono = patientList[0].Tel1,
                                Telefono2 = patientList[0].Tel2,
                                Email = patientList[0].Email,
                                Direccion1 = patientList[0].Address1,
                                Direccion2 = patientList[0].Address2
                            }
                        });
                    }
                }
                }
                catch (Exception e)
                {
                    response = BadRequest(new
                    {
                        ResponseCode = "99",
                        Message = "Ha ocurrido un error: " + e.Message
                    });
                }
            return response;
            }
  
        [HttpGet(Name = "GetRecordVisit")]
        public IActionResult GetRecordVisit(string MedicalCenterToken, int idRecord)
        {
            IActionResult response;
            try
            {
                var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                var oRecord = db.Records.Find(idRecord);
                var medicalCenter = new MedicalCenter();
                if (oRecord == null)
                {
                    response = Ok(new { ResponseCode = "99", Message = "Record No. " + idRecord + " no encontrado" });
                }
                else if (medicalCenter.MedicalCenterValid(MedicalCenterToken) == false)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El Token suministrado no es valido" });
                }
                else
                {
                    var oPatient = db.Patients.Find(oRecord.Idpatient);
                    var oRecordVisit = db.RecordVisits.Where(rv => rv.Idrecord == idRecord).ToList();
                    var rvList = new ResponseObjects.Response_RecordVisit().ToList(oRecordVisit);
                    response = Ok(new
                    {
                        ResponseCode = "00",
                        Message = "Success",
                        Record = new { IDRecord = oRecord.Idrecord, IDPaciente = oRecord.Idpatient },
                        Paciente = new { Nombre = oPatient.FirstName + " " + oPatient.LastName, Identificacion = oPatient.IdentificationNumber, Tel1 = oPatient.Tel1, Tel2 = oPatient.Tel2, Email = oPatient.Email, Direccion1 = oPatient.Address1, Direccion2 = oPatient.Address2 },
                        RecordsVisits = rvList
                    });
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }
  
        [HttpGet(Name = "GetRecordOperation")]
        public IActionResult GetRecordOperation(string MedicalCenterToken, int idRecord)
        {
            IActionResult response;
            try
            {
                var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                var medicalCenter = new MedicalCenter();
                var oRecord = db.Records.Find(idRecord);
                if (oRecord == null)
                {
                    response = Ok(new { ResponseCode = "99", Message = "Record No. " + idRecord + " no encontrado" });
                }
                else if (medicalCenter.MedicalCenterValid(MedicalCenterToken) == false)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El Token suministrado no es valido" });
                }
                else
                {
                    var oPatient = db.Patients.Find(oRecord.Idpatient);
                    var oRecordOperation = db.RecordOperations.Where(ro => ro.Idrecord == idRecord).ToList();
                    var roList = new ResponseObjects.Response_RecordOperation().ToList(oRecordOperation);
                    response = Ok(new
                    {
                        ResponseCode = "00",
                        Message = "Success",
                        Record = new { IDRecord = oRecord.Idrecord, IDPaciente = oRecord.Idpatient },
                        Paciente = new { Nombre = oPatient.FirstName + " " + oPatient.LastName, Identificacion = oPatient.IdentificationNumber, Tel1 = oPatient.Tel1, Tel2 = oPatient.Tel2, Email = oPatient.Email, Direccion1 = oPatient.Address1, Direccion2 = oPatient.Address2 },
                        RecordsOperations = roList
                    });
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }

        [HttpGet(Name = "GetRecordInterment")]
        public IActionResult GetRecordInterment(string MedicalCenterToken, int idRecord)
        {
            IActionResult response;
            try
            {
                var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                var medicalCenter = new MedicalCenter();
                var oRecord = db.Records.Find(idRecord);
                if (oRecord == null)
                {
                    response = Ok(new { ResponseCode = "99", Message = "Record No. " + idRecord + " no encontrado" });
                }
                else if (medicalCenter.MedicalCenterValid(MedicalCenterToken) == false)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El Token suministrado no es valido" });
                }
                else
                {

                    var oPatient = db.Patients.Find(oRecord.Idpatient);
                    var oRecordInterment = db.RecordInterments.Where(ro => ro.Idrecord == idRecord).ToList();
                    var rI = new ResponseObjects.Response_RecordInterment().ToResponseList(oRecordInterment);
                    response = Ok(new
                    {
                        ResponseCode = "00",
                        Message = "Success",
                        Record = new { IDRecord = oRecord.Idrecord, IDPaciente = oRecord.Idpatient },
                        Paciente = new { Nombre = oPatient.FirstName + " " + oPatient.LastName, Identificacion = oPatient.IdentificationNumber, Tel1 = oPatient.Tel1, Tel2 = oPatient.Tel2, Email = oPatient.Email, Direccion1 = oPatient.Address1, Direccion2 = oPatient.Address2 },
                        RecordsInterments = rI
                    });
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }

        [HttpGet(Name = "GetRecordEmergencyEntry")]
        public IActionResult GetRecordEmergencyEntry(string MedicalCenterToken, int idRecord)
        {
            IActionResult response;
            try
            {
                var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                var medicalCenter = new MedicalCenter();
                var oRecord = db.Records.Find(idRecord);
                if (oRecord == null)
                {
                    response = Ok(new { ResponseCode = "99", Message = "Record No. " + idRecord + " no encontrado" });
                }
                else if (medicalCenter.MedicalCenterValid(MedicalCenterToken) == false)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El Token suministrado no es valido" });
                }
                else
                {

                    var oPatient = db.Patients.Find(oRecord.Idpatient);
                    var oRecordEmergencyEntry = db.RecordEmergencyemtries.Where(ro => ro.Idrecord == idRecord).ToList();
                    var rE = new ResponseObjects.Response_RecordEmergencyEntry().ToResponseList(oRecordEmergencyEntry);
                    response = Ok(new
                    {
                        ResponseCode = "00",
                        Message = "Success",
                        Record = new { IDRecord = oRecord.Idrecord, IDPaciente = oRecord.Idpatient },
                        Paciente = new { Nombre = oPatient.FirstName + " " + oPatient.LastName, Identificacion = oPatient.IdentificationNumber, Tel1 = oPatient.Tel1, Tel2 = oPatient.Tel2, Email = oPatient.Email, Direccion1 = oPatient.Address1, Direccion2 = oPatient.Address2 },
                        RecordsEmergencyEntries = rE
                    });
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }

        [HttpGet(Name = "GetRecordAnalysis")]
        public IActionResult GetRecordAnalysis(string MedicalCenterToken, int idRecord)
        {
            IActionResult response;
            try
            {
                var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                var medicalCenter = new MedicalCenter();
                var oRecord = db.Records.Find(idRecord);
                if (oRecord == null)
                {
                    response = Ok(new { ResponseCode = "99", Message = "Record No. " + idRecord + " no encontrado" });
                }
                else if (medicalCenter.MedicalCenterValid(MedicalCenterToken) == false)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El Token suministrado no es valido" });
                }
                else
                {

                    var oPatient = db.Patients.Find(oRecord.Idpatient);
                    var oRecordAnalysis = db.RecordAnalyses.Where(ro => ro.Idrecord == idRecord).ToList();
                    var rI = new ResponseObjects.Response_RecordAnalysis().ToResponseList(oRecordAnalysis);
                    response = Ok(new
                    {
                        ResponseCode = "00",
                        Message = "Success",
                        Record = new { IDRecord = oRecord.Idrecord, IDPaciente = oRecord.Idpatient },
                        Paciente = new { Nombre = oPatient.FirstName + " " + oPatient.LastName, Identificacion = oPatient.IdentificationNumber, Tel1 = oPatient.Tel1, Tel2 = oPatient.Tel2, Email = oPatient.Email, Direccion1 = oPatient.Address1, Direccion2 = oPatient.Address2 },
                        RecordsAnalysis = rI
                    });
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }

        [HttpGet(Name = "GetRecordVaccines")]
        public IActionResult GetRecordVaccines(string MedicalCenterToken, int idRecord)
        {
            IActionResult response;
            try
            {
                var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                var medicalCenter = new MedicalCenter();
                var oRecord = db.Records.Find(idRecord);
                if (oRecord == null)
                {
                    response = Ok(new { ResponseCode = "99", Message = "Record No. " + idRecord + " no encontrado" });
                }
                else if (medicalCenter.MedicalCenterValid(MedicalCenterToken) == false)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El Token suministrado no es valido" });
                }
                else
                {

                    var oPatient = db.Patients.Find(oRecord.Idpatient);
                    var oRecordVaccines = db.RecordVaccines.Where(rvs => rvs.Idrecord == idRecord).Include(nv => nv.NvvaccineNavigation).ToList();
                    var rvs = new ResponseObjects.Response_RecordVaccines().ToResponseList(oRecordVaccines);
                    response = Ok(new
                    {
                        ResponseCode = "00",
                        Message = "Success",
                        Record = new { IDRecord = oRecord.Idrecord, IDPaciente = oRecord.Idpatient },
                        Paciente = new { Nombre = oPatient.FirstName + " " + oPatient.LastName, Identificacion = oPatient.IdentificationNumber, Tel1 = oPatient.Tel1, Tel2 = oPatient.Tel2, Email = oPatient.Email, Direccion1 = oPatient.Address1, Direccion2 = oPatient.Address2 },
                        Vacunas = rvs
                    });
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }
        [HttpGet(Name = "GetRecordAllergies")]
        public IActionResult GetRecordAllergies(string MedicalCenterToken, int idRecord)
        {
            IActionResult response;
            try
            {
                var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                var medicalCenter = new MedicalCenter();
                var oRecord = db.Records.Find(idRecord);
                if (oRecord == null)
                {
                    response = Ok(new { ResponseCode = "99", Message = "Record No. " + idRecord + " no encontrado" });
                }
                else if (medicalCenter.MedicalCenterValid(MedicalCenterToken) == false)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El Token suministrado no es valido" });
                }
                else
                {

                    var oPatient = db.Patients.Find(oRecord.Idpatient);
                    var oRecordAllergies = db.RecordAllergies.Where(rvs => rvs.Idrecord == idRecord).Include(nv => nv.NvallergieNavigation).ToList();
                    var ras = new ResponseObjects.Response_RecordAllergies().ToResponseList(oRecordAllergies);
                    response = Ok(new
                    {
                        ResponseCode = "00",
                        Message = "Success",
                        Record = new { IDRecord = oRecord.Idrecord, IDPaciente = oRecord.Idpatient },
                        Paciente = new { Nombre = oPatient.FirstName + " " + oPatient.LastName, Identificacion = oPatient.IdentificationNumber, Tel1 = oPatient.Tel1, Tel2 = oPatient.Tel2, Email = oPatient.Email, Direccion1 = oPatient.Address1, Direccion2 = oPatient.Address2 },
                        Alergias = ras
                    });
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }

        [HttpPost(Name = "SetRecordVist")]
        public IActionResult SetRecordVist(Request_RecordVisit request)
        {
            IActionResult response = BadRequest();
            var visitDate = new DateTime();
            var doctor = new Doctor();
            try
            {
            if (request.idRecord == 0 || request.Doctor_Identification.Trim() == string.Empty || request.SpecialtyCode.Trim() == string.Empty || request.VisitDate.Trim() == string.Empty)
            {
                response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, IDDoctor, SpecialtyCode, VisitDate" });
            }
            else if (!DateTime.TryParse(request.VisitDate, out visitDate))
            {
                response = BadRequest(new { ResponseCode = 99, Message = "El parametro VisitDate tiene el formato incorrecto" });
            }
            else if (!doctor.ReviewDoctor(request.Doctor_Identification))
            {
                response = BadRequest(new { ResponseCode = 99, Message = "RecordVisit no insertado, El doctor no fue encontrado en el padron electoral" });
            }
            else
            {
                var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                var doctorReview = new Doctor();
                    var medicalCenter = db.MedicalCenters.Where(mc => mc.Token == request.MedicalCenter_Token).ToList();
                    if (medicalCenter.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                    }
                    else if(doctorReview.ReviewDoctor(request.Doctor_Identification))
                    {
                        db.RecordVisits.Add(new RecordVisit()
                        {
                            Idrecord = request.idRecord,
                            Iddoctor = db.Doctors.Where(d => d.IdentificationNumber == request.Doctor_Identification).ToList()[0].Iddoctor,
                            Idspecialty = db.Specialtys.Where(sp => sp.Code == request.SpecialtyCode).ToList()[0].Idspecialty,
                            Observations = request.Observations,
                            Indications = request.Indications,
                            IdMedicalCenter = db.MedicalCenters.Where(m=>m.Token == request.MedicalCenter_Token).First().IdmedicalCenter,
                            VisitDate = DateTime.Parse(request.VisitDate),
                            CreateUser = medicalCenter[0].Description,
                            CreateDate = DateTime.Now
                        });
                        db.SaveChanges();
                        response = Ok(new { ResponseCode = "00", Message = "Success", IdRecord = request.idRecord });
                    }
                    else
                    {
                        response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error con la identificacion del doctor. Intentelo mas tarde, de volver a ocurrir comuniquese con el administrador." });
                    }
            }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = 99, Message = "Ha ocurriedo un error: " + e.Message });
            }
            return response;
        }
  
        [HttpPost(Name = "SetRecordOperation")]
        public IActionResult SetRecordOperation(Request_RecordOperation request)
        {
            IActionResult response = BadRequest();
            var visitDate = new DateTime();
            var doctor = new Doctor();
            try
            {
                if (request.idRecord == 0 || request.Doctor_Identification.Trim() == string.Empty || request.OperationCode.Trim() == string.Empty || request.OperationDate.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, IDDoctor, OperationCode, OperationDate" });
                }
                else if (!DateTime.TryParse(request.OperationDate, out visitDate))
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "El parametro OperationDate tiene el formato incorrecto" });
                }
                else if (!doctor.ReviewDoctor(request.Doctor_Identification))
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "RecordOperation no insertado, El doctor no fue encontrado en el padron electoral" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    var doctorReview = new Doctor();
                    var medicalCenter = db.MedicalCenters.Where(mc => mc.Token == request.MedicalCenter_Token).ToList();
                    if (medicalCenter.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                    }
                    else if(doctorReview.ReviewDoctor(request.Doctor_Identification))
                    {
                        db.RecordOperations.Add(new RecordOperation()
                        {
                            Idrecord = request.idRecord,
                            Iddoctor = db.Doctors.Where(d => d.IdentificationNumber == request.Doctor_Identification).ToList()[0].Iddoctor,
                            Idoperation = db.Operations.Where(op => op.Code == request.OperationCode).ToList()[0].Idoperation,
                            Operationdate = DateTime.Parse(request.OperationDate),
                            IdMedicalCenter = db.MedicalCenters.Where(m=>m.Token == request.MedicalCenter_Token).First().IdmedicalCenter,
                            CreateUser = medicalCenter[0].Description,
                            CreateDate = DateTime.Now
                        });
                        db.SaveChanges();
                        response = Ok(new { ResponseCode = "00", Message = "Success", IdRecord = request.idRecord });
                    }
                    else
                    {
                        response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error con la identificacion del doctor. Intentelo mas tarde, de volver a ocurrir comuniquese con el administrador." });
                    }
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = 99, Message = "Ha ocurriedo un error: " + e.Message });
            }
            return response;
        }
  
        [HttpPost(Name = "SetRecordAnalysis")]
        public IActionResult SetRecordAnalysis(Request_RecordAnalysis request)
        {
            IActionResult response = BadRequest();
            var analysisDate = new DateTime();
            try
            {
                if (request.idRecord == 0 || request.AnalysisCode.Trim() == string.Empty || request.ResultsObservations.Trim() == string.Empty || request.ResultCode.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, AnalysisCode, ResultsObservations, ResultCode" });
                }
                else if (!DateTime.TryParse(request.AnalysisDate, out analysisDate))
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "El parametro AnalysisDate tiene el formato incorrecto" });
                }
                else
                {

                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    var medicalCenter = db.MedicalCenters.Where(mc => mc.Token == request.MedicalCenter_Token).ToList();
                    var nvResults = db.Namevalues.Where(x => x.GroupName == "RESULTTYPEBINARY_GROUP" && x.Customstring1 == request.ResultCode).ToList();
                    var analysis = db.Analyses.Where(a => a.Code == request.AnalysisCode).ToList();
                    if (medicalCenter.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                    }
                    else if (nvResults.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El ResultCode no existe en la base de datos" });
                    }
                    else if (analysis.Count > 0)
                    {
                        db.RecordAnalyses.Add(new RecordAnalysis()
                        {
                            Idrecord = request.idRecord,
                            Idanalysis = analysis[0].Idanalysis,
                            PublicResults = request.PublicResults,
                            Result = nvResults[0].Description,
                            ResultsObservations = request.ResultsObservations,
                            AnalysisDate = DateTime.Parse(request.AnalysisDate),
                            CreateUser = medicalCenter[0].Description,
                            CreateDate = DateTime.Now
                        });
                        db.SaveChanges();
                        response = Ok(new { ResponseCode = "00", Message = "Success", IdRecord = request.idRecord });
                    }
                    else
                    {
                        response = BadRequest(new { Response = "99", Message = "El AnalysisCode suministrado no existe en la base de datos." });
                    }
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = 99, Message = "Ha ocurriedo un error: " + e.Message });
            }
            return response;
        }

        [HttpPost(Name = "SetRecordInterment")]
        public IActionResult SetRecordInterment(Request_RecordInterment request)
        {
            IActionResult response = BadRequest();
            DateTime intermentDate = new DateTime();
            try
            {
                if (request.idRecord == 0 || request.MedicalCenter_Token.Trim() == string.Empty || request.ReasonInterment.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, Token, Reason" });
                }
                else if (!DateTime.TryParse(request.IntermentDate, out intermentDate))
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "El parametro AnalysisDate tiene el formato incorrecto" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    var medicalCenter = db.MedicalCenters.Where(mc => mc.Token == request.MedicalCenter_Token).ToList();
                    if (medicalCenter.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                    }
                    else
                    {
                        db.RecordInterments.Add(new RecordInterment()
                        {
                            Idrecord = request.idRecord,
                            IdmedicalCenter = medicalCenter[0].IdmedicalCenter,
                            Reason = request.ReasonInterment,
                            Intermentdate = intermentDate,
                            CreateUser = medicalCenter[0].Description,
                            CreateDate = DateTime.Now
                        });
                        db.SaveChanges();
                        response = Ok(new { ResponseCode = "00", Message = "Success", IdRecord = request.idRecord });
                    }
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }
        
        [HttpPost(Name = "SetRecordEmergencyEntry")]
        public IActionResult SetRecordEmergencyEntry(Request_RecordEmergencyEntry request)
        {
            IActionResult response = BadRequest();
            DateTime entryDate = new DateTime();
            try
            {
                if (request.idRecord == 0 || request.MedicalCenter_Token.Trim() == string.Empty || request.ReasonInterment.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, Token, Reason" });
                }
                else if (!DateTime.TryParse(request.EntryDate, out entryDate))
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "El parametro AnalysisDate tiene el formato incorrecto" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    var medicalCenter = db.MedicalCenters.Where(mc => mc.Token == request.MedicalCenter_Token).ToList();
                    if (medicalCenter.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                    }
                    else
                    {
                        db.RecordEmergencyemtries.Add(new RecordEmergencyEntry()
                        {
                            Idrecord = request.idRecord,
                            IdmedicalCenter = medicalCenter[0].IdmedicalCenter,
                            Reason = request.ReasonInterment,
                            Intermentdate = entryDate,
                            CreateUser = medicalCenter[0].Description,
                            CreateDate = DateTime.Now
                        });
                        db.SaveChanges();
                        response = Ok(new { ResponseCode = "00", Message = "Success", IDRecord = request.idRecord });
                    }
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }

        [HttpPost(Name = "SetRecordVaccines")]
        public IActionResult SetRecordVaccines(Request_RecordVaccines request)
        {
            IActionResult response = BadRequest();
            try
            {
                if (request.idRecord == 0 || request.Codigos_Vacunas.Count == 0)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, VaccinesCode"});
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    var medicalCenter = db.MedicalCenters.Where(mc => mc.Token == request.MedicalCenter_Token).ToList();
                    if (medicalCenter.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                    }
                    else
                    {
                        foreach (var vaccineCode in request.Codigos_Vacunas)
                        {
                            var vaccineResult = db.Namevalues.Where(nv => nv.Customstring1 == vaccineCode.Codigo).ToList();
                            if (vaccineResult.Count == 0)
                            {
                                throw new Exception("El codigo " + vaccineCode + " no existe en la base de datos.");
                            }
                            else
                            {
                                db.RecordVaccines.Add(new RecordVaccine()
                                {
                                    Idrecord = request.idRecord,
                                    Nvvaccine = vaccineResult[0].Idnamevalue,
                                    CreateUser = medicalCenter[0].Description,
                                    CreateDate = DateTime.Now
                                });
                            }
                        }
                        db.SaveChanges();
                        response = Ok(new { ResponseCode = "00", Message = "Success", IdRecord = request.idRecord });
                    }
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }

        [HttpPost(Name = "SetRecordAllergies")]
        public IActionResult SetRecordAllergies(Request_RecordAllergies request) 
        {
            IActionResult response = BadRequest();
            try
            {
                if (request.idRecord == 0 || request.Codigos_Alergias.Count == 0)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, AllergiesCodes" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    var medicalCenter = db.MedicalCenters.Where(mc => mc.Token == request.MedicalCenter_Token).ToList();
                    if (medicalCenter.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                    }
                    else
                    {
                        foreach (var allergie in request.Codigos_Alergias)
                        {
                            var allergieResult = db.Namevalues.Where(nv => nv.Customstring1 == allergie.Codigo).ToList();
                            if (allergieResult.Count == 0)
                            {
                                throw new Exception("El codigo " + allergie.Codigo + " no existe en la base de datos.");
                            }
                            else
                            {
                                db.RecordAllergies.Add(new RecordAllergy()
                                {
                                    Idrecord = request.idRecord,
                                    Nvallergie = allergieResult[0].Idnamevalue,
                                    CreateUser = medicalCenter[0].Description,
                                    CreateDate = DateTime.Now
                                });
                            }
                        }
                        db.SaveChanges();
                        response = Ok(new { ResponseCode = "00", Message = "Success", IdRecord = request.idRecord });
                    }
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }

    }
}
