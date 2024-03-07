using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNAM.PrimatesApi.Entidades;

namespace UNAM.PrimatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly ConservacionDb _dbContext;
        private readonly ILogger _logger;

        public RolController(ConservacionDb dbContext, ILogger<RolController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        [HttpGet("/id")]
        public async Task<ActionResult> Get([FromRoute] string id)
        {
            var rol = await _dbContext.RolTenants.FirstOrDefaultAsync(x=>x.RolId.Equals(id));

            if(rol == null)
            {
                return NotFound();
            }
            return Ok(rol);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var roles = await _dbContext.RolTenants.ToListAsync();

            if (roles == null || roles.Count == 0)
            {
                return NotFound();
            }
            return Ok(roles);
        }
    }
}
