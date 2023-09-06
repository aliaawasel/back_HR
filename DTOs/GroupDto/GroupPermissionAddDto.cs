using HR_System.Models;

namespace HR_System.DTOs.GroupDto
{
    public class GroupPermissionAddDto
    {
        
            public int GroupID { get; set; }
            public int PermissionID { get; set; }
            public bool Add { get; set; }
            public bool delete { get; set; }
            public bool update { get; set; }


    }
}
