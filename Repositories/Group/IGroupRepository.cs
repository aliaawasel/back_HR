using HR_System.DTOs.GroupDto;

namespace HR_System.Repositories.Group
{
    public interface IGroupRepository
    {
        //void Add(InsertGroup groupDto, permissionsDto permissionDto);
        void AddGroup(GroupDto groupDto);
        void Addpermission(permissionsDto permissionDto);

    }
}
