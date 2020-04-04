using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Model;
using DTO;
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
                                where i.OwnerNavigation.Email == person
                                select new ShoppingListDTOout()
                                {
                                    Name = i.Name,
                                    Delivered = i.Delivered,
                                    Shop = i.ShopNavigation.Name,
                                    Owner = i.OwnerNavigation.FirstName,
                                    DelivererName = i.DelivererNavigation.FirstName,
                                    TotalPrice = i.ShoppingListItem.Sum(x => x.Quantity * x.ItemNavigation.Price),
                                    NbItems = i.ShoppingListItem.Sum(x => x.Quantity)
                                };

            return Ok(shoppingLists);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            string email = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            if (email == null)
            {
                return NotFound();
            }

            ShoppingList shoppingList =  await _context.ShoppingList.FindAsync(id);
            Person deliverer = _context.Person.FirstOrDefault(person => person.Email == email);

            if (shoppingList == null)
            {
                return NotFound();
            }

            shoppingList.Deliverer = deliverer.PersonId;

            _context.Attach(shoppingList);
            _context.Entry(shoppingList).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShoppingListDTOin shoppingListDTOin)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string email = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Person personFound = _context.Person.FirstOrDefault(person => person.Email == email);

            if (personFound == null)
            {
                return NotFound();
            }

            ShoppingList shoppingList = new ShoppingList();
            shoppingList.Name = shoppingListDTOin.ListName;
            shoppingList.Delivered = false;
            shoppingList.Shop = shoppingListDTOin.ShopId;
            shoppingList.Owner = personFound.PersonId;

            _context.ShoppingList.Add(shoppingList);
            _context.SaveChanges();

            shoppingListDTOin.Items.ForEach(item => 
            {
                ShoppingListItem shoppingListItem = new ShoppingListItem();
                shoppingListItem.ShoppingList = shoppingList.ShoppingListId;
                shoppingListItem.Item = item.Item;
                shoppingListItem.Quantity = item.Quantity;
                _context.ShoppingListItem.Add(shoppingListItem);
                _context.SaveChanges();
            });
            
            return Created($"api/Shop/{shoppingList.ShoppingListId}", shoppingList);
        }
        
        [HttpGet]
        [Route("myHelp")]
        public IActionResult MyHelps()
        {
            string person = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (person == null)
            {
                return NotFound();
            }

            var shoppingLists = from i in _context.ShoppingList
                                where i.DelivererNavigation.Email == person
                                && i.Delivered == false
                                select new ShoppingListDTOout()
                                {
                                    Name = i.Name,
                                    Delivered = i.Delivered,
                                    Shop = i.ShopNavigation.Name,
                                    Owner = i.OwnerNavigation.FirstName,
                                    DelivererName = i.DelivererNavigation.FirstName
                                };
            return Ok(shoppingLists);
        }
        

        [HttpPut]
        [Route("delivered/{id}")]
        public async Task<IActionResult> Delivered(int id)
        {
            string email = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            if (email == null)
            {
                return NotFound();
            }

            ShoppingList shoppingList = await _context.ShoppingList.FindAsync(id);
            
            if(shoppingList == null)
            {
                return NotFound();
            }

            if(shoppingList.DelivererNavigation.Email != email || shoppingList.OwnerNavigation.Email != email)
            {
                return Unauthorized();
            }

            shoppingList.Delivered = true;

            _context.Attach(shoppingList);
            _context.Entry(shoppingList).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
