using DocumentAccessApprovalSystemAPI.Enums;

namespace DocumentAccessApprovalSystemAPI.Models
{
    public class AccessRequestForCreationDto
    {
        public int UserId { get; set; }
        public int DocumentId { get; set; }
        public RequestAccessType AccessType { get; set; }
        //public int DecisionId { get; set; }
        public DecisionStatus Status { get; set; }
    }
}
