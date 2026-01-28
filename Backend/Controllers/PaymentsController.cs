//using Microsoft.AspNetCore.Mvc;
//using System;
//using CodeForge_Desktop.Business.Services;
//using CodeForge_Desktop.DataAccess.Repositories;
//using CodeForge_Desktop.Business.DTOs;

//namespace CodeForge_Desktop.Backend.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class PaymentsController : ControllerBase
//    {
//        private readonly EnrollmentService _enrollmentService;
//        private readonly PaymentRepository _paymentRepository;

//        public PaymentsController()
//        {
//            // Simple construction without DI - replace with DI in real backend
//            var enrollRepo = new EnrollmentRepository();
//            var progressRepo = new ProgressRepository();
//            _enrollmentService = new EnrollmentService(enrollRepo, progressRepo);
//            _paymentRepository = new PaymentRepository();
//        }

//        // POST api/payments/create
//        // Body: { "userId": "...", "courseId": "...", "amount": 1299000.0 }
//        [HttpPost("create")]
//        public IActionResult CreatePayment([FromBody] CreatePaymentRequest req)
//        {
//            if (req == null || req.UserId == Guid.Empty || req.CourseId == Guid.Empty)
//                return BadRequest(new { error = "Invalid payload" });

//            // server-side: create pending Payment and return payment info
//            var info = _enrollmentService.InitiatePaymentForCourse(req.UserId, req.CourseId, req.Amount, req.Currency ?? "VND");

//            return Ok(new { data = new { paymentInfo = info } });
//        }

//        // GET api/payments/status/{orderId}
//        [HttpGet("status/{orderId}")]
//        public IActionResult GetStatus(string orderId)
//        {
//            if (string.IsNullOrWhiteSpace(orderId)) return BadRequest(new { error = "orderId required" });

//            var p = _paymentRepository.GetByOrderId(orderId);
//            if (p == null) return NotFound(new { error = "payment not found" });

//            return Ok(new { data = new { status = p.Status, paidAt = p.PaidAt, transactionId = p.TransactionId } });
//        }

//        // POST api/payments/{orderId}/confirm
//        // Body: { "transactionId":"...", "secret":"..." }
//        // This endpoint can be called by bank webhook (after verifying signature) or admin tool
//        [HttpPost("{orderId}/confirm")]
//        public IActionResult Confirm(string orderId, [FromBody] ConfirmRequest req)
//        {
//            if (string.IsNullOrWhiteSpace(orderId)) return BadRequest(new { error = "orderId required" });

//            // TODO: validate webhook signature / secret here before calling ConfirmPaymentAndEnroll
//            bool ok = _enrollmentService.ConfirmPaymentAndEnroll(orderId, req?.TransactionId);

//            if (!ok) return BadRequest(new { error = "Unable to confirm payment or enroll" });

//            return NoContent();
//        }

//        public class CreatePaymentRequest
//        {
//            public Guid UserId { get; set; }
//            public Guid CourseId { get; set; }
//            public decimal Amount { get; set; }
//            public string Currency { get; set; }
//        }

//        public class ConfirmRequest
//        {
//            public string TransactionId { get; set; }
//            public string Secret { get; set; }
//        }
//    }
//}