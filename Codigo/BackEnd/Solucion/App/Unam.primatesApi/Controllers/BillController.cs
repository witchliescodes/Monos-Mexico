using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNAM.PrimatesApi.Entidades;
using UNAM.PrimatesApi.Models;

namespace UNAM.PrimatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly ConservacionDb _dbContext;
        private readonly ILogger _logger;

        public BillController(ConservacionDb dbContext, ILogger<BillController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<UserTenant>> Get([FromBody] string id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId.Equals(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<UserTenant>> GetAll()
        {
            var users = await _dbContext.Users.ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }
        
    }
}
