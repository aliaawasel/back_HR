namespace HR_System.DTOs.AttendanceDto
{
    public class AttendanceByIdDto
    {
        public int Id { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public string Date { get; set; }
        public int DeptId { get; set; }
        public int EmpId { get; set; }

    }
}
