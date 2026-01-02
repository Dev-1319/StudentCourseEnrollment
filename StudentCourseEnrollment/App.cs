using StudentCourseEnrollment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class App
{
    private readonly IEnrollmentService _enrollmentService;

    public App(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("--- Student Enrollment System ---");

        try
        {
            while (true)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("\n[1] Enroll Student");
                Console.WriteLine("\n[2] View all Enrollments");
                Console.WriteLine("\n[0] Exit");


                //Console.WriteLine("\nOptions: [1] Enroll Student [0] Exit");
                var choice = Console.ReadLine();

                if (choice == "0") break;

                if (choice == "1")
                {
                    Console.Write("Enter Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int sId)) continue;

                    Console.Write("Enter Course ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int cId)) continue;

                    Console.WriteLine("Processing...");
                    // This is where your actual program logic will live
                    bool success = await _enrollmentService.EnrollStudentAsync(sId, cId);
                    Console.WriteLine(success ? "Enrollment Successful!" : "Enrollment Failed.");
                }
                if (choice == "2")
                {
                    var report = await _enrollmentService.GetEnrollmentReportAsync();
                    Console.WriteLine("\n--- Current Enrollments ---");
                    Console.WriteLine($"{"ID",-5} | {"Student Name",-15} | {"Course Title",-20} | {"Date",-10}");
                    Console.WriteLine(new string('-', 60));

                    foreach (var enrollment in report)
                    {
                        Console.WriteLine($"{enrollment.StudentId,-5} | {enrollment.StudentName,-15} | {enrollment.CourseName,-20} | {enrollment.EnrollmentDate.ToShortDateString(),-10}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
