using HR_System.DTOs.AttendanceDto;
using OfficeOpenXml.ConditionalFormatting;

namespace HR_System.Repositories.Attendance
{
    public interface IAttendenceRepository
    {
        Models.Attendance GetByID(int id);
        List<AttendenceDto> GetAll();
        int Insert(AttendanceInsertDto NewAttendanceDto);
        void Update(AttendanceInsertDto UpdateAttendanceDto);
        void Delete(int id);
        void SaveExcelData(List<AttendanceInsertDto> excelDataList);
        AttendanceByIdDto GetAttendanceById(int id);

        List<AttendenceDto> GetByName(string name);
        List<AttendenceDto> GetByDate(DateTime date1, DateTime date2);
        int AttendanceDays(int month, int year, int id);
        int absenceDays(int month, int year, int id);
        int CalcAddsHours(int month, int year, int id);
        int CalcSubHours(int month, int year, int id);








    }

}
