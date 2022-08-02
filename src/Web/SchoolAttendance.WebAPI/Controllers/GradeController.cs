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
  public class GradeController : ControllerBase
  {
    private readonly ILogger<GradeController> logger;
    private readonly IConfiguration config;
    private readonly IGradeService gradeService;
    private readonly IIdentityService identityService;

    public GradeController(ILogger<GradeController> logger, IConfiguration config, IGradeService gradeService, IIdentityService identityService)
    {
      this.logger = logger;
      this.config = config;
      this.gradeService = gradeService;
      this.identityService = identityService;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpGet]
    [Route("getGradeList")]
    public List<GradeViewModel> GetGradeList()
    {
      var response = gradeService.GetGradeList();

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpPost]
    [Route("saveGradeDetail")]
    public async Task<ResponseViewModel> SaveGradeDetail(GradeViewModel vm)
    {
      var response = await gradeService.SaveGradeDetail(vm);

      return response;
    }
  }
}
