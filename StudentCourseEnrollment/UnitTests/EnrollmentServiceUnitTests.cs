using Moq;
using StudentCourseEnrollment.DataAccess;
using StudentCourseEnrollment.DTOs;
using StudentCourseEnrollment.Interfaces;
using Xunit;

namespace StudentCourseEnrollment.UnitTests
{
    public class EnrollmentServiceUnitTests
    {
        private readonly Mock<IEnrollmentRepository> _mockEnrollmentRepository;
        private readonly EnrollmentService _mockEnrollmentService;

        public EnrollmentServiceUnitTests()
        {
            //1. Create Fake/Mock Repository call
            _mockEnrollmentRepository = new Mock<IEnrollmentRepository>();

            //2. Make Fake call to service

            _mockEnrollmentService = new EnrollmentService(_mockEnrollmentRepository.Object);
        }

            [Fact] // This tells the "Robot" this is a test
            public async Task EnrollStudent_ShouldReturnSuccess_WhenDataIsValid()
            {
                // ARRANGE: Prepare the data
                var request = new EnrollmentRequestDto { StudentId = 1, CourseId = 101 };

            // Tell the fake repo: "If someone calls EnrollStudentAsync with any IDs, return 'true'"
            _mockEnrollmentRepository.Setup(repo => repo.EnrollStudentAsync(It.IsAny<int>(), It.IsAny<int>()))
                         .ReturnsAsync(true);

                // ACT: Run the actual code
                var result = await _mockEnrollmentService.EnrollStudent(request);

                // ASSERT: Check if the result is what we expected
                Assert.Equal("Student Enrolled successfully.", result);
            }
        [Fact]
        public async Task EnrollStudent_ShouldThrowException_WhenDatabaseFails()
        {
            // ARRANGE
            var request = new EnrollmentRequestDto { StudentId = 1, CourseId = 999 };

            // Tell the fake repo to return 'false' (simulating a DB failure)
            _mockEnrollmentRepository.Setup(repo => repo.EnrollStudentAsync(It.IsAny<int>(), It.IsAny<int>()))
                     .ReturnsAsync(false);

            // ACT & ASSERT: We expect the service to throw an exception
            var exception = await Assert.ThrowsAsync<Exception>(() => _mockEnrollmentService.EnrollStudent(request));

            // Check that the error message is what you wrote in the Service
            Assert.Equal("Database failed to save enrollment.", exception.Message);
        }

    }
}
