using AutoMapper;
using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CinnabunsFinal.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly AppContext context;
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        private const string RoleClaim = "role";

        public AuthController(AppContext context, IConfiguration configuration, UserManager<User> userManager,
                              RoleManager<IdentityRole<int>> roleManager)
        {
            this.context = context;
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
                new Claim(RoleClaim, roles.FirstOrDefault() ?? "")
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

        private static readonly Random random = new Random();
        private string GeneratePassword()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                builder.Append(random.Next(0, 10));
            }
            return builder.ToString();
        }

        [HttpPost("register")]
        //[Authorize(Roles="admin")]
        public JsonResult Register([FromBody]UserResponse user, [FromQuery]string role, [FromQuery]string to)
        {
            if (!(new[] { "admin", "organizer", "volunteer" }).ToList().Contains(role))
                return new JsonResult(new { status = "failed" });

            var claims = new Claim[]
            {
                new Claim("user", JsonConvert.SerializeObject(user)),
                new Claim("role", role)
            };
            var signingKey = configuration["SECRETKEY"];
            var tokenData = new JwtSecurityToken(
                issuer: "cinnabuns-register",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(72),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                    SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

            var pass = GeneratePassword();
            var url = string.Format("http://best-hack.prod.kistriver.net/api/auth/signup/{0}?password={1}", 
                token, pass);

            MailMessage mail = new MailMessage(configuration["MAILFROM"], to);
            SmtpClient client = new SmtpClient();
            client.Port = Int32.Parse(configuration["MAILPORT"]);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = configuration["MAILHOST"];
            client.Timeout = 5000;
            client.Credentials = new System.Net.NetworkCredential(configuration["MAILFROM"], configuration["MAILPASS"]);
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.Subject = "Регистрация";
            mail.Body = string.Format("Ссылка для регистрации: <a href='{0}'>{0}</a>. ", url);
            mail.Body = string.Format("Ссылка для регистрации: <a href='{0}'>{0}</a>. Пароль: {1}", url, pass);
            mail.IsBodyHtml = true;
            client.Send(mail);

            return new JsonResult(new {
                token = url
            });
        }

        [HttpGet("signup/{token}")]
        public UserResponse SignUp(string token, [FromQuery][FromBody] string password)
        {
            if (password == null)
                return BadRequest();

            var jwt = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var claims = jwt.Claims;
            var role = claims.First(c => c.Type == "role").Value;
            var userData = claims.First(c => c.Type == "user").Value;

            var user = JsonConvert.DeserializeObject<User>(userData);
            AsyncHelper.RunSync(() => userManager.CreateAsync(user, password));

            return Mapper.Map<UserResponse>(user);
        }
    }
}
