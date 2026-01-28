using System;

namespace CodeForge_Desktop.Business.DTOs
{
    public class PaymentInfoDto
    {
        public string OrderId { get; set; }
        public string QrPayload { get; set; }     // EMV/VietQR payload
        public string PaymentUrl { get; set; }    // optional redirect URL
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "VND";
    }
}