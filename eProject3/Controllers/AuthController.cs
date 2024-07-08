using eProject3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext db;
        private readonly IConfiguration configuration;

        public AuthController(DatabaseContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserLogin acc)
        {
            var accCheck = await Authentication(acc);
            if (accCheck == null)
            {
                return NotFound("User not found");

            }
            else if (accCheck.LOGIN_PASSWORD != acc.LOGIN_PASSWORD)
            {
                return Unauthorized(new { message = "Incorrect password" });
            }
            else if (accCheck.status == "inactive")
            {
                return Unauthorized(new { message = "Account is inactive" });
            }

            else if(accCheck.status == "locked")
            {
                return Unauthorized(new { message = "Account is locked" });
            }

            var token = GenerateToken(accCheck);
            return Ok(new { token });
        }

        private string GenerateToken(User accCheck)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Username", accCheck.LOGIN_NAME),
                new Claim("Password", accCheck.LOGIN_PASSWORD),
                new Claim(ClaimTypes.Role, accCheck.role_id.ToString()),
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
            claims, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [NonAction]
        private async Task<User> Authentication(UserLogin accLogin)
        {
            var acc = await db.Users.SingleOrDefaultAsync(a=> a.LOGIN_NAME == accLogin.LOGIN_NAME && a.LOGIN_PASSWORD==accLogin.LOGIN_PASSWORD);
            return acc;
        }
    }
}
