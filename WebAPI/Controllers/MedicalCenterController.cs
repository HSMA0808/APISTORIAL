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
    }
}
