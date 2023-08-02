using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicLevels;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicYears;
using SchoolAttendance.Application.Pipelines.User.Queries.GetUserById;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Helpers;
using SchoolAttendance.Domain.Repositories.Query;
using SchoolAttendance.Infrastructure.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly ISchoolAttendanceContext _db;
        private readonly ILogger<IReportService> _logger;
        private readonly IConfiguration _config;
        private readonly ICoreDataService _coreDataService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public ReportService(
            ISchoolAttendanceContext db,
            ILogger<IReportService> logger,
            IConfiguration config,
            ICoreDataService coreDataService,
            ICurrentUserService currentUserService,
            IMediator mediator)
        {
            this._db = db;
            this._logger = logger;
            this._config = config;
            this._coreDataService = coreDataService;
            this._currentUserService = currentUserService;
            this._mediator = mediator;
        }

        public async Task<ClassTeacheeDropDownMasterData> GetTeacherClassMasterData()
        {
            var response = new ClassTeacheeDropDownMasterData();

            var currentYear = _db.AcademicYears.FirstOrDefault(x => x.IsCurrentYear == true);

            var currentUser = await _mediator.Send(new GetUserByIdQuery(_currentUserService.UserId.Value));

            var academicYears = await _mediator.Send(new GetAllAcademicYearsQuery());

            var academicLevels = await _mediator.Send(new GetAllAcademicLevelsQuery());

            response.AcademicYears.AddRange(academicYears);

            response.Grades.AddRange(academicLevels);

            response.SelectedYearId = currentYear.Id;

            var assignedClass = _db.Classes.FirstOrDefault(x => x.ClassTeacherId == _currentUserService.UserId && x.AcademicYear == currentYear.Id);

            if (assignedClass != null)
            {
                response.SelectedClassId = assignedClass.Id;

                response.SelectedGradeId = assignedClass.GradeId;

                response.Classes = _db.Classes.Where(x => x.AcademicYear == currentYear.Id && x.GradeId == assignedClass.GradeId)
                                          .Select(g => new DropDownViewModel() { Id = g.Id, Name = g.Name })
                                          .OrderBy(x => x.Name).ToList();
            }

            DateTime monday = DateTime.UtcNow.ConvertToLocalTime(currentUser.TimeZoneId).StartOfWeek(DayOfWeek.Monday);

            response.FromYear = monday.Year;
            response.FromMonth = monday.Month;
            response.FromDay = monday.Day;

            DateTime friday = monday.AddDays(4);
            response.ToYear = friday.Year;
            response.ToMonth = friday.Month;
            response.ToDay = friday.Day;

            return response;
        }

        public DownloadFileModel DownloadClassAttendanceForAllSubjects(AttendanceReportFilter filter)
        {
            var reportFactory = new ReportFactory();
            var reportParams = new Dictionary<string, string>();
            reportParams.Add("ReportType", "WeeklyAttedance");
            reportParams.Add("fromYear", filter.FromYear.ToString());
            reportParams.Add("fromMonth", filter.FromMonth.ToString());
            reportParams.Add("fromDay", filter.FromDay.ToString());
            reportParams.Add("toYear", filter.ToYear.ToString());
            reportParams.Add("toMonth", filter.ToMonth.ToString());
            reportParams.Add("toDay", filter.ToDay.ToString());
            reportParams.Add("selectedClassId", filter.SelectedClassId.ToString());

            var selectedClass = _db.Classes.FirstOrDefault(x => x.Id == filter.SelectedClassId);
            reportParams.Add("className", selectedClass.Name);

            var reportGenerator = reportFactory.GetReportGenerator(reportParams, _db, _config);

            var response = reportGenerator.GenerateExcelReport();

            return response;
        }

        public DownloadFileModel DownloadClassAttendanceForSelectedSubject(AttendanceReportFilter filter)
        {
            var reportFactory = new ReportFactory();
            var reportParams = new Dictionary<string, string>();
            reportParams.Add("ReportType", "WeeklyAttedance");
            reportParams.Add("fromYear", filter.FromYear.ToString());
            reportParams.Add("fromMonth", filter.FromMonth.ToString());
            reportParams.Add("fromDay", filter.FromDay.ToString());
            reportParams.Add("toYear", filter.ToYear.ToString());
            reportParams.Add("toMonth", filter.ToMonth.ToString());
            reportParams.Add("toDay", filter.ToDay.ToString());
            reportParams.Add("selectedClassId", filter.SelectedClassId.ToString());

            var selectedClass = _db.Classes.FirstOrDefault(x => x.Id == filter.SelectedClassId);
            reportParams.Add("className", selectedClass.Name);

            var reportGenerator = reportFactory.GetReportGenerator(reportParams, _db, _config);

            var response = reportGenerator.GenerateExcelReport();

            return response;
        }

        public DownloadFileModel GenerateZonalReportForSelectedClass(AttendanceReportFilter filter)
        {
            var reportFactory = new ReportFactory();
            var reportParams = new Dictionary<string, string>();
            reportParams.Add("ReportType", "WeeklyAttedance");
            reportParams.Add("fromYear", filter.FromYear.ToString());
            reportParams.Add("fromMonth", filter.FromMonth.ToString());
            reportParams.Add("fromDay", filter.FromDay.ToString());
            reportParams.Add("toYear", filter.ToYear.ToString());
            reportParams.Add("toMonth", filter.ToMonth.ToString());
            reportParams.Add("toDay", filter.ToDay.ToString());
            reportParams.Add("selectedClassId", filter.SelectedClassId.ToString());

            var selectedClass = _db.Classes.FirstOrDefault(x => x.Id == filter.SelectedClassId);
            reportParams.Add("className", selectedClass.Name);

            var poReportGenerator = reportFactory.GetReportGenerator(reportParams, _db, _config);

            return null;
        }
    }
}
