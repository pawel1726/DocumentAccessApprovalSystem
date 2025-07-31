using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentAccessApprovalSystemAPI.Entities
{
    public class Decision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }

        public AccessRequest AccessRequest { get; set; }

    }
}
