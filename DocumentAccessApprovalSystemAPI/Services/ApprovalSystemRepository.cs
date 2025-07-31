using DocumentAccessApprovalSystemAPI.DbContexts;
using DocumentAccessApprovalSystemAPI.Entities;
using DocumentAccessApprovalSystemAPI.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentAccessApprovalSystemAPI.Services
{
    public class ApprovalSystemRepository : IApprovalSystemRepository
    {
        private readonly MyDbContext _context;

        public ApprovalSystemRepository(MyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        


        
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        
        
        public async Task<User?> GetUserAsync(int id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.AccessRequests.AnyAsync(c => c.Id == userId);
        }

        public async Task<IEnumerable<AccessRequest>> GetAccessRequestsForUserAsync(int userId)
        {
            return await _context.AccessRequests.Where(c => c.UserId == userId).ToListAsync();
        }
        public async Task<AccessRequest> GetAccessRequestForUserAsync(int userId, int requestId)
        {
            return await _context.AccessRequests.Where(p => p.UserId == userId && p.Id == requestId).FirstOrDefaultAsync();
        }
        public async Task<AccessRequest?> GetAccessRequestAsync(int requestId)
        {
            return await _context.AccessRequests.Where(p => p.Id == requestId).FirstOrDefaultAsync();
        }

        public async Task AddAccessRequestAsync(int userId, AccessRequest request)
        {
            var user = await GetUserAsync(userId);
            user.AccessRequests.Add(request);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }




        public async Task<bool> SetAccessRequestDecisionAsync(int requestId, DecisionStatus status, string comment, int approverUserId)
        {
            var accessRequest = await _context.AccessRequests
                .Include(ar => ar.Decision)
                .FirstOrDefaultAsync(ar => ar.Id == requestId);

            if (accessRequest == null)
                return false;

            // Create new decision
            var decision = new Decision
            {
                Type = status.ToString(),
                Comment = comment,
                AccessRequest = accessRequest
            };

            _context.Decisions.Add(decision);

            accessRequest.Decision = decision;
            accessRequest.Status = status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AccessRequest>> GetPendingAccessRequestsAsync()
        {
            return await _context.AccessRequests
                .Where(ar => ar.Status == DecisionStatus.Pending)
                .Include(ar => ar.User)
                .Include(ar => ar.Document)
                .ToListAsync();
        }
    }
}
