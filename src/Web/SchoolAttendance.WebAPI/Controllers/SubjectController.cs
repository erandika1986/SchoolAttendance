using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetParentSubjects;
using SchoolAttendance.Application.Pipelines.Subject.Commands.DeleteSubject;
using SchoolAttendance.Application.Pipelines.Subject.Commands.SaveSubject;
using SchoolAttendance.Application.Pipelines.Subject.Queries.GetSubjectList;
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

        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getSubjectList")]
        public async Task<PaginatedItemsViewModel<SubjectViewModel>> GetSubjectList(string searchText, int currentPage, int pageSize, bool status)
        {
            var response = await _mediator.Send(new GetSubjectListQuery(searchText, currentPage, pageSize, status));

            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("getParentSubjects")]
        public async Task<IActionResult> GetParentSubjects()
        {
            var response = await _mediator.Send(new GetParentSubjectsQuery());

            return Ok(response);
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpPost]
        [Route("saveSubject")]
        public async Task<ResponseViewModel> SaveSubject(SubjectViewModel vm)
        {
            var response = await _mediator.Send(new SaveSubjectCommand(vm));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpDelete]
        [Route("deleteSubject/{id}")]
        public async Task<ResponseViewModel> DeleteSubject(int id)
        {
            var response = await _mediator.Send(new DeleteSubjectCommand(id));

            return response;
        }
    }
}
