using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ApiController
    {
        private readonly ILogger<PersonController> _logger;
        private readonly CaddieHackContext _context;

        public ShoppingListController(ILogger<PersonController> logger, CaddieHackContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("myList")]
        public IActionResult Get()
        {
            string person = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (person == null)
            {
                return NotFound();
            }

            var shoppingLists = from i in _context.ShoppingList
                                where i.PersonNavigation.Email == person
                                select new ShoppingList();

            return Ok(shoppingLists);
        }

    }
}
