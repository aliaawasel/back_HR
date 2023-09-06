using HR_System.DTOs.UserDto;

namespace HR_System.Repositories.User
{
    public interface IUserRepository
    {
        List<UserDto> GetAll();
        int Insert(RegisterDto NewUserDto);
        UserDto GetById(string id);
        void Update(UserDto UpdateUSer);
        void Delete(string id);
        int checkMail(string mail);

        int ifFound(string username);
    }
}
