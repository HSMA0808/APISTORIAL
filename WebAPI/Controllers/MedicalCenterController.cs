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
        public IActionResult Register(string Description, string RNC, string Tel, string Tel2, string Email, string Email2, string NameContact)
        {
            IActionResult response;
            response = Ok(new { ResponseCode = 00, Message = "Success" });
            return response;
        }
    }
}
