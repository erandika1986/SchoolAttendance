using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.User.Commands.DeleteStudent;
using SchoolAttendance.Application.Pipelines.User.Commands.DeleteUser;
using SchoolAttendance.Application.Pipelines.User.Commands.SaveStudent;
using SchoolAttendance.Application.Pipelines.User.Commands.SaveUser;
using SchoolAttendance.Application.Pipelines.User.Commands.UpdateUserPassword;
using SchoolAttendance.Application.Pipelines.User.Commands.UploadClassStudents;
using SchoolAttendance.Application.Pipelines.User.Queries.GetStudentById;
using SchoolAttendance.Application.Pipelines.User.Queries.GetStudentDropdownsMasterData;
using SchoolAttendance.Application.Pipelines.User.Queries.GetStudentList;
using SchoolAttendance.Application.Pipelines.User.Queries.GetUserById;
using SchoolAttendance.Application.Pipelines.User.Queries.GetUsersList;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Constants;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAttendance.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getUsersList")]
        public async Task<PaginatedItemsViewModel<UserViewModel>> GetUsersList(int roleId, string searchText, int currentPage, int pageSize)
        {
            var response = await _mediator.Send(new GetUsersListQuery(roleId, searchText, currentPage, pageSize));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getUserById/{id}")]
        public async Task<UserViewModel> GetUserById(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpPost]
        [Route("saveUser")]
        public async Task<ResponseViewModel> SaveUser(UserViewModel vm)
        {
            var response = await _mediator.Send(new SaveUserCommand() { User = vm });

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpDelete]
        [Route("deleteUser/{id}")]
        public async Task<ResponseViewModel> DeleteUser(int id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(id));

            return response;
        }

        [HttpGet]
        [Route("getStudentList")]
        public async Task<PaginatedItemsViewModel<BasicStudentViewModel>> GetStudentList(
            string searchText, 
            int currentPage, 
            int pageSize, 
            int academicYearId, 
            int gradeId, 
            int classId)
        {
            var response = await _mediator
                .Send(new GetStudentListQuery(searchText, currentPage, pageSize, academicYearId, gradeId, classId));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getStudentById/{id}")]
        public async Task<StudentViewModel> GetStudentById(int id)
        {
            var response = await _mediator
                .Send(new GetStudentByIdQuery(id));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpPost]
        [Route("updateUserPassword")]
        public async Task<ResponseViewModel> UpdateUserPassword(PasswordUpdateViewModel vm)
        {
            var response = await _mediator
                .Send(new UpdateUserPasswordCommand(vm));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpPost]
        [Route("saveStudent")]
        public async Task<ResponseViewModel> SaveStudent(StudentViewModel vm)
        {
            var response = await _mediator
                .Send(new SaveStudentCommand(vm));

            return response;
        }


        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpDelete]
        [Route("deleteStudent/{id}")]
        public async Task<ResponseViewModel> DeleteStudent(int id)
        {
            var response = await _mediator
                .Send(new DeleteStudentCommand(id));

            return response;
        }


        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getStudentDropdownsMasterData")]
        public async Task<StudentListDropDownMasterData> GetStudentDropdownsMasterData()
        {
            var response = await _mediator
                .Send(new GetStudentDropdownsMasterDataQuery());

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Route("uploadClassStudents")]
        public async Task<IActionResult> UploadClassStudents()
        {
            var container = new FileContainerViewModel();

            var request = await Request.ReadFormAsync();

            //container.Id = int.Parse(request["id"]);

            foreach (var file in request.Files)
            {
                container.Files.Add(file);
            }

            var response = await _mediator
                .Send(new UploadClassStudentsCommand() 
                { 
                    Container = container 
                });



            return Ok(response);
        }
    }
}
