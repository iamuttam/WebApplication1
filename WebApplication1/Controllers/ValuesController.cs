using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Logging;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(PolicyName = "AllowOnlygoogle")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogs _logs;
        public ValuesController(ILogs logs)
        {
            _logs = logs;
                
        }

        [HttpGet]
        public string ValueDetails()
        {
            _logs.Log("Welcomee from ValueController..");
            return "I Love My Family..";

        }
    }
}
