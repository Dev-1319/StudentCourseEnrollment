using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentCourseEnrollment.DataAccess;
using StudentCourseEnrollment.Extensions;
using StudentCourseEnrollment.Interfaces;


using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Use our extension method!
        services.AddProjectServices(context.Configuration);

        // Register the App itself
        services.AddTransient<App>();
    })
    .Build();

// Simply resolve 'App' and run it
var myApp = host.Services.GetRequiredService<App>();
await myApp.RunAsync();

/*///////////////////////////////

//var builder = WebApplication.CreateBuilder(args);

// 1. Create the Host
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // 2. Read Connection String from appsettings.json
        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

        // Add this check to see if it's actually reading the file
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Could not find the Connection String 'DefaultConnection'.");
        }

        // 3. Register DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // 4. Register Service (Dependency Injection)
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();

        // Register your Main Application Logic here as well
        // services.AddTransient<StartupService>(); 
    })
    .Build();

// 2. Execute Logic (The Action)
// We create a scope to safely resolve Scoped services like the DbContext
using (IServiceScope scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var enrollmentService = services.GetRequiredService<IEnrollmentService>();

        Console.WriteLine("--- Student Enrollment System Started ---");

        // TEST: Attempt to enroll Student 1 in Course 1
        // Note: Make sure these IDs exist in your SQL tables first!
        bool success = await enrollmentService.EnrollStudentAsync(1, 2);

        if (success)
            Console.WriteLine("Success: Student enrolled!");
        else
            Console.WriteLine("Notice: Enrollment skipped (Student might already be enrolled).");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database Error: {ex.Message}");
    }
}

// 3. Keep the app alive (The Background)
Console.WriteLine("Press any key to exit...");
await host.RunAsync();
*///////////