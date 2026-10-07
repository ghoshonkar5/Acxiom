using System;

namespace AcxiomCRM.Data.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string LeadName { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty; 
        public string Status { get; set; } = "New"; 
        public string OwnerId { get; set; } = string.Empty;
        public ApplicationUser? Owner { get; set; }
        public decimal Value { get; set; }
        public DateTime? ConversionDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
