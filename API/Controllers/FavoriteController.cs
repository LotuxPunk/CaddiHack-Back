using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DAL;
using Model;
using DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [Route("myFavorites")]
        [HttpGet]
        [ProducesResponseType(typeof(FavoriteDTOout), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            string person = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if(person == null)
            {
                return NotFound();
            }
            var myFavorites = from i in _context.Favorite
                              where i.PersonNavigation.Email == person
                              select new FavoriteDTOout()
                              {
                                  Name = i.ShopNavigation.Name,
                                  Address = i.ShopNavigation.Address,
                                  PicturePath = i.ShopNavigation.PicturePath,
                                  Description = i.ShopNavigation.Description,
                                  Locality = i.ShopNavigation.LocalityNavigation.Name,
                                  ShopId = i.ShopNavigation.ShopId,
                              };

            return Ok(myFavorites);
    }

        [HttpPost("{shopId}")]
        [ProducesResponseType(typeof(Favorite), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(int shopId)
        {
            string email = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Person personFound = _context.Person.FirstOrDefault(person => person.Email == email);
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

            Favorite favorite = new Favorite();
            favorite.Person = personFound.PersonId;
            favorite.Shop = shopId;

            Favorite heart = _context.Favorite.Where(x => x.Person == personFound.PersonId && x.Shop == shop.ShopId).FirstOrDefault();

            if(heart != null)
            {
                _context.Favorite.Remove(heart);
            }

            else
            {
                _context.Favorite.Add(favorite);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
        

    }
}

