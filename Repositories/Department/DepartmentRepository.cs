using HR_System.DTOs.DepartmentDto;
using HR_System.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HR_System.Repositories.Department
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HREntity hREntity;
        public DepartmentRepository(HREntity hREntity) => this.hREntity = hREntity;

        public Models.Department GetById(int id)
        {
            return hREntity.Departments.FirstOrDefault(D => D.Id == id && D.IsDeleted == false);

        }
        public List<DepartmentDto>? GetAll()
        {
            var Departments= hREntity.Departments.Where(D => D.IsDeleted == false).ToList();
            var DeptsDto = Departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
            }).ToList();
            return (DeptsDto);
        }
        public DepartmentDto? GetDeptById(int id)
        {
            var Dept=GetById(id);
            var DeptDto = new DepartmentDto();
            DeptDto.Id = Dept.Id;
            DeptDto.Name = Dept.Name;
            return(DeptDto);
        }
        public void Delete(int id)
        {
            try
            {
                var Department = GetById(id);
                if (Department != null)
                {
                    Department.IsDeleted = true;
                    hREntity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Insert(DepartmentDto NewDeptDto)
        {
            var Dept = new Models.Department
            {
                Name = NewDeptDto.Name
            };
            hREntity.Add(Dept);
            hREntity.SaveChanges();
        }

        public void Update(DepartmentDto UpdateDeptDto)
        {
            var Dept = new Models.Department
            {
                Id = UpdateDeptDto.Id,
                Name = UpdateDeptDto.Name
            };
            hREntity.Update(Dept);
            hREntity.SaveChanges();
        
       }
        public int ifDeptExist(int id, string Name)
        {
            var all = GetAll();

            foreach (var o in all)
            {
                if (o.Name == Name && o.Id != id)
                {

                    return 0;
                }
            }
            return 1;

        }
    }
    }

