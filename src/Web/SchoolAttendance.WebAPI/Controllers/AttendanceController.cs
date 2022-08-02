using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAttendance.WebAPI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class AttendanceController : ControllerBase
  {
    private readonly ILogger<AttendanceController> logger;
    private readonly IConfiguration config;
    private readonly IAttendanceService attendanceService;
    private readonly IIdentityService identityService;

    public AttendanceController(ILogger<AttendanceController> logger, IConfiguration config, IAttendanceService attendanceService, IIdentityService identityService)
    {
      this.logger = logger;
      this.config = config;
      this.attendanceService = attendanceService;
      this.identityService = identityService;
    }

    [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
    [HttpPost]
    [Route("getStartAndEndTime")]
    public AttendanceFilterViewModel GetStartAndEndTime(AttendanceFilterViewModel filter)
    {
      var response = attendanceService.GetStartAndEndTime(filter); 

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
    [HttpGet]
    [Route("getAttendanceListTeacherDropdownMasterData")]
    public AttendanceListFilterMasterData GetAttendanceListTeacherDropdownMasterData()
    {

      var userName = identityService.GetUserName();
      var response = attendanceService.GetAttendanceListTeacherDropdownMasterData(userName); ;

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
    [HttpGet]
    [Route("getMySubjectAttendance")]
    public PaginatedItemsViewModel<BasicAttendanceViewModel> GetMySubjectAttendance(string searchText, int currentPage, int pageSize, int academicYearId, int gradeId, int classId, int subjectId)
    {
      var userName = identityService.GetUserName();

      var response = attendanceService.GetMySubjectAttendance(searchText, currentPage, pageSize, academicYearId, gradeId, classId, subjectId, userName);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
    [HttpGet]
    [Route("getAttendanceDetailForSubjectClassById/{id}")]
    public SubjectAttendaceViewModel GetAttendanceDetailForSubjectClassById(int id)
    {
      var userName = identityService.GetUserName();
      var response = attendanceService.GetAttendanceDetailForSubjectClassById(id, userName);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
    [HttpPost]
    [Route("getAttendanceDetailForClassSubject")]
    public SubjectAttendaceViewModel GetAttendanceDetailForClassSubject([FromBody] AttendanceFilterViewModel filter)
    {
      var userName = identityService.GetUserName();
      var response = attendanceService.GetAttendanceDetailForClassSubject(filter, userName);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
    [HttpPost]
    [Route("saveAttendanceDetailForClassSubject")]
    public async Task<ResponseViewModel> SaveAttendanceDetailForClassSubject([FromBody] SubjectAttendaceViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await attendanceService.SaveAttendanceDetailForClassSubject(vm);

      return response;
    }
  }
}
