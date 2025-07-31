using AutoMapper;
using DocumentAccessApprovalSystemAPI.Entities;
using DocumentAccessApprovalSystemAPI.Enums;
using DocumentAccessApprovalSystemAPI.Models;
using DocumentAccessApprovalSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAccessApprovalSystemAPI.Controllers
{
    [Route("api/users/{userId}/accessrequests")]
    [ApiController]
    public class AccessRequestsController : ControllerBase
    {
        private IApprovalSystemRepository _approvalSystemRepository;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        public AccessRequestsController(IApprovalSystemRepository approvalSystemRepository, IMapper mapper, IMailService MailService)
        {
            _approvalSystemRepository = approvalSystemRepository;
            _mapper = mapper;
            _mailService = MailService;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessRequestDto>>> GetAccessRequests(int userId)
        {
            if (!await _approvalSystemRepository.UserExistsAsync(userId))
            {
                return NotFound();
            }

            var accessRequestsEntites = await _approvalSystemRepository.GetAccessRequestsForUserAsync(userId);
            return Ok(_mapper.Map<IEnumerable<AccessRequestDto>>(accessRequestsEntites));
            

        }


        [HttpGet("{requestid}", Name = "GetAccessRequest")]
        public async Task<ActionResult<AccessRequestDto>> GetAccessRequest(int requestid)
        {
            
            var accessRequestEntity = await _approvalSystemRepository.GetAccessRequestAsync(requestid);
            return Ok(_mapper.Map<AccessRequestDto>(accessRequestEntity));
        }
        [HttpPost]
        public async Task<ActionResult<Entities.AccessRequest>> CreateAccessRequest(int userId, AccessRequestForCreationDto request)//complex type assumed FromBody
        {
            //if(ModelState.IsValid == false)
            //{
            //    return BadRequest(ModelState);
            //}

            if (!await _approvalSystemRepository.UserExistsAsync(userId))
            {
                return NotFound();
            }
            var finalRequest = _mapper.Map<Entities.AccessRequest>(request);
            await _approvalSystemRepository.AddAccessRequestAsync(userId, finalRequest);
            await _approvalSystemRepository.SaveChangesAsync();

            
            
            var createdRequestToReturn = _mapper.Map<Models.AccessRequestDto>(finalRequest);


            return CreatedAtRoute("GetAccessRequest", new
            {
                userId = userId,
                requestid = createdRequestToReturn.Id
            },
            createdRequestToReturn);    
        }
        
        
        
        [HttpPost("{requestId}/decision")]
        public async Task<IActionResult> MakeDecision(int userId, int requestId, [FromBody] DecisionForCreationDto decisionDto)
        {
            // Check if user exists and is approver
            var user = await _approvalSystemRepository.GetUserAsync(userId);
            if (user == null || user.Role != Role.Approver)
                return BadRequest();// change

            var accessRequest = await _approvalSystemRepository.GetAccessRequestAsync(requestId);
            if (accessRequest == null)
                return NotFound();

            // Create decision
            var decision = new Decision
            {
                Type = decisionDto.Status.ToString(),
                Comment = decisionDto.Comment,
                AccessRequest = accessRequest
            };

            accessRequest.Decision = decision;
            accessRequest.Status = decisionDto.Status;

            await _approvalSystemRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("{requestId}/approve")]
        public async Task<IActionResult> ApproveAccessRequest(int userId, int requestId, [FromBody] string comment)
        {
            var user = await _approvalSystemRepository.GetUserAsync(userId);
            if (user == null || user.Role != Role.Approver)
                return BadRequest();

            var success = await _approvalSystemRepository.SetAccessRequestDecisionAsync(
                requestId, DecisionStatus.Approved, comment, userId);

            if (!success)
                return NotFound();
            
            var accessRequest = await _approvalSystemRepository.GetAccessRequestAsync(requestId);
            var userRequestor = await _approvalSystemRepository.GetUserAsync(accessRequest.UserId);

            _mailService.Send(userRequestor.Email, "Access Granted", "Access to file was granted");
            return NoContent();
        }

        [HttpPost("{requestId}/reject")]
        public async Task<IActionResult> RejectAccessRequest(int userId, int requestId, [FromBody] string comment)
        {
            var user = await _approvalSystemRepository.GetUserAsync(userId);
            if (user == null || user.Role != Role.Approver)
                return BadRequest();

            var success = await _approvalSystemRepository.SetAccessRequestDecisionAsync(
                requestId, DecisionStatus.Rejected, comment, userId);

            if (!success)
                return NotFound();

            return NoContent();
        }
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<PendingAccessRequestForApproverDto>>> GetPendingAccessRequestsForApprover(int userId)
        {
            var user = await _approvalSystemRepository.GetUserAsync(userId);
            if (user == null || user.Role != Role.Approver)
                return BadRequest();//change

            var pendingRequests = await _approvalSystemRepository.GetPendingAccessRequestsAsync();
            //manual mapping
            var result = pendingRequests.Select(ar => new PendingAccessRequestForApproverDto
            {
                Id = ar.Id,
                Username = ar.User.Name,
                Filename = ar.Document.FileName,
                AccessType = ar.AccessType.ToString(),
                Status = ar.Status.ToString()
            });

            return Ok(result);
        }
    }
}
