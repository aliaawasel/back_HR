
using HR_System.CustomAuthorization;
using HR_System.Models;
using HR_System.Repositories.Attendance;
using HR_System.Repositories.Department;
using HR_System.Repositories.Employee;
using HR_System.Repositories.GeneralSettings;
using HR_System.Repositories.Group;
using HR_System.Repositories.Official_Vocations;
using HR_System.Repositories.Salary;
using HR_System.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Office.Interop.Excel;
using System.Security.Claims;
using System.Text;

namespace HR_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<HREntity>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultString")));
            

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IGeneralSettingsRepository, GeneralSettingsRepository>();
            builder.Services.AddScoped<IAttendenceRepository, AttendenceRepository>();
            builder.Services.AddScoped<IOfficialVocationsRepository, OfficialVocationsRepository>();
            builder.Services.AddScoped<ISalaryReportRepository, SalaryReportRepository>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();


            builder.Services.AddAuthentication(Options => Options.DefaultAuthenticateScheme = "myschema").AddJwtBearer("myschema",
            Options =>
            {
                string key = builder.Configuration.GetValue<string>("Secret");
                var securitykey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = securitykey,
                    ValidateIssuer = false,
                    ValidateAudience = false,

                };
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequireUppercase = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequiredLength = 8;
                Options.User.RequireUniqueEmail = true;
                //Options.Lockout.MaxFailedAccessAttempts = 5;
                //Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            })
            .AddEntityFrameworkStores<HREntity>();

            builder.Services.AddAuthentication(Options => { 
                Options.DefaultAuthenticateScheme = "default";
                Options.DefaultChallengeScheme = "default";
                });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowingAPIClientsAccess", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("users", policy =>
                {
                    policy.Requirements.Add(new PermissionRequirement("المستخدمين"));
                });
                options.AddPolicy("Employees", policy =>
                {
                    policy.Requirements.Add(new PermissionRequirement("الموظفيين"));
                });
                options.AddPolicy("groups", policy =>
                {
                    policy.Requirements.Add(new PermissionRequirement("المجموعات"));
                });
                options.AddPolicy("vactions", policy =>
                {
                    policy.Requirements.Add(new PermissionRequirement("الاجازات الرسميه"));
                });
            });

            //builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            //builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowingAPIClientsAccess");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}