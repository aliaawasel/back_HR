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
        [HttpPost("Insert")]
        public IActionResult Insert(GroupDto group)
        {
           _groupRepository.AddGroup(group);
            if(ModelState.IsValid)
            {
                return Ok();
            }
            else { return BadRequest(); }
           

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
