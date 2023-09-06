using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public DateTime Date { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public int EMPId { get; set; }
        [ForeignKey("EMPId")]
        public virtual Employee Employee { get; set; }
        

    }
}
