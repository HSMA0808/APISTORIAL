using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using WebAPI.Models;

namespace WebAPI.Controllers
{
        [ApiController]
        [Route("[controller]/[action]")]

        public class RecordsController : ControllerBase
        {
            [HttpGet(Name = "GetRecordById")]
            public IActionResult GetRecordById(int IdRecord = 1)
            {
                switch (IdRecord)
                {
                    case < 0:
                    case 0:
                        return BadRequest(new {
                            Message = "El parametro IdRecord debe puede ser 0 o nulo"
                        });
                    break;
                }
                var db = new APISTORIAL_v1Context(new DbContextOptions<APISTORIAL_v1Context>());
                var nvList = db.Namevalues.ToList();
                return Ok(new { 
                    ResponseCode = "00",
                    Message = "Success",
                    NameValueList = nvList
                });
            }
            [HttpPost(Name = "NewRecord")]
            public IActionResult NewRecord(string record)
            {
            return Ok(new
            {
                ResponseCode = "00",
                Message = "Success",
            });
        }
        }
}
