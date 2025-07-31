using DocumentAccessApprovalSystemAPI.Enums;
using System.Data;

namespace DocumentAccessApprovalSystemAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public ICollection<AccessRequest> AccessRequests { get; set; } = new List<AccessRequest>();
    }
}
