using AdvertisingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformsController(PlatformService service) : ControllerBase
    {
        private readonly PlatformService _service = service;

        [HttpPost("upload")]
        public IActionResult Upload([FromQuery] string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return BadRequest("File not found");

            _service.LoadFromFile(filePath);
            return Ok("Platforms loaded");
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest("Location is required");

            var result = _service.Search(location);
            return Ok(result);
        }
    }
}
