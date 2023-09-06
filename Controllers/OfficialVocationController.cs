using HR_System.DTOs.OfficialvocationDto;
using HR_System.Repositories.Official_Vocations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficialVocationController : ControllerBase
    {
        private readonly IOfficialVocationsRepository VocationsRepo;
        public OfficialVocationController(IOfficialVocationsRepository VocationsRepository)
        {
            this.VocationsRepo = VocationsRepository;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var vocations = VocationsRepo.GetAll();
            if (vocations != null)
            {
                return Ok(vocations);
            }
            else if (vocations == null)
            {
                return NotFound();
            }
                return BadRequest();
            
        }
        [HttpGet("ById/{id}")]
        public IActionResult GetbyId(int id)
        {
            var vocation = VocationsRepo.GetVocationById(id);
            if (vocation != null)
            {
                return Ok(vocation);
            }
            else if (vocation == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Insert")]
        public IActionResult Insert(OfficialVocationDto vocationDto)
        {
            VocationsRepo.Insert(vocationDto);

            if (ModelState.IsValid == true)
            {
                
                    return Ok();
                
                
                
            }
                return BadRequest();
            

        }
        [HttpPut("Update")]
        public IActionResult Update(OfficialVocationDto vocationDto)
        {
           VocationsRepo.Update(vocationDto);
            if (ModelState.IsValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                VocationsRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExistName")]
        public IActionResult ExistName (int id,string name)
        {

            var res =VocationsRepo.ifNameExist(id, name);
           
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
        [HttpGet("ExistDate")]
        public IActionResult ExistDate(int id,string date)
        {
            var Date = Convert.ToDateTime(date);

            var res = VocationsRepo.ifDateExist(id,Date);
             if (res == 0)
            {
                var sentence = new { Message = "DateExist" };
                return Ok(sentence);
            }
            else
            {
                return Ok();
            }
        }

    }
}
