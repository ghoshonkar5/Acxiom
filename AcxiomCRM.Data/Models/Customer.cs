using System;

namespace AcxiomCRM.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
        public string OwnerId { get; set; } = string.Empty;
        public ApplicationUser? Owner { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
