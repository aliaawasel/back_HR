using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.Models
{
    public class GroupPermissions
    {
        public int GroupID { get; set; }
        public int PermissionID { get; set; }

        public virtual Group Groups { get; set; }

        public virtual Permissions Permissions { get; set; }


    }
}
