namespace DocumentAccessApprovalSystemAPI.Models
{
    public class AccessRequestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DocumentId { get; set; }
        public string AccessType { get; set; } 
        public int DecisionId { get; set; }
        public string Status { get; set; } 
      
    }
}
