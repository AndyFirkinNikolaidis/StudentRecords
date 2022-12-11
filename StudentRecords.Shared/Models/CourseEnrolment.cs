using System.ComponentModel.DataAnnotations;

namespace StudentRecords.Shared.Models
{
    public class CourseEnrolment
    {
        public string? EnrolmentId { get; set; }
        [Required]
        public string? AcademicYear { get; set; }
        [Required]
        public int YearOfStudy { get; set; }
        [Required]
        public string? Occurrence { get; set; }
        [Required]
        public string? ModeOfAttendance { get; set; }
        [Required]
        public string? EnrolmentStatus { get; set; }
        [Required]
        public DateTime CourseEntryDate { get; set; }
        [Required]
        public DateTime ExpectedEndDate { get; set; }
        public Course Course { get; set; } = new Course();
    }


}
