using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Infrastructure.Data;
using SchoolAttendance.Infrastructure.Interceptors;
using SchoolAttendance.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<SchoolAttendanceContext>(options =>
                    options.UseInMemoryDatabase("DemoDb"));
            }
            else
            {
                services.AddDbContext<SchoolAttendanceContext>(options =>
                {
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });
            }

            services.AddScoped<ISchoolAttendanceContext>(provider => provider.GetRequiredService<SchoolAttendanceContext>());

            services.AddScoped<ApplicationDbContextInitialiser>();


            services.AddTransient<IAssessmentService, AssessmentService>();
            services.AddTransient<IAttendanceService, AttendanceService>();
            services.AddTransient<IAzureBlobService, AzureBlobService>();
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<ICoreDataService, CoreDataService>();
            //services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IDropDownService, DropDownService>();
            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<ILessonDesignService, LessonDesignService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IStudentSubjectScoreService, StudentSubjectScoreService>();
            services.AddTransient<IStudentSubjectService, StudentSubjectService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
