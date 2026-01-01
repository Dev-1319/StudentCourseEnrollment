using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StudentCourseEnrollment.DataAccess;
using StudentCourseEnrollment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourseEnrollment.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Move the DbContext setup here
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions=>
                                        sqlOptions.EnableRetryOnFailure(
                                        maxRetryCount: 5,
                                        maxRetryDelay: TimeSpan.FromSeconds(30),
                                        errorNumbersToAdd: null)
                    )
                );

            // Move your DI registrations here
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();

            return services;
        }
    }
}
