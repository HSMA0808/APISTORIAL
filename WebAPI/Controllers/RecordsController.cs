using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using WebAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers
{
        [ApiController]
        [EnableCors("MyPolicy")]
        [Route("[controller]/[action]")]

        public class RecordsController : ControllerBase
        {
            [HttpGet(Name = "GetRecord")]
            public IActionResult GetRecord(string Identification = "")
            {
                bool NewRecord = false;
                try
                {
                    Record oRecord = new Record();
                    if (Identification.Trim() == string.Empty)
                    {
                        return BadRequest(new
                        {
                            ResponseCode = "99",
                            Message = "El parametro <Identification> no puede ser enviado sin data"
                        });
                    }
                    else
                    {
                        var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                        var patientList = db.Patients.Where(x => x.IdentificationNumber == Identification).ToList();
                        if (patientList.Count() <= 0)
                        {
                            NewRecord = true;
                            //Logica para crear el paciente consultando el padron
                            return Ok(new
                            {
                                ResponseCode = "00",
                                Message = "Success",
                                oRecord
                            });
                        }
                        else
                        {
                            var idPatient = patientList[0].Idpatient;
                            oRecord = db.Records.Where(x => x.Idpatient == idPatient).Include(r => r.RecordVisits).ToList()[0];
                            var oRecordVisits = oRecord.RecordVisits.ToList()[0];
                            var oSpecialty = db.Specialtys.Find(oRecordVisits.Idspecialty);
                        var response = new
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
                                Nombre = patientList[0].FirstName + " " + patientList[0].LastName,
                                Identificacion = patientList[0].IdentificationNumber,
                                Telefono = patientList[0].Tel1,
                                Telefono2 = patientList[0].Tel2,
                                Email = patientList[0].Email,
                                Direccion1 = patientList[0].Address1,
                                Direccion2 = patientList[0].Address2
                            },
                            RecordVisits = new
                            {
                                IDRecordVisit = oRecordVisits.IdrecordVisits,
                                IDDoctor = oRecordVisits.Iddoctor,
                                EspecialidadMedica = oSpecialty.Description,
                                Observaciones = oRecordVisits.Observations,
                                Indicaciones = oRecordVisits.Indications,
                                Fecha_Visita = oRecordVisits.VisitDate
                            }
                        };
                        return Ok(JsonConvert.SerializeObject(response));
                        }
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        ResponseCode = "99",
                        Message = "Ha ocurrido un error: " + e.Message
                    });
                }
            }

            
            [HttpPost(Name = "SetRecordVist")]
            public IActionResult SetRecordVist(int idRecord, string Doctor_Identification, string SpecialtyCode, string Observations, string Indications, string VisitDate, string Referencia)
            {
                IActionResult response = BadRequest();
                var visitDate = new DateTime();
                var doctor = new Doctor();
                try
                {
                if (idRecord == 0 || Doctor_Identification.Trim() == string.Empty || SpecialtyCode.Trim() == string.Empty || VisitDate.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, IDDoctor, SpecialtyCode, VisitDate" });
                }
                else if (!DateTime.TryParse(VisitDate, out visitDate))
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "El parametro VisitDate tiene el formato incorrecto" });
                }
                else if (!doctor.ReviewDoctor(Doctor_Identification))
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "RecordVisit no insertado, El doctor no fue encontrado en el padron electoral" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    db.RecordVisits.Add(new RecordVisit()
                    {
                        Idrecord = idRecord,
                        Iddoctor = db.Doctors.Where(d => d.IdentificationNumber == Doctor_Identification).ToList()[0].Iddoctor,
                        Idspecialty = db.Specialtys.Where(sp => sp.Code == SpecialtyCode).ToList()[0].Idspecialty,
                        Observations = Observations,
                        Indications = Indications,
                        VisitDate = DateTime.Parse(VisitDate),
                        CreateUser = Referencia,
                        CreateDate = DateTime.Now
                    });
                    db.SaveChanges();
                }
                }
                catch (Exception e)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Ha ocurriedo un error: " + e.Message });
                }
                return response;
            }

            [HttpPost(Name = "SetRecordOperation")]
            public IActionResult SetRecordOperation(int idRecord, string Doctor_Identification, string OperationCode, string OperationDate, string Referencia)
            {
                IActionResult response = BadRequest();
                var visitDate = new DateTime();
                var doctor = new Doctor();
                try
                {
                    if (idRecord == 0 || Doctor_Identification.Trim() == string.Empty || OperationCode.Trim() == string.Empty || OperationDate.Trim() == string.Empty)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, IDDoctor, OperationCode, OperationDate" });
                    }
                    else if (!DateTime.TryParse(OperationDate, out visitDate))
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El parametro OperationDate tiene el formato incorrecto" });
                    }
                    else if (!doctor.ReviewDoctor(Doctor_Identification))
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "RecordOperation no insertado, El doctor no fue encontrado en el padron electoral" });
                    }
                    else
                    {
                        var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                        db.RecordOperations.Add(new RecordOperation()
                        {
                            Idrecord = idRecord,
                            Iddoctor = db.Doctors.Where(d => d.IdentificationNumber == Doctor_Identification).ToList()[0].Iddoctor,
                            Idoperation = db.Operations.Where(op => op.Code == OperationCode).ToList()[0].Idoperation,
                            Operationdate = DateTime.Parse(OperationDate),
                            CreateUser = Referencia,
                            CreateDate = DateTime.Now
                        });
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Ha ocurriedo un error: " + e.Message });
                }
                return response;
            }

            [HttpPost(Name = "SetRecordAnalysis")]
            public IActionResult SetRecordAnalysis(int idRecord, string AnalysisCode, bool PublicResults, string Results, string AnalysisDate, string Referencia)
            {
                IActionResult response = BadRequest();
                var analysisDate = new DateTime();
                var doctor = new Doctor();
                try
                {
                    if (idRecord == 0 || AnalysisCode.Trim() == string.Empty || Results.Trim() == string.Empty)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "Les siguientes parametros no pueden ser enviados nulos: IDRecord, AnalysisCode, Results" });
                    }
                    else if (!DateTime.TryParse(AnalysisDate, out analysisDate))
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El parametro AnalysisDate tiene el formato incorrecto" });
                    }
                    else
                    {
                        var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                        db.RecordAnalyses.Add(new RecordAnalysis()
                        {
                            Idrecord = idRecord,
                            Idanalysis = db.Analyses.Where(a => a.Code == AnalysisCode).ToList()[0].Idanalysis,
                            PublicResults = PublicResults,
                            Results = Results,
                            AnalysisDate = DateTime.Parse(AnalysisDate),
                            CreateUser = Referencia,
                            CreateDate = DateTime.Now
                        });
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "Ha ocurriedo un error: " + e.Message });
                }
                return response;
            }

    }
}
