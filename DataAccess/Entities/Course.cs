using System;


namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Course
    {
        // Khóa chính (tương ứng cột CourseID)
        public Guid CourseId { get; set; } = Guid.NewGuid();

        // Thông tin cơ bản
        public string Title { get; set; }
        public string Slug { get; set; }               // unique slug để routing / SEO
        public string Description { get; set; }
        public string Overview { get; set; }

        // Phân loại & Cấp độ
        public string Level { get; set; }              // beginner, intermediate, advanced
        public string Language { get; set; }           // C#, Java, Python...
        public Guid CategoryId { get; set; }           // FK -> CourseCategories
        public Guid CreatedBy { get; set; }            // FK -> Users (tác giả / người tạo)

        // Thông tin thương mại / metadata
        public decimal Price { get; set; } = 0m;
        public decimal Discount { get; set; } = 0m;    // phần trăm hoặc đơn vị tùy thiết kế
        public string Thumbnail { get; set; }          // đường dẫn file hoặc URL

        // Thống kê / rating
        public double Rating { get; set; } = 0.0;
        public int TotalRatings { get; set; } = 0;
        public int TotalStudents { get; set; } = 0;
        public int LessonCount { get; set; } = 0;

        // Thời lượng (schema dùng INT) - đơn vị do bạn quyết định (phút/giờ/giây)
        public int Duration { get; set; } = 0;

        // Trạng thái & audit
        public string Status { get; set; } = "active"; // draft, active, ...
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Xóa mềm
        public bool IsDeleted { get; set; } = false;

        // Constructor mặc định
        public Course()
        {
            // CourseId và CreatedAt đã được khởi tạo ở khai báo thuộc tính
        }
    }
}