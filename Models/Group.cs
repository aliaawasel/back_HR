using System.ComponentModel;

namespace HR_System.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        //public virtual ICollection<Permissions> permissions { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<GroupPermissions> GroupPermissions { get; set; }


    }
}
