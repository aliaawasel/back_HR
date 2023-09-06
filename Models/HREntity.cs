using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HR_System.Models
{
    public class HREntity :IdentityDbContext<ApplicationUser>
    {
        public HREntity()
        {

        }
        public HREntity(DbContextOptions options) : base(options)
        {

        }
        //public HREntity(DbContextOptions<HREntity> options) : base(options)
        //{

        //}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }
        public DbSet<GroupPermissions> GroupPermissions { get; set; }
        public DbSet<OfficialVocations> OfficialVocations { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneralSettings>()
                .HasData(new GeneralSettings { ID = 1, Add_hours = 100, Sub_hours = 100, vacation1 = "Friday", vacation2 = "Saturday" });

            modelBuilder.Entity<GroupPermissions>()
                .HasKey(ea => new { ea.GroupID, ea.PermissionID });

            //modelBuilder.Entity<Department>()
            //    .HasIndex(e => e.Name)
            //    .IsUnique();
            //modelBuilder.Entity<User>()
            //   .HasIndex(e => e.Username)
            //   .IsUnique();
            //modelBuilder.Entity<User>()
            //   .HasIndex(e => e.Email)
            //   .IsUnique();
            //modelBuilder.Entity<OfficialVocations>()
            //   .HasIndex(e => e.Name)
            //   .IsUnique();
            //modelBuilder.Entity<OfficialVocations>()
            //   .HasIndex(e => e.Date)
            //   .IsUnique();




         modelBuilder.Entity<Department>()
                .HasIndex(e => new { e.Name })
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
               .HasIndex(e => new { e.UserName,e.Email })
               .IsUnique()
               .HasFilter("[IsDeleted] = 0");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OfficialVocations>()
               .HasIndex(e => new { e.Name ,e.Date })
               .IsUnique()
               .HasFilter("[IsDeleted] = 0");

            base.OnModelCreating(modelBuilder);

        }
    }
}
