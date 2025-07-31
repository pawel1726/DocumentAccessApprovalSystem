using DocumentAccessApprovalSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAccessApprovalSystemAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApprovalSystemRepository _approvalSystemRepository;

        public UsersController(IApprovalSystemRepository approvalSystemRepository)
        {
            _approvalSystemRepository = approvalSystemRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entities.User>>> GetCities()
        {
            var usersEntites = await _approvalSystemRepository.GetUsersAsync();
            
            return Ok(usersEntites);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.User>> GetUser(int id) 
        {
            var userToReturn = await _approvalSystemRepository.GetUserAsync(id);
            
            if (userToReturn == null)
            {
                return NotFound();
            }

            
            return Ok(userToReturn);
            
            


        }
        
    }
}
