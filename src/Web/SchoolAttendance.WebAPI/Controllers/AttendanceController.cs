using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.Attendance.Commands.SaveAttendanceDetailForClassSubject;
using SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceDetailForClassSubject;
using SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceDetailForSubjectClassById;
using SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceListTeacherDropdownMasterData;
using SchoolAttendance.Application.Pipelines.Attendance.Queries.GetMySubjectAttendance;
using SchoolAttendance.Application.Pipelines.Attendance.Queries.GetStartAndEndTime;
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
    public class AttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttendanceController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
        [HttpPost]
        [Route("getStartAndEndTime")]
        public async Task<AttendanceFilterViewModel> GetStartAndEndTime(AttendanceFilterViewModel filter)
        {
            var response = await _mediator.Send(new GetStartAndEndTimeQuery(filter));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
        [HttpGet]
        [Route("getAttendanceListTeacherDropdownMasterData")]
        public async Task<AttendanceListFilterMasterData> GetAttendanceListTeacherDropdownMasterData()
        {
            var response = await _mediator.Send(new GetAttendanceListTeacherDropdownMasterDataQuery());

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
        [HttpGet]
        [Route("getMySubjectAttendance")]
        public async Task<PaginatedItemsViewModel<BasicAttendanceViewModel>> GetMySubjectAttendance(string searchText, int currentPage, int pageSize, int academicYearId, int gradeId, int classId, int subjectId)
        {
            var response = await _mediator.Send(new GetMySubjectAttendanceQuery(searchText, currentPage, pageSize, academicYearId, gradeId, classId, subjectId));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
        [HttpGet]
        [Route("getAttendanceDetailForSubjectClassById/{id}")]
        public async Task<SubjectAttendaceViewModel> GetAttendanceDetailForSubjectClassById(int id)
        {
            var response = await _mediator.Send(new GetAttendanceDetailForSubjectClassByIdQuery(id));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
        [HttpPost]
        [Route("getAttendanceDetailForClassSubject")]
        public async Task<SubjectAttendaceViewModel> GetAttendanceDetailForClassSubject([FromBody] AttendanceFilterViewModel filter)
        {
            var response = await _mediator.Send(new GetAttendanceDetailForClassSubjectQuery(filter));

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.AuthorizedForAdminAndTeacher)]
        [HttpPost]
        [Route("saveAttendanceDetailForClassSubject")]
        public async Task<ResponseViewModel> SaveAttendanceDetailForClassSubject([FromBody] SubjectAttendaceViewModel vm)
        {
            var response = await _mediator.Send(new SaveAttendanceDetailForClassSubjectCommand() 
            {  
                SubjectAttendaceViewModel = vm 
            });

            return response;
        }
    }
}
