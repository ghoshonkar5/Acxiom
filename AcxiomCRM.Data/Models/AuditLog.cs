using System;

namespace AcxiomCRM.Data.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty; 
        public string Module { get; set; } = string.Empty; 
        public string RecordId { get; set; } = string.Empty;
        public string Result { get; set; } = "Success"; 
        public string IpAddress { get; set; } = string.Empty;
        public string? Diff { get; set; } 
    }
}
