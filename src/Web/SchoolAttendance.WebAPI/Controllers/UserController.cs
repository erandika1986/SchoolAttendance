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
  public class UserController : ControllerBase
  {
    private readonly ILogger<UserController> logger;
    private readonly IConfiguration config;
    private readonly IIdentityService identityService;
    private readonly IUserService userService;

    public UserController(ILogger<UserController> logger, IConfiguration config, IIdentityService identityService, IUserService userService)
    {
      this.logger = logger;
      this.identityService = identityService;
      this.config = config;
      this.userService = userService;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpGet]
    [Route("getUsersList")]
    public PaginatedItemsViewModel<UserViewModel> GetUsersList(string searchText, int currentPage, int pageSize)
    {
      var response = userService.GetUsersList(searchText, currentPage, pageSize);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpGet]
    [Route("getUserById/{id}")]
    public UserViewModel GetUserById(int id)
    {
      var response = userService.GetUserById(id);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpPost]
    [Route("saveUser")]
    public async Task<ResponseViewModel> SaveUser(UserViewModel vm)
    {
      var userName = identityService.GetUserName();

      var response = await userService.SaveUser(vm, userName);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpDelete]
    [Route("deleteUser/{id}")]
    public async Task<ResponseViewModel> DeleteUser(int id)
    {

      var response = await userService.DeleteUser(id);

      return response;
    }

    [HttpGet]
    [Route("getStudentList")]
    public PaginatedItemsViewModel<BasicStudentViewModel> GetStudentList(string searchText, int currentPage, int pageSize,int academicYearId,int gradeId, int classId)
    {
      var response = userService.GetStudentList(searchText, currentPage, pageSize, academicYearId, gradeId, classId);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpGet]
    [Route("getStudentById/{id}")]
    public StudentViewModel GetStudentById(int id)
    {
      StudentViewModel response = userService.GetStudentById(id);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpPost]
    [Route("updateUserPassword")]
    public async Task<ResponseViewModel> UpdateUserPassword(PasswordUpdateViewModel vm)
    {
      var response = await userService.UpdateUserPassword(vm);

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpPost]
    [Route("saveStudent")]
    public async Task<ResponseViewModel> SaveStudent(StudentViewModel vm)
    {
      var userName = identityService.GetUserName();

      var response = await userService.SaveStudent(vm, userName);

      return response;
    }


    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpDelete]
    [Route("deleteStudent/{id}")]
    public async Task<ResponseViewModel> DeleteStudent(int id)
    {
      var response = await userService.DeleteStudent(id);

      return response;
    }


    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpGet]
    [Route("getStudentDropdownsMasterData")]
    public StudentListDropDownMasterData GetStudentDropdownsMasterData()
    {
      var response =  userService.GetStudentDropdownsMasterData();

      return response;
    }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    [Route("uploadClassStudents")]
    public async Task<IActionResult> UploadClassStudents()
    {
      var userName = identityService.GetUserName();

      var container = new FileContainerViewModel();

      var request = await Request.ReadFormAsync();

      //container.Id = int.Parse(request["id"]);

      foreach (var file in request.Files)
      {
        container.Files.Add(file);
      }

      var response = await userService.UploadClassStudents(container, userName);

      return Ok(response);
    }
  }
}
