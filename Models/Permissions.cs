using System.ComponentModel.DataAnnotations;

namespace HR_System.Models
{
    public class Permissions
    {
        [Key]
        public int Id { get; set; }
        public string pageName { get; set; }
        public virtual ICollection<GroupPermissions> GroupPermissions { get; set; }






    }
}
