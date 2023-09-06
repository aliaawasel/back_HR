using HR_System.DTOs.ApplicationUserDto;
using HR_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;


        public ApplicationUserController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        //public async Task<IActionResult> LogIn(LoginDto LogInUser)
        //{
        //if(ModelState.IsValid)
        //{
        //    ApplicationUser user=await userManager.FindByNameAsync(LogInUser.Username);
        //    if(user != null)
        //    {
        //     bool found= await userManager.CheckPasswordAsync(user,LogInUser.Password);
        //        if(found)
        //        {
        //            var Claims =new List<Claim>();
        //            Claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        //            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
        //            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
        //            SigningCredentials signincred = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        //            //create token
        //            JwtSecurityToken mytoken = new JwtSecurityToken(issuer: configuration["JWT:ValidIssuer"], audience: configuration["JWT:ValidAudience"], claims:Claims,expires:DateTime.Now.AddHours(1),signingCredentials:signincred);
        //            return Ok(new {
        //                token =new JwtSecurityTokenHandler().WriteToken(mytoken), expiration = mytoken.ValidTo });
        //        }
        //        return Unauthorized();
        //    }
        //    return Unauthorized();
        //}
        //return Unauthorized();

        //}

        [HttpPost("StaticlogIn")]

        public IActionResult staticlogin(LoginDto userlogin) {

            if (userlogin.Username != "Aliaa" && userlogin.Password != "12345")
            {
                return Unauthorized();
            }

            #region define claims
            List<Claim> userdata = new List<Claim>();
            userdata.Add(new Claim(ClaimTypes.NameIdentifier, userlogin.Username));
            userdata.Add(new Claim(ClaimTypes.Email, $"{userlogin.Username}@gmail.com"));
            #endregion

            #region secrete key
            string key = configuration.GetValue<string>("Secret");
            var securitykey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            #endregion

            #region createtoken
            var singcer = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            // token object
            var token = new JwtSecurityToken(claims: userdata, signingCredentials: singcer, expires: DateTime.Now.AddDays(1), issuer: "Admin", audience: "weather");

            //object to string

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            #endregion
            return Ok(stringToken);

        }
        [HttpPost("logIn")]
        public async Task<IActionResult> login(LoginDto loginDto )
        {
            var user= userManager.FindByNameAsync(loginDto.Username);
            if (user == null) {
                return NotFound();
            }
            return Ok();
        }


    } }
