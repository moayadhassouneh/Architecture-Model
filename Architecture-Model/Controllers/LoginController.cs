using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Architecture_Model.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [HttpGet]             
         public ActionResult<IEnumerable<string>> Getcustom()
        {
            var re = Request;
            var headers = re.Headers;
            string token = headers.Where(p => p.Key == "token").FirstOrDefault().Value;
            bool isvalid = ValidateCurrentToken(token);
            return new string[] { "value1", "value2", "value3", "value4", "value5" };
        }

        [HttpGet]
        [Authorize]       
        public ActionResult<IEnumerable<string>> Get()
        {
            UserModel user =JsonConvert.DeserializeObject<UserModel>( ((ClaimsIdentity) User.Identity).Claims.Where(p => p.Type == ClaimTypes.SerialNumber).FirstOrDefault().Value);
            return new string[] { "value1", "value2", "value3", "value4", "value5" };
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Surname, userInfo.Username),
                new Claim(ClaimTypes.Email, userInfo.EmailAddress),
                new Claim(ClaimTypes.SerialNumber, JsonConvert.SerializeObject(userInfo)),
                new Claim("DateOfJoing", DateTime.Now.AddDays(-300).ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private bool ValidateCurrentToken(string token)
        {
            var mySecret = _config["Jwt:Key"];
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            var myIssuer = _config["Jwt:Issuer"];           
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = myIssuer,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        } 

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            //Validate the User Credentials  
            //Demo Purpose, I have Passed HardCoded User Information  
            if (login.Username == "moayad")
            {
                user = new UserModel { Username = "moayad Trivedi", EmailAddress = "test.btest@gmail.com" };
            }
            return user;
        }

    }


    public class UserModel
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
    }
}