using HR_System.DTOs.DepartmentDto;
using HR_System.Models;
using HR_System.Repositories.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _Deptrepository;
        public DepartmentController(IDepartmentRepository DeptRepo) => this._Deptrepository = DeptRepo;

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var Depts = _Deptrepository.GetAll();
            if (Depts != null)
            {
                return Ok(Depts);
            }
            else if (Depts == null)
            {
                return NotFound();
            }
            return BadRequest();
        }


        [HttpGet("{DeptID}")]
        public IActionResult GetByID(int DeptID)
        {
            var dept = _Deptrepository.GetDeptById(DeptID);
            if (dept != null)
            {
                return Ok(dept);
            }
            else if (dept == null)
            {
                return NotFound();
            }
            return BadRequest();
        }


        [HttpPost("Insert")]
        public IActionResult Insert(DepartmentDto Dept)
        {
            _Deptrepository.Insert(Dept);

            if (ModelState.IsValid == true)
            {
                
                    return Ok();
                
            }
            return BadRequest();
        }
        [HttpPut("Update")]
        public IActionResult Update(DepartmentDto Dept)
        {
            _Deptrepository.Update(Dept);
            if (ModelState.IsValid == true)
            {
                
                    return Ok();
                
             

            }
            return BadRequest();
        }

        [HttpDelete("Delete/{DeptID}")]
        public IActionResult Delete(int DeptID)
        {
            try
            {
                _Deptrepository.Delete(DeptID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("DeptExist")]
        public IActionResult ExistDept(int id, string name)
        {

            var res = _Deptrepository.ifDeptExist(id, name);

            if (res == 0)
            {
                var sentence = new { Message = "NameExist" };
                return Ok(sentence);
            }
            else
            {
                return Ok();
            }
        }

    }
}
