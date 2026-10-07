using System;

namespace AcxiomCRM.Data.Models
{
    public class FollowUp
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; 
        public string Status { get; set; } = "Planned"; 
        public string OwnerId { get; set; } = string.Empty;
        public ApplicationUser? Owner { get; set; }
        
        public int? LeadId { get; set; }
        public Lead? Lead { get; set; }
        
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        
        public int? OpportunityId { get; set; }
        public Opportunity? Opportunity { get; set; }
    }
}
