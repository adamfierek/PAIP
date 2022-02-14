using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaipController : ControllerBase
    {
        private readonly LibraryService _libraryService;

        public PaipController(LibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet("test")] 
        public IActionResult Test()
        {
            return Ok("Witaj API!");
        }

        [HttpGet("validate/{pesel}")]
        public IActionResult Validate(string pesel)
        {
            var result = _libraryService.ValidatePesel(pesela);
            return Ok(result);
        }
    }
}
