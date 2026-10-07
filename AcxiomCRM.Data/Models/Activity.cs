using System;

namespace AcxiomCRM.Data.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; 
        public string Status { get; set; } = "Completed"; 
        public string OwnerId { get; set; } = string.Empty;
        public ApplicationUser? Owner { get; set; }
        public string RelatedPerson { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
