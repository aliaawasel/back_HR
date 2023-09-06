using HR_System.DTOs.ApplicationUserDto;
using HR_System.DTOs.EmployeeDto;
using HR_System.DTOs.UserDto;
using HR_System.Models;
using HR_System.Repositories.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> userManager;


        public UserController(IUserRepository UserRepo, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this._userRepository = UserRepo;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var Users = _userRepository.GetAll();

            return Users.IsNullOrEmpty() ? NotFound() : Ok(Users);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(string id)
        {
            var user = _userRepository.GetById(id);
            return user is null ? NotFound() : Ok(user);
        }


        [HttpPost("insert")]
        public IActionResult Insert(RegisterDto newUser)
        {
            var result = _userRepository.Insert(newUser);
            if (ModelState.IsValid == true)
            {
                if (result == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                };
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(UserDto updateUser)
        {
            if (ModelState.IsValid == true)
            {
                _userRepository.Update(updateUser);
                return Ok();
            }
            else { return BadRequest(); }
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
               _userRepository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("IFfound")]
        public IActionResult IFfound(string username)
        {
            var res = _userRepository.ifFound(username);
            if (res == 1)
            {
                return Ok();
            }
            else if (res == 0)
            {
                var sentence = new { Message = "Found" };
                return Ok(sentence);
            }
            else return BadRequest();

        }

        [HttpGet("FounMail")]

        public IActionResult FoundMail(string mail)
        {
            var res = _userRepository.checkMail(mail);
            if (res == 1)
            {
                return Ok();
            }
            else if (res == 0)
            {
                var sentence = new { Message = "Found" };
                return Ok(sentence);
            }
            else return BadRequest();
        }

        //[HttpPost]
        //[Route("Register")]
        //public async Task<ActionResult> Register(RegisterDto registerDto)
        //{
        //    ApplicationUser employee = new ApplicationUser
        //    {
        //        UserName = registerDto.Username,
        //        FullName = registerDto.FullName,
        //        Email = registerDto.Email,
        //        GroupID = registerDto.Group,
        //    };

        //    var result = await userManager.CreateAsync(employee, registerDto.Password);
        //    if (!result.Succeeded)
        //    {
        //        return BadRequest(result.Errors);
        //    }

        //    var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, employee.Id),
        //    new Claim(ClaimTypes.Name, employee.UserName),
        //    new Claim(ClaimTypes.Role, "Employee"),
        //};
        //    await userManager.AddClaimsAsync(employee, claims);

        //    return Ok();
        //}


        //[HttpPost("logIn")]
        //public async Task<IActionResult> LogIn(LoginDto LogInUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await userManager.FindByNameAsync(LogInUser.Username);
        //        if (user != null)
        //        {
        //            bool found = await userManager.CheckPasswordAsync(user, LogInUser.Password);
        //            if (found)
        //            {
        //                var Claims = new List<Claim>();
        //                Claims.Add(new Claim(ClaimTypes.Name, user.Username));
        //                Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //                SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
        //                SigningCredentials signincred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //                //create token
        //                JwtSecurityToken mytoken = new JwtSecurityToken(issuer: configuration["JWT:ValidIssuer"], audience: configuration["JWT:ValidAudience"], claims: Claims, expires: DateTime.Now.AddHours(1), signingCredentials: signincred);
        //                return Ok(new
        //                {
        //                    token = new JwtSecurityTokenHandler().WriteToken(mytoken),
        //                    expiration = mytoken.ValidTo
        //                });
        //            }
        //            return Unauthorized();
        //        }
        //        return Unauthorized();
        //    }
        //    return Unauthorized();
        //}
    }
}


