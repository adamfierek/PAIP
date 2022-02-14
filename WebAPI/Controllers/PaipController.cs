using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaipController : ControllerBase
    {
        [HttpGet] //HttpPost, HttpPut, HttpDelete
        public IActionResult Test()
        {
            return Ok("Witaj API!");
        }
    }
}
