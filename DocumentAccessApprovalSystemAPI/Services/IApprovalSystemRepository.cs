using DocumentAccessApprovalSystemAPI.Entities;
using DocumentAccessApprovalSystemAPI.Enums;

namespace DocumentAccessApprovalSystemAPI.Services
{
    public interface IApprovalSystemRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(int userId);
        Task<bool> UserExistsAsync(int userId);


        Task<IEnumerable<AccessRequest>> GetAccessRequestsForUserAsync(int userId);
        Task<AccessRequest> GetAccessRequestForUserAsync(int userId, int requestId);
        Task<AccessRequest?> GetAccessRequestAsync(int requestId);
        Task AddAccessRequestAsync(int requestId, AccessRequest request);


        Task<bool> SetAccessRequestDecisionAsync(int requestId, DecisionStatus status, string comment, int approverUserId);//-------


        Task<IEnumerable<AccessRequest>> GetPendingAccessRequestsAsync();

        Task<bool> SaveChangesAsync();
    }
}
