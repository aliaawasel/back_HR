using HR_System.DTOs.AttendanceDto;
using HR_System.DTOs.DepartmentDto;
using HR_System.Models;
using HR_System.Repositories.Employee;
using HR_System.Repositories.GeneralSettings;
using HR_System.Repositories.Official_Vocations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HR_System.Repositories.Attendance
{
    public class AttendenceRepository : IAttendenceRepository
    {
        private readonly HREntity hREntity;
        private readonly IOfficialVocationsRepository officialVocationsRepository;
        private readonly IGeneralSettingsRepository generalSettingsRepository;
        private readonly IEmployeeRepository employeeRepository;

        public AttendenceRepository(HREntity hREntity, IOfficialVocationsRepository vocationsRepository, IGeneralSettingsRepository settingsRepository , IEmployeeRepository employeeRepository)
        {
            this.hREntity = hREntity;
            this.officialVocationsRepository = vocationsRepository;
            this.generalSettingsRepository = settingsRepository;
            this.employeeRepository = employeeRepository;
        }

        public Models.Attendance GetByID(int id)
        {
            return hREntity.Attendances.Include(a => a.Employee).ThenInclude(e => e.Department).FirstOrDefault(a => a.Id == id && a.IsDeleted != true);
        }
        public List<AttendenceDto> GetAll()
        {
            var Attendances = hREntity.Attendances.Include(a => a.Employee).ThenInclude(e => e.Department).Where(a => a.IsDeleted != true).OrderBy(e=>e.Date);
            var AttDto = Attendances.Select(a => new AttendenceDto()
            {
                Id = a.Id,
                EmployeeName = a.Employee.Name,
                DepartmentName = a.Employee.Department.Name,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut,
                Date = a.Date,
            }).ToList();
            return (AttDto);

        }
        public AttendanceByIdDto GetAttendanceById(int id)
        {
            var Attendances = GetByID(id);
            var AttDto = new AttendanceByIdDto();
            AttDto.Id = id;
            AttDto.DeptId=Attendances.Employee.Department.Id;
            AttDto.CheckOut = Attendances.CheckOut;
            AttDto.CheckIn = Attendances.CheckIn;
            AttDto.Date = Attendances.Date.ToString("yyyy-MM-dd");
            AttDto.EmpId = Attendances.Employee.ID;
            return (AttDto);

        }
        public int Insert(AttendanceInsertDto NewAttendanceDto)
        {

            var Attendance = new Models.Attendance
            {
                Date = NewAttendanceDto.Date,
                CheckIn = NewAttendanceDto.CheckIn,
                CheckOut = NewAttendanceDto.CheckOut,
                EMPId = NewAttendanceDto.EMPId,

            };
            //if (officialVocationsRepository.ifVocation(Attendance.Date.Day,Attendance.Date.Month,Attendance.Date.Year) == 1)
            //{
            //    return 0;
            //}
            var all=GetAll();
            foreach(var item in all)
            {
                if(item.EmployeeName== (employeeRepository.GetbyId(Attendance.EMPId).Name) && (employeeRepository.GetbyId(Attendance.EMPId).IsDeleted)!=true && item.Date.Date==Attendance.Date.Date)
                {
                    return 0;
                }
            }

                hREntity.Add(Attendance);
                hREntity.SaveChanges();
            return 1;
            
        }
        public void Update(AttendanceInsertDto UpdateAttendanceDto)
        {
            var Attendance = new Models.Attendance
            {
                Id = UpdateAttendanceDto.Id,
                Date = UpdateAttendanceDto.Date,
                CheckIn = UpdateAttendanceDto.CheckIn,
                CheckOut = UpdateAttendanceDto.CheckOut,
                EMPId = UpdateAttendanceDto.EMPId,
            };
            hREntity.Update(Attendance);
            hREntity.SaveChanges();
        }
        public void Delete(int id)
        {
            var OldAttendance = GetByID(id);
            if (OldAttendance != null)
            {
                OldAttendance.IsDeleted = true;
                hREntity.SaveChanges();

            }
        }


        public void SaveExcelData(List<AttendanceInsertDto> excelDataList)
        {
            var entities = excelDataList.Select(dto => new Models.Attendance()
            {
                EMPId = dto.EMPId,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                Date = dto.Date
            }).ToList();

            hREntity.Attendances.AddRange(entities);
            hREntity.SaveChanges();
        }

        public List<AttendenceDto> GetByName(string name)
        {
            var Attendances = GetAll();

            return Attendances.Where(e => e.EmployeeName.Replace(" ","").Contains(name.Replace(" ", ""))).ToList();
        }

        public List<AttendenceDto> GetByDate(DateTime date1, DateTime date2)
        {
            var Attendances = GetAll();

            return Attendances.Where(e => e.Date >= date1 && e.Date <= date2).ToList();
        }

        //public List<EmployeesCheckInDto> GetCheckIns(int id)
        //{
        //    var Attendances = hREntity.Attendances.Include(a => a.Employee).Where(e => e.EMPId == id && e.IsDeleted != true).ToList();
        //    var ChekIns = Attendances.Select(a => new EmployeesCheckInDto()
        //    {
        //        CheckIn = a.CheckIn,
        //    }).ToList();
        //    return (ChekIns);

        //}


        //public List<EmployeesCheckInDto> GetCheckIns(int id, int month, int year)
        //{
        //    var Attendances = hREntity.Attendances.Include(a => a.Employee).Where(e => e.EMPId == id && e.Date.Month == month && e.Date.Year == year && e.IsDeleted != true).ToList();
        //    var ChekIns = Attendances.Select(a => new EmployeesCheckInDto()
        //    {
        //        CheckIn = a.CheckIn,
        //    }).ToList();

        //    return (ChekIns);
        //}
        public int NumberOfDays(int month, int year)
        {
            int days = 0;
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:

                    days = 31;
                    break;
                case 2:
                    if (year % 4 == 0)
                    {
                        days = 29;
                        break;
                    }
                    else
                    {
                        days = 28; break;
                    }
                case 4:
                case 6:
                case 9:
                case 11:
                    days = 30; break;

            }
            return days;
        }
        public int absenceDays(int month, int year, int id)
        {
            int absenceDays = 0;
            int DaysNum = NumberOfDays(month, year);
            for (var day = 1; day <= DaysNum; day++)
            {
                var Attendances = hREntity.Attendances.Include(a => a.Employee).Where(e => e.EMPId == id && e.Date.Month == month && e.Date.Year == year && e.Date.Day == day && e.IsDeleted != true).FirstOrDefault();
                if (Attendances == null)
                {
                    var res = officialVocationsRepository.ifVocation(day, month, year);
                    if (res == 0)
                    {

                        if (generalSettingsRepository.IFWeeklyVaction(day, month, year) == 0)
                        {
                            absenceDays++;
                        }
                    }

                }
            }
            return absenceDays;
        }
        public int AttendanceDays(int month, int year, int id)
        {
            int attendancesNum = 0;
            int DaysNum = NumberOfDays(month, year);
            for (var day = 1; day <= DaysNum; day++)
            {
                var Attendances = hREntity.Attendances.Include(a => a.Employee).Where(e => e.EMPId == id && e.Date.Month == month && e.Date.Year == year && e.Date.Day == day && e.IsDeleted != true).FirstOrDefault();
                if (Attendances != null)
                {
                    var res = officialVocationsRepository.ifVocation(day, month, year);
                    if (res == 0)
                    {
                        if (generalSettingsRepository.IFWeeklyVaction(day, month, year) == 0)
                        {
                            attendancesNum++;
                        }
                    }

                }
            }
            return attendancesNum;
        }


        //public int CalcAddsHours(int month, int year ,int id)
        //{
        //    var hours = 0;
        //    var Attendances = hREntity.Attendances.Include(a => a.Employee).Where(e => e.EMPId == id && e.Date.Month == month && e.Date.Year == year && e.IsDeleted != true).ToList();
        //    foreach( var x in Attendances)
        //    {
        //        var Daytime=x.Employee.EndTime.Subtract(x.Employee.StartTime);
        //        var Actualtime= x.CheckOut.Subtract(x.CheckIn);
        //        hours=Daytime.Hours-Actualtime.Hours;
        //    }
        //    return hours;
        //}

        public int CalcAddsHours(int month, int year ,int id)
        {
            int AddHours = 0;
            var Attendances = hREntity.Attendances.Include(a => a.Employee).Where(e => e.EMPId == id && e.Date.Month == month && e.Date.Year == year && e.IsDeleted != true).ToList();
            foreach (var x in Attendances)
            {
                if (generalSettingsRepository.IFWeeklyVaction(x.Date.Day, x.Date.Month, x.Date.Year)== 1){
                    var hours= x.CheckOut.Subtract(x.CheckIn);
                    AddHours +=hours.Hours;
                };
                if (x.CheckOut > x.Employee.EndTime)
                {
                    var overTime = x.CheckOut.Subtract(x.Employee.EndTime);
                    AddHours +=overTime.Hours;
                }
            }
            return AddHours;
        }
        public int CalcSubHours(int month, int year, int id)
        {
            var Emps=employeeRepository.GetbyId(id);
            int SubHours = 0;
            //var Absences = absenceDays(month, year, id);
            //SubHours=Absences* ((Emps.EndTime.Subtract(Emps.StartTime)).Hours);
            var Attendances = hREntity.Attendances.Include(a => a.Employee).Where(e => e.EMPId == id && e.Date.Month == month && e.Date.Year == year && e.IsDeleted != true).ToList();
            foreach (var x in Attendances)
            {
                if (x.CheckIn > x.Employee.StartTime)
                {
                    var attendanceLate = x.CheckIn.Subtract(x.Employee.StartTime);
                    SubHours+=attendanceLate.Hours;
                }
                if (x.CheckOut < x.Employee.EndTime)
                {
                    var departure = x.Employee.EndTime.Subtract(x.CheckOut);
                    SubHours+=departure.Hours;
                }
            }
            return SubHours;
        }


    }

    

}
