using HR_System.DTOs.EmployeeDto;
using HR_System.Models;
using HR_System.Repositories.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Authorize(Policy = "المستخدمين")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository EmpRepo) => this._employeeRepository = EmpRepo;

        [HttpGet("all")]
        public IActionResult GetAll()
        {
           var employees=_employeeRepository.GetAll();
            if (employees != null)
            {
                return Ok(employees);
            }
           else if (employees == null)
            {
                return NotFound();
            }
            return BadRequest();
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetByID(int id) { 
        var employee=_employeeRepository.GetEmpById(id);
            if (employee != null)
            {
                return Ok(employee);
            }
           else if (employee == null)
            {
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPost("Insert")]
        public IActionResult Insert(EmployeeInsertDto Emp)
        {
            _employeeRepository.Insert(Emp);
            if(ModelState.IsValid==true)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("Update")]
        public IActionResult Update(EmployeeInsertDto Employee) { 
            _employeeRepository.Update(Employee);
            if(ModelState.IsValid==true)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{EmpId}")]
        public IActionResult Delete(int EmpId) {
            try
            {
                _employeeRepository.Delete(EmpId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByDepartment/{DeptID}")]
        public IActionResult GetByDeptID(int DeptID)
        {
            var employees = _employeeRepository.GetByDeptID(DeptID);
            if (employees != null)
            {
                return Ok(employees);
            }
            else if (employees == null)
            {
                return NotFound();
            }
            return BadRequest();
        }


    }
}
