using HR_System.DTOs.AttendanceDto;
using HR_System.DTOs.EmployeeDto;
using HR_System.Models;
using HR_System.Repositories;
using HR_System.Repositories.Attendance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;



namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendenceRepository _attendenceRepository;
        public AttendanceController(IAttendenceRepository attendanceRepo) => this._attendenceRepository = attendanceRepo;

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var attendances = _attendenceRepository.GetAll();
            if (attendances != null)
            {
                return Ok(attendances);
            }
            else if (attendances == null)
            {
                return NotFound();
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var attendance = _attendenceRepository.GetAttendanceById(id);
            if (attendance != null)
            {
                return Ok(attendance);
            }
           else if (attendance == null)
            {
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPost("Insert")]
        public IActionResult Insert(AttendanceInsertDto Attendance)
        {
           var res= _attendenceRepository.Insert(Attendance);
            if (res == 1)
            {
                if (ModelState.IsValid == true)
                {
                    return Ok();
                }
                return BadRequest();
            }
            else
            {
                if (ModelState.IsValid == true)
                {
                    var sentence = new { Message = "AddedBefore" };

                    return Ok (sentence);
                }
                return BadRequest();
            }
        }
        [HttpPut("Update")]
        public IActionResult Update( AttendanceInsertDto Attendance)
        {
            _attendenceRepository.Update(Attendance);
            if (ModelState.IsValid == true) { return Ok(); }
            return BadRequest();
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _attendenceRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("Upload")]
        public IActionResult Upload(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Assuming the data is on the first sheet
                    var rowCount = worksheet.Dimension.Rows;

                    var excelDataList = new List<AttendanceInsertDto>();

                    for (int row = 1; row <= rowCount; row++) // Start from row 2 to skip the header row
                    {
                        var dto = new AttendanceInsertDto() 
                        {
                            EMPId =int.Parse( worksheet.Cells[row, 1].Value?.ToString()),
                            CheckIn = TimeSpan.Parse( worksheet.Cells[row, 2].Value?.ToString()),
                            CheckOut= TimeSpan.Parse(worksheet.Cells[row, 3].Value?.ToString()),
                            Date =DateTime.Parse( worksheet.Cells[row, 4].Value?.ToString())
                        };

                        excelDataList.Add(dto);
                    }

                    _attendenceRepository.SaveExcelData(excelDataList);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during file processing
                return BadRequest("An error occurred: " + ex.Message);
            }
        }
        [HttpGet ("name")]
        public IActionResult GetbyNAme(string name)
        {
            var emps=_attendenceRepository.GetByName (name);
            if(emps !=null)
            {
                return Ok(emps);
            }
           else if (emps == null)
            {
                return NotFound();
            }
            return BadRequest();
        }

        [HttpGet("date")]
        public IActionResult GetbyDate(string d1 ,string d2)
        {
            var date1 = Convert.ToDateTime(d1);
            var date2 = Convert.ToDateTime(d2);

            var emps = _attendenceRepository.GetByDate(date1,date2);
            if (emps != null)
            {
                return Ok(emps);
            }
           else if (emps == null)
            {
                return NotFound();
            }
            return BadRequest();
        }
        [HttpGet("{month},{year},{id}")]
        public IActionResult absenceDays (int month , int year,int id) { 
           var re= _attendenceRepository.absenceDays(month,year,id);
            return Ok(re);
        }


        [HttpGet("hours/{month},{year},{id}")]
        public IActionResult hours(int month, int year,int id)
        {
            var re = _attendenceRepository.CalcAddsHours(month, year,id);
            return Ok(re);
        }
    }
}
