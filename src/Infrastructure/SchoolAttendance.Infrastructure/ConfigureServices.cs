using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using SchoolAttendance.Infrastructure.Data;
using SchoolAttendance.Infrastructure.Interceptors;
using SchoolAttendance.Infrastructure.Repositories.Commands;
using SchoolAttendance.Infrastructure.Repositories.Queries;
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


            services.AddTransient<IUserQueryRepository, UserQueryRepository>();
            services.AddTransient<IUserCommandRepository, UserCommandRepository>();
            services.AddTransient<IStudentClassCommandRepository, StudentClassCommandRepository>();
            services.AddTransient<IUserRoleCommandRepository, UserRoleCommandRepository>();
            services.AddTransient<ISubjectTeacherCommandRepository, SubjectTeacherCommandRepository>();
            services.AddTransient<IAcademicYearQueryRepository, AcademicYearQueryRepository>();
            services.AddTransient<IClassQueryRepository, ClassQueryRepository>();
            services.AddTransient<IGradeQueryRepository, GradeQueryRepository>();
            services.AddTransient<IGradeCommandRepository, GradeCommandRepository>();
            services.AddTransient<IGradeSubjectCommandRepository, GradeSubjectCommandRepository>();
            services.AddTransient<IClassSubjectCommandRepository, ClassSubjectCommandRepository>();
            services.AddTransient<IClassSubjectTimeTableQueryRepository, ClassSubjectTimeTableQueryRepository>();


            services.AddTransient<IGradeQueryRepository, GradeQueryRepository>();
            services.AddTransient<IGradeCommandRepository, GradeCommandRepository>();



            services.AddTransient<IAssessmentService, AssessmentService>();
            services.AddTransient<IAttendanceService, AttendanceService>();
            services.AddTransient<IAzureBlobService, AzureBlobService>();
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<ICoreDataService, CoreDataService>();
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
