using StudentCourseEnrollment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentCourseEnrollment.DTOs;

namespace StudentCourseEnrollment.Interfaces
{
    public interface IEnrollmentService
    {
        Task<bool> EnrollStudentAsync(int studentId, int courseId);

        //Task<List<Enrollment>> GetEnrollmentReportAsync();

        Task<IEnumerable<EnrollmentReportDto>> GetEnrollmentReportAsync();

    }
}
