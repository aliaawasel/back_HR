using System.ComponentModel;

namespace HR_System.Models
{
    public class OfficialVocations
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

    }
}
