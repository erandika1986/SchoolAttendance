using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Helpers;
using SchoolAttendance.Infrastructure.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
  public class ReportService: IReportService
  {
    private readonly ISchoolAttendanceContext db;
    private readonly ILogger<IReportService> logger;
    private readonly IConfiguration config;
    private readonly ICoreDataService coreDataService;
    private readonly IDropDownService dropDownService;

    public ReportService(ISchoolAttendanceContext db, ILogger<IReportService> logger, IConfiguration config, ICoreDataService coreDataService, IDropDownService dropDownService)
    {
      this.db = db;
      this.logger = logger;
      this.config = config;
      this.coreDataService = coreDataService;
      this.dropDownService = dropDownService;
    }

    public ClassTeacheeDropDownMasterData GetTeacherClassMasterData(string userName)
    {
      var response = new ClassTeacheeDropDownMasterData();

      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      var currentYear = db.AcademicYears.FirstOrDefault(x => x.IsCurrentYear == true);

      //response.Role = currentUser.Role;

      response.AcademicYears.AddRange(dropDownService.GetAllAcademicYears());

      response.Grades.AddRange(dropDownService.GetAllAcademicLevels());

      response.SelectedYearId = currentYear.Id;

      var assigneClass = db.Classes.FirstOrDefault(x => x.ClassTeacherId == currentUser.Id && x.AcademicYear == currentYear.Id);

      if(assigneClass!=null)
      {
        response.SelectedClassId = assigneClass.Id;

        response.SelectedGradeId = assigneClass.GradeId;

        response.Classes = db.Classes.Where(x => x.AcademicYear == currentYear.Id && x.GradeId == assigneClass.GradeId)
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
      var reportFacotry = new ReportFactory();
      var reportParams = new Dictionary<string, string>();
      reportParams.Add("ReportType", "WeeklyAttedance");
      reportParams.Add("fromYear", filter.FromYear.ToString());
      reportParams.Add("fromMonth", filter.FromMonth.ToString());
      reportParams.Add("fromDay", filter.FromDay.ToString());
      reportParams.Add("toYear", filter.ToYear.ToString());
      reportParams.Add("toMonth", filter.ToMonth.ToString());
      reportParams.Add("toDay", filter.ToDay.ToString());
      reportParams.Add("selectedClassId", filter.SelectedClassId.ToString());

      var selectedClass = db.Classes.FirstOrDefault(x => x.Id == filter.SelectedClassId);
      reportParams.Add("clasName", selectedClass.Name);

      var reportGenerator = reportFacotry.GetReportGenerator(reportParams, db, config);

      var response = reportGenerator.GenerateExcelReport();

      return response;
    }

    public DownloadFileModel DownloadClassAttendanceForSelectedSubject(AttendanceReportFilter filter)
    {
      var reportFacotry = new ReportFactory();
      var reportParams = new Dictionary<string, string>();
      reportParams.Add("ReportType", "WeeklyAttedance");
      reportParams.Add("fromYear", filter.FromYear.ToString());
      reportParams.Add("fromMonth", filter.FromMonth.ToString());
      reportParams.Add("fromDay", filter.FromDay.ToString());
      reportParams.Add("toYear", filter.ToYear.ToString());
      reportParams.Add("toMonth", filter.ToMonth.ToString());
      reportParams.Add("toDay", filter.ToDay.ToString());
      reportParams.Add("selectedClassId", filter.SelectedClassId.ToString());

      var selectedClass = db.Classes.FirstOrDefault(x => x.Id == filter.SelectedClassId);
      reportParams.Add("clasName", selectedClass.Name);

      var reportGenerator = reportFacotry.GetReportGenerator(reportParams, db, config);

      var response = reportGenerator.GenerateExcelReport();

      return response;
    }

    public DownloadFileModel GenerateZonalReportForSelectedClass(AttendanceReportFilter filter)
    {
      var reportFacotry = new ReportFactory();
      var reportParams = new Dictionary<string, string>();
      reportParams.Add("ReportType", "WeeklyAttedance");
      reportParams.Add("fromYear", filter.FromYear.ToString());
      reportParams.Add("fromMonth", filter.FromMonth.ToString());
      reportParams.Add("fromDay", filter.FromDay.ToString());
      reportParams.Add("toYear", filter.ToYear.ToString());
      reportParams.Add("toMonth", filter.ToMonth.ToString());
      reportParams.Add("toDay", filter.ToDay.ToString());
      reportParams.Add("selectedClassId", filter.SelectedClassId.ToString());

      var selectedClass = db.Classes.FirstOrDefault(x => x.Id == filter.SelectedClassId);
      reportParams.Add("clasName", selectedClass.Name);

      var poReportGenerator = reportFacotry.GetReportGenerator(reportParams, db, config);

      return null;
    }
  }
}
