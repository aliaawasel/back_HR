using HR_System.DTOs.AttendanceDto;
using HR_System.DTOs.SalaryReportDto;
using HR_System.Models;
using HR_System.Repositories.Attendance;
using HR_System.Repositories.Employee;
using HR_System.Repositories.GeneralSettings;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Repositories.Salary
{
    public class SalaryReportRepository:ISalaryReportRepository
    {
        private readonly HREntity hREntity;
        private readonly IAttendenceRepository attendenceRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IGeneralSettingsRepository generalSettingsRepository;
        public SalaryReportRepository(HREntity hREntity , IAttendenceRepository attendenceRepository, IEmployeeRepository employeeRepository, IGeneralSettingsRepository settingsRepository)
        {
            this.hREntity = hREntity;
            this.attendenceRepository = attendenceRepository;
            this.employeeRepository = employeeRepository;
            this.generalSettingsRepository = settingsRepository;
        }
        public List<SalaryReportDto> getAll(int month , int year )
        {
            var salaries = hREntity.Employees.Include(e => e.Department).Where(e => e.IsDeleted != true).ToList();
            var SalaryDto = salaries.Select(s => new SalaryReportDto
            {
                EmployeeName = s.Name,
                DepartmentName = s.Department.Name,
                NetSalary = s.Salary,
                AttendanceDays = attendenceRepository.AttendanceDays(month,year,s.ID),
                AbsenceDays=attendenceRepository.absenceDays(month,year,s.ID),
                Adds=attendenceRepository.CalcAddsHours(month,year,s.ID),
                Subs=attendenceRepository.CalcSubHours(month,year,s.ID),
                TotalAdds= CalCTotalAdds(month,year,s.ID),
                TotalSubs=CalCTotalSubs(month,year,s.ID),
                ActualSalary=(s.Salary+CalCTotalAdds(month, year,s.ID))-CalCTotalSubs(month,year,s.ID),
            }).ToList();
            return (SalaryDto);
        }
               
        public double CalCTotalAdds(int month, int year,int id)
        {
            int AddsHours = attendenceRepository.CalcAddsHours(month, year, id);
            double AddValue = generalSettingsRepository.GetById(1).Add_hours;
            double totalAdds = AddsHours * AddValue;
            return (totalAdds);
        }

        public double CalCTotalSubs(int month, int year, int id)
        {
            int SubHours = attendenceRepository.CalcSubHours(month, year, id);
            double subValue = generalSettingsRepository.GetById(1).Sub_hours;
            double totalsubs = SubHours * subValue;
            return (totalsubs);
        }

        public List<SalaryReportDto> GetByName(string name, int month,int year)
        {
            var Salaries =getAll(month,year) ;

            return Salaries.Where(e => e.EmployeeName.Contains(name)).ToList();
        }
    }
}
