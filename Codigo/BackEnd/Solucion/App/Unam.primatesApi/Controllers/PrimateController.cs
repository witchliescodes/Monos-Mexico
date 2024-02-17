using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UNAM.PrimatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimateController : ControllerBase
    {
        private ILogger<PrimateController> _logger;

        public PrimateController(ILogger<PrimateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }


        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

    }
}
