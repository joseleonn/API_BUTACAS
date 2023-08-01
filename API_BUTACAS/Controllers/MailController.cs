using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service.IServices;

namespace API_BUTACAS.Controllers
{
    [EnableCors("RulesCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {
        private readonly IMailService _service;

        public MailController(IMailService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("EnviarMail")]
        public IActionResult EnviarMail([FromBody] EnviarMailDTO mail)
        {
            try
            {
                _service.EnviarMailmethod(mail);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
