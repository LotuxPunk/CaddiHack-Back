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
using System.Security.Claims;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ApiController
    {
        private readonly ILogger<LocalityController> _logger;
        private readonly CaddieHackContext _context;


        public FavoriteController(ILogger<LocalityController> logger, CaddieHackContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("myFavorites")]
        [ProducesResponseType(typeof(Favorite), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            string person = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if(person == null)
            {
                return NotFound();
            }
            var myFavorites = from i in _context.Favorite
                                where i.PersonNavigation.Email == person
                                select new Favorite();

            return Ok(myFavorites);
        }

        /*
        [HttpPost]
        [ProducesResponseType(typeof(Favorite), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] int shopId)
        {
            string email = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (email == null)
            {
                return NotFound();
            }

            var person = from i in _context.Person
                            where i.Email == email
                            select new Person();

            Shop shop = _context.Shop.Find(shopId);
            if (shop == null)
            {
                return NotFound();
            }

            Favorite favorite = new Favorite(person.Pe, shop)
             _context.Rental.Add(rental);
            await _context.SaveChangesAsync();
            return Created($"api/Picture/{rental.RentalId}", rental);



            return Ok(myFavorites);
        }
        */

    }
}

