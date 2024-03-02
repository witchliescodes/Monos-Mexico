using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNAM.PrimatesApi.Entidades;
using UNAM.PrimatesApi.Models;

namespace UNAM.PrimatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly ConservacionDb _dbContext;
        private readonly ILogger _logger;

        public DocumentController(ConservacionDb dbContext, ILogger<DocumentController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<Document>> Get([FromRoute] string userId)
        {
            var documents = await _dbContext.Documents.Where(x => x.UserId.Equals(userId)).ToListAsync();

            return Ok();
        }
    }
}
