using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {

        private readonly CaddieHackContext _context;
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtController(CaddieHackContext context, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _context = context;
            _jwtOptions = jwtOptions.Value;
        }
        
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel loginModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(loginModel.Password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                loginModel.Password = builder.ToString();
            }

            Person personFound = 
                _context.Person.FirstOrDefault(person => person.Email == loginModel.Email && person.Password == loginModel.Password);

            if(personFound == null)
            {
                return Unauthorized();
            }


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, personFound.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                           ClaimValueTypes.Integer64),
                new Claim("idUser", personFound.PersonId.ToString())

            };

            claims.Add(new Claim("role", personFound.Role));
            claims.Add(new Claim("name", personFound.Name));
            claims.Add(new Claim("firstName", personFound.Name));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            var reponse = new Token()
            {
                Access_Token = encodedJwt,
                Expires_In = (int)_jwtOptions.ValidFor.TotalSeconds // TODO : Aller remettre 5 minutes dans le ValidFor des jwtOptions
            };

            return Ok(reponse);
            
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}