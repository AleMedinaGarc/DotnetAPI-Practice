using Microsoft.AspNetCore.Mvc;

namespace APICarData.Api.Controllers
{
    public class InfoController : Controller
    {
        [HttpGet]
        [Route("api/version")]
        public IActionResult GetVersion()
        {
            return Ok(new { message = "Tu API Name- 3.1.1" });
        }

        [HttpGet]
        [Route("api/swagger")]
        public IActionResult IsSwagger()
        {
            return Ok(new { swagger = "true" /*this.config.Swagger*/ });
        }

        [HttpGet]
        [Route("api/error")]
        public IActionResult ApiError()
        {
            var t = System.DateTime.Now.Hour - 14;
            return Ok(new { response = 10 / t });
        }

    }
}
