using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Logging;

namespace WebApplication1.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors(PolicyName = "AllowAll")]
    [Authorize(AuthenticationSchemes = "LoginForMicrosoftlUser", Roles = "Superadmin,Admin")]

    public class StudentController:ControllerBase
    {
        private readonly ILogs _logs;
        public StudentController(ILogs logs)
        {
            _logs = logs;
            
        }

        [HttpGet]    
        public String GetStudentName()
        {
            _logs.Log("Get StudentName Method Started");
            return "Studen Roll No 1";
        }
    }
}
