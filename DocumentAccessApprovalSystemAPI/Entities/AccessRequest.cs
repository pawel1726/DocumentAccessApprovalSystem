using DocumentAccessApprovalSystemAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DocumentAccessApprovalSystemAPI.Entities
{
    public class AccessRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
        
        public RequestAccessType AccessType { get; set; }

        [ForeignKey("DecisionId")] 
        public Decision? Decision { get; set; }
        public int? DecisionId { get; set; }
        public DecisionStatus Status { get; set; } = DecisionStatus.Pending; 
    }
}
