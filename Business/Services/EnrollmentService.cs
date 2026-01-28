using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.Business.DTOs;
using System;

namespace CodeForge_Desktop.Business.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IProgressRepository _progressRepository;
        private readonly PaymentRepository _paymentRepository;

        // Constructor Injection
        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IProgressRepository progressRepository)
        {
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _progressRepository = progressRepository ?? throw new ArgumentNullException(nameof(progressRepository));
            _paymentRepository = new PaymentRepository(); // simple composition; replace with DI if available
        }

        public bool IsUserEnrolled(Guid userId, Guid courseId)
        {
            // Gọi Repository để check
            return _enrollmentRepository.IsUserEnrolled(userId, courseId);
        }

        public bool EnrollUserToCourse(Guid userId, Guid courseId)
        {
            // 1. Kiểm tra đã đăng ký chưa
            if (_enrollmentRepository.IsUserEnrolled(userId, courseId))
            {
                return true; // Đã đăng ký rồi coi như thành công
            }

            try
            {
                // 2. Tạo Enrollment mới
                var enrollment = new Enrollment
                {
                    EnrollmentID = Guid.NewGuid(),
                    UserID = userId,
                    CourseID = courseId,
                    EnrolledAt = DateTime.UtcNow,
                    Status = "enrolled" // Mặc định là đã tham gia
                };

                int result = _enrollmentRepository.Add(enrollment);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Log error here if needed
                Console.WriteLine("Enroll Error: " + ex.Message);
                return false;
            }
        }

        public int GetEnrolledStudentCount(Guid courseId)
        {
            return _enrollmentRepository.GetEnrolledStudentCount(courseId);
        }

        // ----------------------
        // Payment / Paid flow
        // ----------------------

        /// <summary>
        /// Create a Payment record for the given user/course and return PaymentInfo
        /// (orderId + VietQR payload). The client can display QR to complete payment.
        /// </summary>
        public PaymentInfoDto InitiatePaymentForCourse(Guid userId, Guid courseId, decimal amount, string currency = "VND")
        {
            // Create order id using timestamp + random short guid
            var orderId = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}";

            var payment = new Payment
            {
                PaymentID = Guid.NewGuid(),
                UserID = userId,
                CourseID = courseId,
                Amount = amount,
                Currency = currency,
                PaymentMethod = "VietQR",
                Status = "pending",
                CreatedAt = DateTime.UtcNow,
                OrderId = orderId,
                PaymentGateway = "VietQR",
                IsDeleted = false
            };

            // Persist
            _paymentRepository.Create(payment);

            // Build VietQR payload (simple example). For production build proper EMVCo payload or use bank API.
            var vietQrPayload = $"vietqr://transfer?order={orderId}&amount={amount}&currency={currency}&note=Course:{courseId}";

            return new PaymentInfoDto
            {
                OrderId = orderId,
                QrPayload = vietQrPayload,
                PaymentUrl = null,
                Amount = amount,
                Currency = currency
            };
        }

        /// <summary>
        /// Confirm payment by orderId (called from webhook / manual confirm).
        /// Marks payment as paid, records transactionId and enrolls user.
        /// Returns true if processed (idempotent).
        /// </summary>
        public bool ConfirmPaymentAndEnroll(string orderId, string transactionId = null)
        {
            if (string.IsNullOrWhiteSpace(orderId)) return false;

            // Find payment
            var payment = _paymentRepository.GetByOrderId(orderId);
            if (payment == null) return false;

            // If already paid, do nothing (idempotent)
            if (string.Equals(payment.Status, "paid", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(payment.Status, "succeeded", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            try
            {
                // Mark paid
                _paymentRepository.MarkPaid(payment.PaymentID, transactionId ?? Guid.NewGuid().ToString());

                // Create enrollment server-side (so client cannot fake)
                var enrolled = EnrollUserToCourse(payment.UserID, payment.CourseID);

                return enrolled;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ConfirmPaymentAndEnroll error: " + ex.Message);
                return false;
            }
        }
    }
}