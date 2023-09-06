using HR_System.DTOs.AttendanceDto;
using HR_System.DTOs.DepartmentDto;
using HR_System.DTOs.GroupDto;
using HR_System.Models;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Repositories.Group
{
    public class GroupRepository : IGroupRepository
    {
        private readonly HREntity hREntity;
        public GroupRepository(HREntity hREntity)
        {
            this.hREntity = hREntity;
        }
        //    public GroupDto GetAll() {
        //        var Group=hREntity.GroupPermissions.Include(g=>g.Group).Include(g=>g.Permissions).ToList();
        //        var groupDto=Group.Where(g=>new GroupDto()
        //        {
        //            Id=g.Group.Id,
        //            GroupName=g.Group.Name,
        //            GeneralSettingsPage=g.Permissions.GeneralSettingsPage,
        //            UserPage=g.Permissions.UserPage,
        //            AttendancePage=g.Permissions.AttendancePage,
        //            EmployeePage=g.Permissions.EmployeePage,
        //            DepartmentPage=g.Permissions.DepartmentPage,
        //            SalaryReportPage=g.Permissions.SalaryReportPage,
        //            =g.Add

        //        }).ToList();
        //        return groupDto;
        //    }
        //public List<GroupDto>? GetAll()
        //{
        //    var groups = hREntity.GroupPermissions.Include(a=>a.Permissions).Include(a=>a.Groups).Where(a => a.IsDeleted != true).ToList();
        //    var groupDto = groups.Select(a => new GroupDto()
        //    {
        //        GroupName = a.Groups.Name,
        //        PageName= a.Permissions.pageName,
        //        Add=a.Add,
        //        Delete=a.Delete,
        //        Update=a.Update,

        //    }).ToList();
        //    return (groupDto);

        //}

        //public void Add(InsertGroup groupDto,permissionsDto permissionDto)
        //{
        //    var permission = new Models.Permissions
        //    {
        //        pageName = permissionDto.PageName
        //    };
        //    var group = new Models.Group
        //    {
        //        Name = groupDto.GroupName,
        //        GroupPermissions = groupDto.GroupPermissions.Select(gp => new GroupPermissions
        //        {
        //            GroupID = gp.GroupID,
        //            PermissionID = gp.PermissionID,
        //            Add = gp.Add,
        //            Update = gp.update,
        //            Delete = gp.delete,
        //        }).ToList()

        //    };
        //    hREntity.Permissions.Add(permission);
        //    hREntity.Groups.Add(group);
        //    hREntity.SaveChanges();
        //}
        public void AddGroup(GroupDto groupDto)
        {
            var group = new Models.Group
            {
                Name = groupDto.GroupName,
            };
           
            hREntity.Groups.Add(group);
            hREntity.SaveChanges();
            foreach (var item in groupDto.pages)
            {
                GroupPermissions groupPermissions = new GroupPermissions
                {
                    PermissionID = item,
                    GroupID = group.Id
                };
                hREntity.GroupPermissions.Add(groupPermissions);
            }
            hREntity.SaveChanges();

        }
        public void Addpermission(permissionsDto permissionDto)
        {
            var permission = new Models.Permissions
            {
                pageName = permissionDto.PageName,
            };
            hREntity.Permissions.Add(permission);
            hREntity.SaveChanges();
        }
    }
    }

