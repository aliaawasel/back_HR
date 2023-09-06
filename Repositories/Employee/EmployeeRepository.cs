using HR_System.DTOs.EmployeeDto;
using HR_System.Models;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Repositories.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HREntity hREntity;
        public EmployeeRepository(HREntity hREntity) => this.hREntity = hREntity;

        public Models.Employee GetbyId(int id)
        {
            var Employee = hREntity.Employees.Include(e => e.Department).FirstOrDefault(e => e.ID == id && e.IsDeleted != true);
            return (Employee);
        }
        public List<EmployeeViewDto> GetAll()
        {
            var Employees = hREntity.Employees.Include(e=>e.Department).Where(e => e.IsDeleted != true).ToList();
            var EMPDto = Employees.Select(e => new EmployeeViewDto
            {
                ID = e.ID,
                Name = e.Name,
                Address = e.Address,
                Gender = e.Gender,
                NationalID = e.NationalID,
                Nationality = e.Nationality,
                Phone = e.Phone,
                BirthDate = e.BirthDate,
                Salary = e.Salary,
                HireDate = e.HireDate,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                DepartmentName=e.Department.Name
            }).ToList();
            return (EMPDto);
        }
        public EmployeeDto GetEmpById(int id)
        {
            var Employee = GetbyId(id);
            var EmpDTO = new EmployeeDto();
            EmpDTO.ID = Employee.ID;
            EmpDTO.Name = Employee.Name;
            EmpDTO.DeptID = Employee.Department.Id;
            EmpDTO.StartTime = Employee.StartTime;
            EmpDTO.NationalID= Employee.NationalID;
            EmpDTO.Nationality = Employee.Nationality;
            EmpDTO.EndTime = Employee.EndTime;
            EmpDTO.Phone = Employee.Phone;
            EmpDTO.Salary = Employee.Salary;
            EmpDTO.BirthDate = Employee.BirthDate.ToString("yyyy-MM-dd");
            EmpDTO.HireDate = (Employee.HireDate).ToString("yyyy-MM-dd");
            EmpDTO.Address = Employee.Address;
            EmpDTO.Gender = Employee.Gender;

            return (EmpDTO);
        }
        public void Insert(EmployeeInsertDto NewEmpDto)
        {
            var NewEmp = new Models.Employee
            {
                Name = NewEmpDto.Name,
                Address = NewEmpDto.Address,
                Gender = NewEmpDto.Gender,
                Nationality = NewEmpDto.Nationality,
                Phone = NewEmpDto.Phone,
                BirthDate = NewEmpDto.BirthDate,
                NationalID = NewEmpDto.NationalID,
                Salary = NewEmpDto.Salary,
                HireDate = NewEmpDto.HireDate,
                StartTime = NewEmpDto.StartTime,
                EndTime = NewEmpDto.EndTime,
                DeptID = NewEmpDto.DeptID
            };
            hREntity.Add(NewEmp);
            hREntity.SaveChanges();
        }
        public void Update(EmployeeInsertDto UpdateEmpDto)
        {
            var NewEmp = new Models.Employee
            {
                ID = UpdateEmpDto.ID,
                Name = UpdateEmpDto.Name,
                Address = UpdateEmpDto.Address,
                Gender = UpdateEmpDto.Gender,
                Nationality = UpdateEmpDto.Nationality,
                Phone = UpdateEmpDto.Phone,
                BirthDate = UpdateEmpDto.BirthDate,
                NationalID = UpdateEmpDto.NationalID,
                Salary = UpdateEmpDto.Salary,
                HireDate = UpdateEmpDto.HireDate,
                StartTime = UpdateEmpDto.StartTime,
                EndTime = UpdateEmpDto.EndTime,
                DeptID = UpdateEmpDto.DeptID
            };
            hREntity.Update(NewEmp);
            hREntity.SaveChanges();
        }
        public void Delete(int id)
        {
            var emp = GetbyId(id);
            if (emp != null)
            {
                emp.IsDeleted = true;
                hREntity.SaveChanges(true);

            }
        }

        public List<EmployeeDto> GetByDeptID(int deptid)
        {
            var Employee = hREntity.Employees.Where(e => e.DeptID == deptid && e.IsDeleted != true).ToList();
            var EMPDto = Employee.Select(e => new EmployeeDto
            {
                ID = e.ID,
                Name = e.Name,
                Address = e.Address,
                Gender = e.Gender,
                NationalID = e.NationalID,
                Nationality = e.Nationality,
                Phone = e.Phone,
                BirthDate = e.BirthDate.ToString(),
                Salary = e.Salary,
                HireDate = e.HireDate.ToString(),
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                DeptID = e.DeptID,
            }).ToList();
            return (EMPDto);

        }
    }
}
