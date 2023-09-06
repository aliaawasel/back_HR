namespace HR_System.DTOs.SalaryReportDto
{
    public class SalaryReportDto
    {
        public String EmployeeName { get; set; }
        public String DepartmentName { get; set; }

        public double NetSalary { get; set; }
        public int AttendanceDays { get; set; }
        public int AbsenceDays { get; set;}
        public int Adds { get; set; }
        public int Subs { get; set; }

        public double TotalAdds { get; set;}
        public double TotalSubs { get; set; }

        public double ActualSalary { get; set; }



    }
}
