using HR_System.Models;

namespace HR_System.DTOs.GroupDto
{
    public class getAllGroupsDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<string> pagesName { get; set; }


    }
}
