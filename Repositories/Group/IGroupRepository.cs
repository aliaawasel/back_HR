using HR_System.DTOs.GroupDto;

namespace HR_System.Repositories.Group
{
    public interface IGroupRepository
    {
        //void Add(InsertGroup groupDto, permissionsDto permissionDto);
        List<getAllGroupsDto> GetAllGroups();

        void AddGroup(GroupDto groupDto);
        void Addpermission(permissionsDto permissionDto);
        void UpdateGroup(GroupDto groupDto);

        void DeleteGroup(int id);




    }
}
