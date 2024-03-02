using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNAM.PrimatesApi.Entidades;
using UNAM.PrimatesApi.Models;

namespace UNAM.PrimatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ConservacionDb _dbContext;
        private ILogger _logger;

        public AuthController(ConservacionDb dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        
    }
}
