using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using WebAPI.Models;
using Microsoft.AspNetCore.Cors;
using WebAPI.ResponseObjects;
using WebAPI.RequestObjects;


namespace WebAPI.Controllers
{
    [ApiController]
    [EnableCors("MyPolicy")]
    [Route("[controller]/[action]")]
    public class MedicalCenterController : ControllerBase
    {
        [HttpPost(Name = "Register")]
        public IActionResult Register(Request_MedicalCenter request)
        {
            IActionResult response = BadRequest();
            try
            {
                if (request.Descripcion.Trim() == string.Empty || request.Rnc.Trim() == string.Empty || request.Tel1.Trim() == string.Empty || request.Email1.Trim() == string.Empty || request.NombreContacto.Trim() == string.Empty)
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "Los siguientes campos son reqeuridos: Descripcion, RNC, Tel1, Email1, NombreContacto" });
                }
                else if (StaticsOperations.validateIdentification(request.Rnc, 7))
                {
                    response = BadRequest(new { ResponseCode = "99", Message = "Numeracion RNC invalida" });
                }
                else
                {
                    var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                    if (db.MedicalCenters.Where(mc => mc.Rnc == request.Rnc).ToList().Count == 1)
                    {
                        throw new Exception("Ya existe un centro medico registrado con el RNC " + request.Rnc);
                    }
                    db.MedicalCenters.Add(new MedicalCenter()
                    {
                        Description = request.Descripcion,
                        Rnc = request.Rnc,
                        Tel1 = request.Tel1,
                        Tel2 = request.Tel2,
                        Email1 = request.Email1,
                        Email2 = request.Email2,
                        NameContact = request.NombreContacto,
                        Token = Guid.NewGuid().ToString(),
                        NvstatusCenter = 7,
                        CreateUser = request.Referencia,
                        CreateDate = DateTime.Now
                    }); ;
                    db.SaveChanges();
                    response = Ok(new { ResponseCode = "00", Message = "Success" });
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
                        var response_medicalCenter = new Response_MedicalCenter();
                        var cmList = response_medicalCenter.ToResponseList(medicalCenterList);
                        response = Ok(new { ResponseCode = "00", Message = "Success", CentrosMedicos = cmList });
                    }
                    else
                    {
                        var medicalCenterList = db.MedicalCenters.Where(cm => cm.NvstatusCenter == Estatus).ToList();
                        var response_medicalCenter = new Response_MedicalCenter();
                        var cmList = response_medicalCenter.ToResponseList(medicalCenterList);
                        response = Ok(new { ResponseCode = "00", Message = "Success", CentrosMedicos = cmList });
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
                        var response_medicalCenter = new Response_MedicalCenter();
                        var cmList = response_medicalCenter.ToResponseList(centrosMedico);
                        response = Ok(new { ResponseCode = "00", Message = "Success", CentroMedico = cmList[0] });
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
