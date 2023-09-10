using HR_System.DTOs.ApplicationUserDto;
using HR_System.DTOs.UserDto;
using HR_System.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HR_System.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly HREntity hREntity;
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(HREntity hREntity, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.hREntity = hREntity;
        }

        public List<UserDto> GetAll()
        {
            var users = hREntity.ApplicationUsers.Where(u => !u.IsDeleted).ToList();
            var UserSDto = hREntity.ApplicationUsers.Select(u => new UserDto
            {
                ID = u.Id,
                Username = u.UserName,
                FullName = u.FullName,
                Password = u.PasswordHash,
                Email = u.Email,
                GroupID = u.GroupID,
                IsDeleted = u.IsDeleted,
                
            }).ToList();
            return (UserSDto);
        }

        public UserDto GetById(string id)
        {
            var user = hREntity.ApplicationUsers.FirstOrDefault(u => u.Id==id && u.IsDeleted != true);
            UserDto userDto = new UserDto();
            userDto.ID = id;
            userDto.Username = user.UserName;
            userDto.FullName = user.FullName;
            userDto.Password = user.PasswordHash;
            userDto.Email = user.Email;
            userDto.GroupID = user.GroupID;
            return (userDto);
        }
        public async Task<int> Insert(RegisterDto NewUserDto)
        {
            ApplicationUser newUser = new Models.ApplicationUser
            {
                UserName = NewUserDto.Username,
                FullName = NewUserDto.FullName,
                Email = NewUserDto.Email,
                GroupID = NewUserDto.Group
            };
            try
            {
                var result = await userManager.CreateAsync(newUser, NewUserDto.Password);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, newUser.Id),
                    new Claim(ClaimTypes.Email, newUser.Email),
                    new Claim(ClaimTypes.Name, newUser.FullName),
                    new Claim(ClaimTypes.Role,"User")
                };
                await userManager.AddClaimsAsync(newUser, claims);
                hREntity.ApplicationUsers.Add(newUser);
                hREntity.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public void Update(UserDto UpdateUserDto)
        {

            var newUser = new Models.ApplicationUser
            {
                Id = UpdateUserDto.ID,
                UserName = UpdateUserDto.Username,
                FullName = UpdateUserDto.FullName,
                PasswordHash = UpdateUserDto.Password,
                Email = UpdateUserDto.Email,
                GroupID = UpdateUserDto.GroupID
            };
            hREntity.Update(newUser);
            hREntity.SaveChanges();
        }
        public void Delete(string id)
        {
            var old = hREntity.Users.FirstOrDefault(u => u.Id == id && u.IsDeleted != true);
            if (old != null)
            {
                old.IsDeleted = true;
                hREntity.SaveChanges();
            }
        }
        public int ifFound(string username)
        {
            var all = GetAll();
            foreach (var user in all)
            {
                if (user.Username == username)
                {
                    return 0;
                }
            }
            return 1;

        }
        public int checkMail (string mail)
        {
            var all = GetAll();
            foreach (var user in all)
            {
                if (user.Email == mail)
                {
                    return 0;
                }
            }
            return 1;

        }

    }
}
