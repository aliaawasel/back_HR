using HR_System.Repositories.Salary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryReportController : ControllerBase
    {
        private readonly ISalaryReportRepository salaryRepo;
        
        public SalaryReportController(ISalaryReportRepository salaryRepo)
        {
            this.salaryRepo = salaryRepo;
        }

        [HttpGet("{month},{year}")]
        public IActionResult Get(int month, int year)
        {
            var all = salaryRepo.getAll(month, year);
            if (all != null)
            {
                return Ok(all);
            }
            else if (all == null)
            {
                return NotFound();
            }
            return BadRequest();

        }
        [HttpGet("ByName/{month},{year}/{name}")]
        public IActionResult GetByname(int month, int year ,string name)
        {
            var all = salaryRepo.GetByName(name,month ,year);
            if (all != null)
            {
                return Ok(all);
            }
            else if (all == null)
            {
                return NotFound();
            }
            return BadRequest();

        }
    }
}
