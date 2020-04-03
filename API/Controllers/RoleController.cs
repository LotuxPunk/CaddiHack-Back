using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DAL;
using Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiController
    {
        private readonly ILogger<LocalityController> _logger;
        private readonly CaddieHackContext _context;


        public RoleController(ILogger<LocalityController> logger, CaddieHackContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Shop), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_context.Role.ToArray());
        }

    }
}
