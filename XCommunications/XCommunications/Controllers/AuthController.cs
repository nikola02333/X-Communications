using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using XCommunications.Business.Interfaces;
using XCommunications.Business.Models;
using XCommunications.Business.Models.Data;
using XCommunications.WebAPI.Models;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IMapper mapper;
        private IService<WorkerServiceModel> service;
        private ILog log;
        //
        private UserManager<ApplicationUser> _userMenager;
        public AuthController(UserManager<ApplicationUser> userMenager)
        {
            this._userMenager = userMenager;
        }
        //
        public AuthController(IService<WorkerServiceModel> service, IMapper mapper, ILog log)
        {
            this.service = service;
            this.mapper = mapper;
            this.log = log;
        }

        [HttpPost]
        [Route("login")]
        //WorkerControllerModel
        public async Task<IActionResult> Login([FromBody] WorkerControllerModel model)
        {
           // var user = await _userMenager.FindByNameAsync(model.Username);
            //
           WorkerControllerModel worker = mapper.Map<WorkerControllerModel>(service.Get(model.Id));
          //if(worker.Id == )
         /*   if (user != null && await _userMenager.CheckPasswordAsync(user, model.Password))
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
            }*/


            return NoContent();
        }
    }
}