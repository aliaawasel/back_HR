using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
      
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public int GroupID { get; set; }

        [ForeignKey("GroupID")]

        public virtual Group Group { get; set; }


    }
}
