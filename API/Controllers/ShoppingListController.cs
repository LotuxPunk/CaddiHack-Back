﻿using System;
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
                                where i.OwnerNavigation.Email == person
                                select new ShoppingList();

            return Ok(shoppingLists);
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
                                select new ShoppingList();

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

            _context.Attach(shoppingList);
            _context.Entry(shoppingList).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
