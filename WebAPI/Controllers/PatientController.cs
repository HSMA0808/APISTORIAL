using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.RequestObjects;
using WebAPI.ResponseObjects;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [EnableCors("MyPolicy")]
    [Route("[controller]/[action]")]
    public class PatientController : Controller
    {
        [HttpPost(Name = "RegisterPatient")]
        public IActionResult RegisterPatient(Request_Patient request)
        {
            IActionResult response = BadRequest();
            try
            {
                if (request.PrimerNombre.Trim() == string.Empty || request.Apellidos.Trim() == string.Empty || request.NumeroIdentificacion.Trim() == string.Empty || request.Codigo_TipoIdentificacion == String.Empty || request.Telefono1.Trim() == string.Empty || request.Email.Trim() == string.Empty || request.Direccion1.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "Los siguientes parametros no pueden ser enviados nulos o en 0: Nombre, Apellido, No. Identificacion, Tipo Identificacion, Telefono 1, Email 1, Direccion 1" });
                }
                else if (!StaticsOperations.validateIdentification(request.NumeroIdentificacion, "C"))
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El numero de identificacion suministrado es invalido" });
                }
                else if (request.Codigo_TipoIdentificacion != "C" && request.Codigo_TipoIdentificacion != "P")
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El tipo de identificacion suministrado es invalido" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                    var nvBlood = db.Namevalues.Where(nv => nv.GroupName == "BLOODTYPE_GROUP" && nv.Customstring1 == request.Codigo_TipoSangre).ToList();
                    var medicalCenterValid = db.MedicalCenters.Where(mc => mc.Token == request.MedicalCenter_Token && mc.NvstatusCenter == 6).ToList();
                    var patient = db.Patients.Where(p => p.IdentificationNumber == request.NumeroIdentificacion).ToList();
                    if (medicalCenterValid.Count() == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                    }
                    else if (patient.Count() == 1)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El cliente ya existe en la base de datos" });
                    }
                    else if(nvBlood.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El tipo de sangre suministrado es incorrecto" });
                    }
                    else
                    {
                        var identificationtype = db.Namevalues.Where(nv => nv.GroupName == "IDENTIFICATIONTYPE_GROUP" && nv.Customstring1 == request.Codigo_TipoIdentificacion).ToList()[0];
                        db.Patients.Add(new Patient()
                        {
                            FirstName = request.PrimerNombre,
                            LastName = request.Apellidos,
                            MiddleName = request.SegundoNombre,
                            Sex = request.Sexo,
                            NvidentificationType = identificationtype.Idnamevalue,
                            IdentificationNumber = request.NumeroIdentificacion,
                            NvbloodType = nvBlood[0].Idnamevalue,
                            Tel1 = request.Telefono1,
                            Tel2 = request.Telefono2,
                            Email = request.Email,
                            Address1 = request.Direccion1,
                            Address2 = request.Direccion2,
                            CreateDate = DateTime.Now,
                            CreateUser = medicalCenterValid[0].Description
                        });
                        db.SaveChanges();
                        response = Ok(new { ResponseCode = "00", Message = "Success" });
                    }
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(new { ResponseCode = 99, Message = "Ha ocurriedo un error: " + ex.Message });
            }

            return response;
        }

        [HttpGet(Name = "GetPatientByIdentificationNumber")]
        public IActionResult GetPatientByIdentificationNumber(string MedicalCenter_Token, string TipoIdentificacion, string NumeroIdentificacion)
        {
            IActionResult response = BadRequest();
            try
            {
                if (TipoIdentificacion != "C" && TipoIdentificacion != "P")
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El tipo de identificacion es invalido" });
                }
                else if (NumeroIdentificacion.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El numero de identificacion no puede ser enviado nulo o sin data" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                    var medicalCenterValid = db.MedicalCenters.Where(mc => mc.Token == MedicalCenter_Token && mc.NvstatusCenter == 6).ToList();
                    if (medicalCenterValid.Count() == 0)
                    {
                        response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                    }
                    else
                    {
                        var identificationType = db.Namevalues.Where(nv => nv.GroupName == "IDENTIFICATIONTYPE_GROUP" && nv.Customstring1 == TipoIdentificacion).ToList()[0];

                        var patient = db.Patients.Where(p => p.IdentificationNumber == NumeroIdentificacion && p.NvidentificationType == identificationType.Idnamevalue).ToList()[0];
                        var patientResponse = new Response_Patient().ToResponseEntity(patient);
                        response = Ok(new { ResponseCode = "00", Message = "Success", Paciente = patient });
                    }
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(new { ResponseCode = 99, Message = "Ha ocurriedo un error: " + ex.Message });
            }

            return response;
        }

        [HttpGet(Name = "GetPatients")]
        public IActionResult GetPatients(string MedicalCenter_Token)
        {
            IActionResult response = BadRequest();
            try
            {
                var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                var medicalCenterValid = db.MedicalCenters.Where(mc => mc.Token == MedicalCenter_Token && mc.NvstatusCenter == 6).ToList();
                if (medicalCenterValid.Count() == 0)
                {
                    response = BadRequest(new { ResponseCode = 99, Message = "El Token suministrado no es valido" });
                }
                else
                {
                    var patient = db.Patients.ToList();
                    var patientListResponse = new Response_Patient().ToResponseList(patient);
                    response = Ok(new { ResponseCode = "00", Message = "Success", Pacientes = patientListResponse });
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(new { ResponseCode = 99, Message = "Ha ocurriedo un error: " + ex.Message });
            }

            return response;
        }
    }
}
