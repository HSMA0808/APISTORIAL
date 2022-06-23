using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WebAPI.Controllers
{
        [ApiController]
        [Route("[controller]/[action]")]

        public class RecordsController : ControllerBase
        {
        [HttpGet(Name = "GetRecord")]
        public IActionResult GetRecord(string Identification = "")
        {
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
                    var patientList = db.Patients.Where(x => x.IdentificationNumber == Identification).ToList().ToList();
                    if (patientList.Count() <= 0)
                    {
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
                        return Ok(new
                        {
                            ResponseCode = "00",
                            Message = "Success",
                            Record = new
                            {
                                IDRecord = oRecord.Idrecord,
                                IDPatient = oRecord.Idpatient,
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
                        });
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

    }
}
