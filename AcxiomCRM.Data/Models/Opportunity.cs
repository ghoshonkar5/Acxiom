using System;

namespace AcxiomCRM.Data.Models
{
    public class Opportunity
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string OpportunityName { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string Stage { get; set; } = "Qualification"; 
        public decimal Amount { get; set; }
        public int Probability { get; set; }
        public DateTime ExpectedCloseDate { get; set; }
        public string OwnerId { get; set; } = string.Empty;
        public ApplicationUser? Owner { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
