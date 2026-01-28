using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class PaymentRepository
    {
        public void Create(Payment p)
        {
            DbContext.Execute(@"
INSERT INTO Payments (PaymentID, [UserID], [CourseID], [Amount], [Currency], [PaymentMethod], [Status], [CreatedAt], [OrderId], [PaymentGateway], [TransactionId], [IsDeleted])
VALUES (@PaymentID, @UserID, @CourseID, @Amount, @Currency, @PaymentMethod, @Status, @CreatedAt, @OrderId, @PaymentGateway, @TransactionId, @IsDeleted)",
                new SqlParameter("@PaymentID", p.PaymentID),
                new SqlParameter("@UserID", p.UserID),
                new SqlParameter("@CourseID", p.CourseID),
                new SqlParameter("@Amount", p.Amount),
                new SqlParameter("@Currency", p.Currency),
                new SqlParameter("@PaymentMethod", (object)p.PaymentMethod ?? DBNull.Value),
                new SqlParameter("@Status", p.Status),
                new SqlParameter("@CreatedAt", p.CreatedAt),
                new SqlParameter("@OrderId", (object)p.OrderId ?? DBNull.Value),
                new SqlParameter("@PaymentGateway", (object)p.PaymentGateway ?? DBNull.Value),
                new SqlParameter("@TransactionId", (object)p.TransactionId ?? DBNull.Value),
                new SqlParameter("@IsDeleted", p.IsDeleted ? 1 : 0)
            );
        }

        public Payment GetByPaymentId(Guid paymentId)
        {
            var dt = DbContext.Query("SELECT TOP 1 * FROM Payments WHERE PaymentID = @P AND IsDeleted = 0",
                new SqlParameter("@P", paymentId));
            if (dt == null || dt.Rows.Count == 0) return null;
            var r = dt.Rows[0];
            return MapRowToPayment(r);
        }

        public Payment GetByOrderId(string orderId)
        {
            var dt = DbContext.Query("SELECT TOP 1 * FROM Payments WHERE OrderId = @O AND IsDeleted = 0",
                new SqlParameter("@O", orderId));
            if (dt == null || dt.Rows.Count == 0) return null;
            return MapRowToPayment(dt.Rows[0]);
        }

        public void MarkPaid(Guid paymentId, string transactionId)
        {
            DbContext.Execute(@"
UPDATE Payments
SET [Status] = 'paid', [TransactionId] = @T, [PaidAt] = @Now
WHERE PaymentID = @P",
                new SqlParameter("@T", (object)transactionId ?? DBNull.Value),
                new SqlParameter("@Now", DateTime.UtcNow),
                new SqlParameter("@P", paymentId)
            );
        }

        private Payment MapRowToPayment(DataRow r)
        {
            return new Payment
            {
                PaymentID = r["PaymentID"] != DBNull.Value ? (Guid)r["PaymentID"] : Guid.Empty,
                UserID = r["UserID"] != DBNull.Value ? (Guid)r["UserID"] : Guid.Empty,
                CourseID = r["CourseID"] != DBNull.Value ? (Guid)r["CourseID"] : Guid.Empty,
                Amount = r["Amount"] != DBNull.Value ? Convert.ToDecimal(r["Amount"]) : 0m,
                Currency = r.Table.Columns.Contains("Currency") && r["Currency"] != DBNull.Value ? r["Currency"].ToString() : "VND",
                PaymentMethod = r.Table.Columns.Contains("PaymentMethod") && r["PaymentMethod"] != DBNull.Value ? r["PaymentMethod"].ToString() : null,
                Status = r.Table.Columns.Contains("Status") && r["Status"] != DBNull.Value ? r["Status"].ToString() : "pending",
                CreatedAt = r.Table.Columns.Contains("CreatedAt") && r["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(r["CreatedAt"]) : DateTime.UtcNow,
                PaidAt = r.Table.Columns.Contains("PaidAt") && r["PaidAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r["PaidAt"]) : null,
                OrderId = r.Table.Columns.Contains("OrderId") && r["OrderId"] != DBNull.Value ? r["OrderId"].ToString() : null,
                PaymentGateway = r.Table.Columns.Contains("PaymentGateway") && r["PaymentGateway"] != DBNull.Value ? r["PaymentGateway"].ToString() : null,
                TransactionId = r.Table.Columns.Contains("TransactionId") && r["TransactionId"] != DBNull.Value ? r["TransactionId"].ToString() : null,
                IsDeleted = r.Table.Columns.Contains("IsDeleted") && r["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(r["IsDeleted"]) : false
            };
        }
    }
}