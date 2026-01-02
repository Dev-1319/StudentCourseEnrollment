namespace StudentCourseEnrollment.DTOs
{
    public class EnrollmentReportDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
