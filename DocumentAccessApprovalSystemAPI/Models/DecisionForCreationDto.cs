using DocumentAccessApprovalSystemAPI.Enums;

namespace DocumentAccessApprovalSystemAPI.Models
{
    public class DecisionForCreationDto
    {
        public DecisionStatus Status { get; set; } // Approved or Rejected
        public string Comment { get; set; }
    }
}
