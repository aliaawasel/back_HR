using HR_System.DTOs.GeneralSettingDto;
using HR_System.Models;
using HR_System.Repositories.GeneralSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralSettingsController : ControllerBase
    {
        private readonly IGeneralSettingsRepository _generalSettingsRepository;
        public GeneralSettingsController(IGeneralSettingsRepository generalSettingsRepo)
        {
            this._generalSettingsRepository = generalSettingsRepo;
        }


        //[HttpGet("all")]
        //public IActionResult GetAll()
        //{
        //    var Settings= _generalSettingsRepository.GetAll();
        //    if (Settings != null)
        //    {
        //        return Ok(Settings);
        //    }
        //    return BadRequest();
        //}
        [HttpPut("Update")]
        public IActionResult Add(GeneralSettingDto setting)
        {
            _generalSettingsRepository.UpdateSetting(setting);
            if (ModelState.IsValid)
            {
                return Ok();
            }
                  return BadRequest();
        }


        //[HttpDelete("Delete/{id}")]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        _generalSettingsRepository.RemoveSetting(id);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("{id}")]
        public IActionResult GetSetting(int id) { 
            var setting=_generalSettingsRepository.GetById(id);
            if(setting != null)
            {
                return Ok(setting);
            }
            else if (setting == null)
            {
                return NotFound();
            }
            return BadRequest();
        }
        [HttpGet("{day},{month},{year}")]
        public IActionResult vaction(int day, int month , int year) {
            var re= _generalSettingsRepository.IFWeeklyVaction(day, month, year);
            return Ok(re);
        }
    }
}
