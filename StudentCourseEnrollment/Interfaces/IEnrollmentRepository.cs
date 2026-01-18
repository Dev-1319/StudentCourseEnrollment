using StudentCourseEnrollment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourseEnrollment.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetFullEnrollmentDetailsAsync();

        Task<Enrollment> GetByIdAsync(int id);

        Task AddAsync(Enrollment enrollment);

        Task UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(int id);

        Task<bool> SaveChangesAsync();

        public Task<bool> EnrollStudentAsync(int studentId, int couseId);
    }
}