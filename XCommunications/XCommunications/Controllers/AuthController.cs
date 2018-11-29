using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using XCommunications.Business.Models;
using XCommunications.Business.Models.Data;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private UserManager<ApplicationUser> _userMenager;
        public AuthController(UserManager<ApplicationUser> userMenager)
        {
            this._userMenager = userMenager;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userMenager.FindByNameAsync(model.Username);

            if (user != null && await _userMenager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new[]
                {
                    new Claim (JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

                var token = new JwtSecurityToken(
                     issuer: "nikola",
                     audience: "XCommunication",
                expires: DateTime.UtcNow.AddHours(2),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }


            return NoContent();
        }
    }
}