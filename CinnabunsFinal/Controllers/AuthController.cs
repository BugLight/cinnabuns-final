using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CinnabunsFinal.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public AuthController(IConfiguration configuration, UserManager<User> userManager,
                              RoleManager<IdentityRole<int>> roleManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult<AuthResponse>> SignIn([FromBody] SignIn signIn)
        {
            var user = await userManager.FindByNameAsync(signIn.UserName);
            if (user == null)
                return Unauthorized();
            if (!await userManager.CheckPasswordAsync(user, signIn.Password))
                return Unauthorized();
            return await GenerateTokenAsync(user);
        }

        private async Task<AuthResponse> GenerateTokenAsync(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Role, roles[0])
            };
            var signingKey = configuration["SECRETKEY"];
            var token = new JwtSecurityToken(
                issuer: "cinnabuns",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                    SecurityAlgorithms.HmacSha256));

            return new AuthResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
