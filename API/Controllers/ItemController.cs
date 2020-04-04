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
using DTO;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ApiController
    {
        private readonly ILogger<LocalityController> _logger;
        private readonly CaddieHackContext _context;


        public ItemController(ILogger<LocalityController> logger, CaddieHackContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            Shop shopFound = await _context.Shop.FindAsync(id);

            if(shopFound == null)
            {
                return NotFound();
            }

            var shopItems = from i in _context.Item
                            where i.ShopNavigation == shopFound
                            select new ItemDTOout()
                            {
                                Label = i.Label,
                                Unit = i.Unit,
                                Price = i.Price,
                                Item = i.Id
                            };
 
            return Ok(shopItems);
        }

        /*
        [HttpPost]
        [ProducesResponseType(typeof(Person), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult<Person>> Post([FromBody]Person person)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Boolean emailExist = await _context.Person.AnyAsync(per => per.Email == person.Email);

            if (emailExist)
            {
                throw new BusinessException(ExceptionConstants.EmailAlreadyUsed, StatusCodes.Status400BadRequest);
            }

            Boolean localityExist = await _context.Locality.AnyAsync(loc => loc.LocalityId == person.Locality);

            if (!localityExist)
            {
                throw new BusinessException(ExceptionConstants.LocalityNotFound, StatusCodes.Status404NotFound);
            }

            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(person.Password));


                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                person.Password = builder.ToString();

            }

            _context.Person.Add(person);
            await _context.SaveChangesAsync();
            return Created($"api/Item/{person.PersonId}", person);
        }
        */




    }
}


