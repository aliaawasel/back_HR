using System.ComponentModel;

namespace HR_System.DTOs.AttendanceDto
{
    public class AttendenceDto
    {
        public int Id { get; set; }

        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public DateTime Date { get; set; }

        public String DepartmentName { get; set; }

        public String EmployeeName { get; set; }

    }
}
