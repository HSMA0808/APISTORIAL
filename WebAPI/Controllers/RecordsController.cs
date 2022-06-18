using Microsoft.AspNetCore.Mvc;
using ApistorialModels.Models;
using System.Configuration;

namespace WebAPI.Controllers
{
        [ApiController]
        [Route("[controller]")]
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
                }
                Record record = new Record();
                var oRecord = record.Find(IdRecord, "");
                return Ok(new {
                    ResponseCode = "00",
                    Message = "Success",
                    IdRecord = oRecord.ID,
                    MedicalCenterCreator = oRecord.medicalCenter_Creator.Description,
                    Create_Date = oRecord.CreateDate,
                    LastMedicalCenterUpdate = oRecord.last_MedicalCenterUpdate.Description,
                    Update_Date = oRecord.UpdateDate,
                    Patient = oRecord.patient
                });
            }
    }
}
