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

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ApiController
    {
        private readonly ILogger<PersonController> _logger;
        private readonly CaddieHackContext _context;

        public PersonController(ILogger<PersonController> logger, CaddieHackContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Person.ToArray());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            return Ok(_context.Person.Find(id));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Person), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult<Person>> Post([FromBody]Person person)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Boolean emailExist = await _context.Person.AnyAsync(per => per.Email == person.Email);

            if(emailExist)
            {
                // TODO
            }

            Boolean localityExist = await _context.Locality.AnyAsync(loc => loc.LocalityId == person.Locality);

            if(!localityExist)
            {
                // TODO
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
            return Created($"api/Person/{person.PersonId}", person);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
               // TODO
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}