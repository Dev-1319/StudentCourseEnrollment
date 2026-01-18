using Microsoft.EntityFrameworkCore;
using StudentCourseEnrollment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentCourseEnrollment.DTOs;

using StudentCourseEnrollment.Interfaces;

namespace StudentCourseEnrollment.DataAccess
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;

        // We ask for the Interface, not the concrete class!
        public EnrollmentService(IEnrollmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> EnrollStudentAsync(int studentId, int courseId)
        {
            // 1. Business Logic: Check if student exists or course is full (Future step)
            // 2. Create the data object
            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now
            };

            // 3. Use the repository to save
            await _repository.AddAsync(enrollment);
            return await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<EnrollmentReportDto>> GetEnrollmentReportAsync()
        {
           // throw new Exception("Test Crash!");

            var enrollments = await _repository.GetFullEnrollmentDetailsAsync();

            return enrollments.Select(a => new EnrollmentReportDto
            {
                StudentId = a.StudentId,
                StudentName = a.Student.Name,
                CourseName = a.Course.Title,
                EnrollmentDate = DateTime.Now
            }
            ).ToList();
            ;
            //return await _repository.GetFullEnrollmentDetailsAsync();
        }

        public async Task<string> EnrollStudent(EnrollmentRequestDto enrollmentRequest)
        {

            var success = await _repository.EnrollStudentAsync(enrollmentRequest.StudentId, enrollmentRequest.CourseId);
            if (!success) throw new Exception("Database failed to save enrollment.");
            
            return "Student Enrolled successfully.";

        }
    }
}