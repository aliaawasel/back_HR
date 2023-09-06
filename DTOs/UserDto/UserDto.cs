using System.ComponentModel.DataAnnotations;

namespace HR_System.DTOs.UserDto
{
    public class UserDto
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int GroupID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
