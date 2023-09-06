using HR_System.DTOs.DepartmentDto;

namespace HR_System.Repositories.Department
{
    public interface IDepartmentRepository
    {
        Models.Department GetById(int id);

        List<DepartmentDto>? GetAll();
        DepartmentDto? GetDeptById(int id);
        void Insert(DepartmentDto NewDepartment);
        void Update(DepartmentDto UpdateDepartment);
        void Delete(int id);
        int ifDeptExist(int id, string Name);

    }
}
