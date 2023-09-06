namespace HR_System.DTOs.EmployeeDto
{
    public class EmployeeViewDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Phone { get; set; }
        public string NationalID { get; set; }
        public DateTime BirthDate { get; set; }

        public double Salary { get; set; }
        public DateTime HireDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public String DepartmentName { get; set; }
    }
}
