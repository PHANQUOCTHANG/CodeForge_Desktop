using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Payment
    {
        public Guid PaymentID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public Guid CourseID { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string PaymentMethod { get; set; }
        public string Status { get; set; } = "pending";
        public DateTime? PaidAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string OrderId { get; set; }
        public string PaymentGateway { get; set; }
        public string TransactionId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}