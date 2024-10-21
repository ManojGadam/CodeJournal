using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PersonalWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

    

        [HttpGet]
        public IActionResult GetBoolean()
        {
            return Ok(new { ret=true });
        }

    }
}