using HR_System.DTOs.SalaryReportDto;

namespace HR_System.Repositories.Salary
{
    public interface ISalaryReportRepository
    {
        List<SalaryReportDto> getAll(int month , int year);
        List<SalaryReportDto> GetByName(string name, int month, int year);

    }
}
