using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAttendance.WebAPI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ReportController : ControllerBase
  {
    private readonly ILogger<ReportController> logger;
    private readonly IConfiguration config;
    private readonly IReportService reportService;
    private readonly IIdentityService identityService;

    public ReportController(ILogger<ReportController> logger, IConfiguration config, IReportService reportService, IIdentityService identityService)
    {
      this.logger = logger;
      this.config = config;
      this.reportService = reportService;
      this.identityService = identityService;
    }

    [HttpGet]
    [Authorize]
    [Route("getTeacherClassMasterData")]
    public async Task<ClassTeacheeDropDownMasterData> GetTeacherClassMasterData()
    {
      var userName = identityService.GetUserName();
      var response = await reportService.GetTeacherClassMasterData();

      return response;
    }

    [HttpPost]
    [Authorize]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadClassAttendanceForAllSubjects")]
    public FileStreamResult DownloadClassAttendanceForAllSubjects(AttendanceReportFilter filter)
    {
      var response = reportService.DownloadClassAttendanceForAllSubjects(filter);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }

    [HttpPost]
    [Authorize]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadClassAttendanceForSelectedSubject")]
    public FileStreamResult DownloadClassAttendanceForSelectedSubject(AttendanceReportFilter filter)
    {
      var response = reportService.DownloadClassAttendanceForSelectedSubject(filter);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }

    [HttpPost]
    [Authorize]
    [RequestSizeLimit(long.MaxValue)]
    [Route("generateZonalReportForSelectedClass")]
    public FileStreamResult GenerateZonalReportForSelectedClass(AttendanceReportFilter filter)
    {
      var response = reportService.GenerateZonalReportForSelectedClass(filter);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }
  }

}
