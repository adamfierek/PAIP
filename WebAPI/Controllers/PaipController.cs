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

        [HttpGet("validate")]
        public IActionResult Validate([FromQuery] string pesel)
        {
            var result = _libraryService.ValidatePesel(pesel);
            return Ok(result);
        }

        [HttpGet("prime/{number:int}")]
        public IActionResult IsPrime(int number)
        {
            var result = _libraryService.IsPrime(number);
            var message = result ? "Liczba jest pierwsza" : "Liczba nie jest pierwsza";
            return Ok(message);
        }
    }
}
