using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentCourseEnrollment.DTOs;
using StudentCourseEnrollment.Interfaces;

namespace StudentCourseEnrollment.Controller
{
    [ApiController]
    [Route("api/[controller]")] // This makes the URL: api/enrollment
    //[controller] is replaced by the class name minus “Controller” → EnrollmentController → api/enrollment.
    public class EnrollmentController : ControllerBase
    {
        //DI injection
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service) 
        {
            _service = service;
        }

        // GET: api/enrollment
        [HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        var data = await _service.GetEnrollmentReportAsync();
        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Set a breakpoint on the line below!
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        public async Task<ActionResult<IEnumerable<EnrollmentReportDto>>> GetEnrollmentReportAsync()
        {
            var report = await _service.GetEnrollmentReportAsync();
            return Ok(report);
        }


        // POST: api/enrollment
        [HttpPost]
        public async Task<IActionResult> Enroll(int studentId, int courseId)
        {
            var success = await _service.EnrollStudentAsync(studentId, courseId);
            if (!success) return BadRequest("Enrollment failed.");

            return Ok("Successfully enrolled!");
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll([FromBody] EnrollmentRequestDto request)
        {
            var result = await _service.EnrollStudent(request);
            return Ok(result);
        }

    }
}
