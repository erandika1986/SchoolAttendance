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
  public class SubjectController : ControllerBase
  {
    private readonly ILogger<SubjectController> logger;
    private readonly IConfiguration config;
    private readonly ISubjectService subjectService;
    private readonly IIdentityService identityService;

    public SubjectController(ILogger<SubjectController> logger, IConfiguration config, ISubjectService subjectService, IIdentityService identityService)
    {
      this.logger = logger;
      this.config = config;
      this.subjectService = subjectService;
      this.identityService = identityService;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpGet]
    [Route("getSubjectList")]
    public PaginatedItemsViewModel<SubjectViewModel> GetSubjectList(string searchText, int currentPage, int pageSize, bool status)
    {
      var response = subjectService.GetSubjectList(searchText, currentPage, pageSize, status);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpPost]
    [Route("saveSubject")]
    public async Task<ResponseViewModel> SaveSubject(SubjectViewModel vm)
    {
      var response = await subjectService.SaveSubject(vm);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpDelete]
    [Route("deleteSubject/{id}")]
    public async Task<ResponseViewModel> DeleteSubject(int id)
    {
      var  response = await subjectService.DeleteSubject(id);

      return response;
    }
  }
}
