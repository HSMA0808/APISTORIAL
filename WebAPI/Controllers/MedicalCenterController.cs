using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MedicalCenterController : ControllerBase
    {
        [HttpPost(Name = "Register")]
        public IActionResult Register(string Descripcion, string RNC, string Tel1, string? Tel2, string Email1, string? Email2, string NombreContacto, string Referencia)
        {
            IActionResult response = BadRequest();
            try
            {
                if (Descripcion.Trim() == string.Empty || RNC.Trim() == string.Empty || Tel1.Trim() == string.Empty || Email1.Trim() == string.Empty || NombreContacto.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "Los siguientes campos son reqeuridos: Descripcion, RNC, Tel1, Email1, NombreContacto" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    if (db.MedicalCenters.Where(mc => mc.Rnc == RNC).ToList().Count == 1)
                    {
                        throw new Exception("Ya existe un centro medico registrado con el RNC " + RNC);
                    }
                    db.MedicalCenters.Add(new MedicalCenter() { 
                        Description = Descripcion,
                        Rnc = RNC,
                        Tel1 = Tel1,
                        Tel2 = Tel2,
                        Email1 = Email1,
                        Email2 = Email2,
                        NameContact = NombreContacto,
                        Token = Guid.NewGuid().ToString(),
                        NvstatusCenter = 7,
                        CreateUser = Referencia,
                        CreateDate = DateTime.Now
                    });;
                    db.SaveChanges();
                    response = Ok(new { ResponseCode = "00", Message = "Success"});
                }
            }
            catch (Exception e)
            {
                response = BadRequest(new { ResponseCode = "99", Message = "Ha ocurrido un error: " + e.Message });
            }
            return response;
        }

        [HttpGet(Name = "GetmMedicalCenterList")]
        public IActionResult GetmMedicalCenterList(string ApistorialKey, int Estatus)
        { 
            IActionResult response = BadRequest();
            try
            {
                if (ApistorialKey == null || ApistorialKey.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El parametro 'ApistorialKey' no puede ser enviado null o vacio" });
                }
                else if (Estatus != 0 && Estatus != 6 && Estatus != 7)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "El parametro 'Estatus' debe tener uno de los siguientes valores: 6 - Activo, 7 - Pendiente, 0 - Todos" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    if (Estatus == 0)
                    {
                        var medicalCenterList = db.MedicalCenters.ToList();
                        response = Ok(new { ResponseCode = "00", Message = "Success", CentrosMedicos = medicalCenterList });
                    }
                    else
                    {
                        var medicalCenterList = db.MedicalCenters.Where(cm => cm.NvstatusCenter == Estatus).ToList();
                        response = Ok(new { ResponseCode = "00", Message = "Success", CentrosMedicos = medicalCenterList });
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

        [HttpGet(Name = "GetMedicalCenterStatus")]
        public IActionResult GetMedicalCenterStatus(string RNC, string MedicalCenterToken)
        {
            IActionResult response = BadRequest();
            try
            {
                if (RNC == null || RNC.Trim() == string.Empty || MedicalCenterToken == null || MedicalCenterToken.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "Los parametros 'RNC' y 'MedicalCenterToken' no pueden ser enviados vacios o nulos" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    var centrosMedico = db.MedicalCenters.Where(cm => cm.Rnc == RNC && cm.Token == MedicalCenterToken).ToList();
                    if (centrosMedico.Count == 0)
                    {
                        response = BadRequest(new { ResponseCode = "99", Message = "No se encontro un centro medico registrado con los parametros suministrados." });
                    }
                    else
                    {
                        response = Ok(new { ResponseCode = "00", Message = "Success", CentroMedico = centrosMedico[0] });
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
    }
}
