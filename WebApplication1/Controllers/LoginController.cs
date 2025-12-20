using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public ActionResult  Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) {
                return BadRequest("Please provide username and password");
            }

            LoginResponseDTO loginResponseDTO = new() { UserName = loginDTO.UserName };

            if (loginDTO.UserName == "uttam" && loginDTO.Password == "uttam@123")
            {
                var Key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                   {
                        new Claim(ClaimTypes.Name,loginDTO.UserName),
                        new Claim(ClaimTypes.Role,"Admin")
                   }),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                loginResponseDTO.token = tokenHandler.WriteToken(token);
            }
            else
            {
                return Ok("Invalid Username/password");
            }
            return Ok(loginResponseDTO);
        }
    }
    
}
