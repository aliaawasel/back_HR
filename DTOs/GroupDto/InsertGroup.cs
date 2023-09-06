namespace HR_System.DTOs.GroupDto
{
    public class InsertGroup
    {
        public string GroupName { get; set; }
        public List<getAllGroupsDto> GroupPermissions { get; set; }

    }
}
