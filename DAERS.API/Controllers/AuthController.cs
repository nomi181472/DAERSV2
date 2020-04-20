using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAERS.API.Data;
using DAERS.API.Dtos;
using DAERS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DAERS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository Repo;
        private readonly IConfiguration Config;

        public AuthController(IAuthRepository _repo,IConfiguration config)
        {
            Repo = _repo;
            Config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto RegDto)
        {
            // validation
            RegDto.Username=RegDto.Username.ToLower();
            if(await Repo.UserExists(RegDto.Username))
            return BadRequest("Username already exist");
            var userToCreate=new User
            {
                UserName=RegDto.Username
            };
            var createdUser=await Repo.Register(userToCreate,RegDto.Password);
            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto LogDto)
        {
            throw new Exception("Computer says no!");
            var userFromRepo=await Repo.Login(LogDto.Username.ToLower(),LogDto.Password);
            if(userFromRepo==null)
            return Unauthorized();
            var claims=new[]{
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName)

            };
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.GetSection("AppSettings:Token").Value));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
            var tokenDescriptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };
            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new{
                token=tokenHandler.WriteToken(token)
            });
        }
    }
}