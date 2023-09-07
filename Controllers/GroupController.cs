using HR_System.DTOs.GroupDto;
using HR_System.Repositories.Group;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        public GroupController(IGroupRepository groupRepo)
        {
            this._groupRepository=groupRepo;
        }




        [HttpGet("allGroups")]
        public IActionResult GetAll()
        {
           var groups= _groupRepository.GetAllGroups();
            if (groups == null)
            {
                return NotFound();
            }
            else if (groups!= null) {
            return Ok(groups);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost("Insert/group")]
        public IActionResult Insert(GroupDto group)
        {
           _groupRepository.AddGroup(group);
            if(ModelState.IsValid)
            {
                return Ok();
            }
            else { return BadRequest(); }
           

        }


        [HttpPut("Update/group")]
        public IActionResult Update(GroupDto group)
        {
            _groupRepository.UpdateGroup(group);
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else { return BadRequest(); }
        }

        [HttpDelete("Delete/group/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _groupRepository.DeleteGroup(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Insert/pages")]
        public IActionResult InsertPages(permissionsDto permission)
        {
            _groupRepository.Addpermission(permission);
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else { return BadRequest(); }


        }
    }
}
