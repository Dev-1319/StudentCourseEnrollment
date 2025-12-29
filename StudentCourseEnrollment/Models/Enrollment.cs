using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourseEnrollment.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        public int CourseId {  get; set; }
        public DateTime EnrollmentDate { get; set; }

        //Navigation properties
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
