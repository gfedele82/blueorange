using Contracts.Engine;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace Test.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEngineSender engine;

        public EmailController(IEngineSender _engine)
        {
            engine = _engine;
        }

        [HttpPost("SendEmail")]
        public async Task<EmailResponse> SendEmail(Email email)
        {
            return await engine.ProcessSender(email);
        }
    }
}
