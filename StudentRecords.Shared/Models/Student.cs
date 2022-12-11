using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecords.Shared.Models
{

    public class Student
    {
        public int? StudentId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? KnownAs { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? Gender { get; set; }
        [Required]
        public string? UniversityEmail { get; set; }

        public string? NetworkId { get; set; }
        [Required]
        public string? HomeOrOverseas { get; set; }
        public List<CourseEnrolment> CourseEnrolment { get; set; } = new ();
    }


}
