using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DAL;
using DTO;
using Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ApiController
    {
        private readonly ILogger<LocalityController> _logger;
        private readonly CaddieHackContext _context;


        public ShopController(ILogger<LocalityController> logger, CaddieHackContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ShopDTOout), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            string email = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            var shops = from i in _context.Shop
                        select new ShopDTOout()
                        {
                            ShopId = i.ShopId,
                            Name = i.Name,
                            Address = i.Address,
                            PicturePath = i.PicturePath,
                            Locality = i.Locality,
                            //IsFavorite = _context.Favorite.Any(person, shop);
                       };
            return Ok(shops);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Shop), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> Post([FromForm] ShopDTOin shop)
        {
            var stream = shop.Picture.OpenReadStream();

            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("drtn3myw4", "662324151252959", "nX0XPARZfpO_WRuESu_3cPoidrA");

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new CloudinaryDotNet.FileDescription(shop.Picture.Name, stream)
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            Shop newShop = new Shop();
            newShop.Name = shop.Name;
            newShop.Address = shop.Address;
            newShop.PicturePath = uploadResult.SecureUri.ToString();
            newShop.Locality = shop.Locality;

            _context.Shop.Add(newShop);
            await _context.SaveChangesAsync();
            return Created($"api/Shop/{newShop.ShopId}", newShop);
        }

    }
}