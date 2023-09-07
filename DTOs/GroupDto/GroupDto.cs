using HR_System.Models;

namespace HR_System.DTOs.GroupDto
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<int> pages { get; set; }


    }
}
