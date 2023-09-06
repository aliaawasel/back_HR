namespace HR_System.DTOs.AttendanceDto
{
    public class AttendanceInsertDto
    {
        public int Id { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public DateTime Date { get; set; }
        public int EMPId { get; set; }
    }
}
