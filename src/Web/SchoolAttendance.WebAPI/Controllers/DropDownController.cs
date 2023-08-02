using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicLevels;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicYears;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllDepartmentHeads;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllLevelHeads;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllSubjects;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllSystemRoles;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllTeachers;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAssignedClassForLoggedInUser;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAssignedClassForTeacher;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAssignedClassSubjectForLoggedInUser;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAssignedClassSubjectForTeacher;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetClassesForSelectedGrade;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetSubjectClasses;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetTeacherAssignedSubject;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetTeacherAssignedSubjectForSelectedGrade;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetTeachGradesForLoggedInUser;
using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAttendance.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly ILogger<DropDownController> logger;
        private readonly IConfiguration config;
        private readonly IIdentityService identityService;
        private readonly IMediator _mediator;

        public DropDownController(
            ILogger<DropDownController> logger,
            IConfiguration config,
            IIdentityService identityService,
            IMediator mediator)
        {
            this.logger = logger;
            this.config = config;
            this.identityService = identityService;
            this._mediator = mediator;
        }

        [HttpGet]
        [Route("getAllAcademicLevels")]
        public async Task<IActionResult> GetAllAcademicLevels()
        {
            var response = await _mediator.Send(new GetAllAcademicLevelsQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllAcademicYears")]
        public async Task<IActionResult> GetAllAcademicYears()
        {
            var response = await _mediator.Send(new GetAllAcademicYearsQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("getClassesForSelectedGrade/{academicYearId}/{gradeId}")]
        public async Task<IActionResult> GetClassesForSelectedGrade(int academicYearId, int gradeId)
        {
            var response = await _mediator.Send(new GetClassesForSelectedGradeQuery(academicYearId, gradeId));

            return Ok(response);
        }

        [HttpGet]
        [Route("getTeachGradesForLoggedInUser/{academicYear}")]
        public async Task<IActionResult> GetTeachGradesForLoggedInUser(int academicYear)
        {
            var response = await _mediator.Send(new GetTeachGradesForLoggedInUserQuery(academicYear));

            return Ok(response);
        }


        [HttpGet]
        [Route("getAssignedClassForLoggedInUser/{academicYear}/{gradeId}")]
        public async Task<IActionResult> GetAssignedClassForLoggedInUser(int academicYear, int gradeId)
        {
            var response = await _mediator.Send(new GetAssignedClassForLoggedInUserQuery(academicYear, gradeId));

            return Ok(response);
        }

        [HttpGet]
        [Route("getAssignedClassSubjectForLoggedInUser/{classId}")]
        public async Task<IActionResult> GetAssignedClassSubjectForLoggedInUser(int classId)
        {
            var response = await _mediator.Send(new GetAssignedClassSubjectForLoggedInUserQuery(classId));

            return Ok(response);
        }

        [HttpGet]
        [Route("getSubjectClasses/{gradeId}/{subjectId}/{lessonId}")]
        public async Task<IActionResult> GetSubjectClasses(int gradeId, int subjectId, int lessonId)
        {
            var response = await _mediator.Send(new GetSubjectClassesQuery(gradeId, subjectId, lessonId));

            return Ok(response);
        }

        [HttpGet]
        [Route("getAssignedClassForTeacher/{academicYear}/{gradeId}/{teacherId}")]
        public async Task<IActionResult> GetAssignedClassForTeacher(int academicYear, int gradeId, int teacherId)
        {
            var response = await _mediator.Send(new GetAssignedClassForTeacherQuery(academicYear, gradeId, teacherId));

            return Ok(response);
        }

        [HttpGet]
        [Route("getAssignedClassSubjectForTeacher/{classId}/{teacherId}")]
        public async Task<IActionResult> GetAssignedClassSubjectForTeacher(int classId, int teacherId)
        {
            var response = await _mediator.Send(new GetAssignedClassSubjectForTeacherQuery(classId, teacherId));

            return Ok(response);
        }

        [HttpGet]
        [Route("getTeacherAssignedSubjectForSelectedGrade/{gradeId}")]
        public async Task<IActionResult> GetTeacherAssignedSubjectForSelectedGrade(int gradeId)
        {
            var response =await _mediator.Send(new GetTeacherAssignedSubjectForSelectedGradeQuery(gradeId));

            return Ok(response);
        }

        [HttpGet]
        [Route("getTeacherAssignedSubject/{gradeId}")]
        public async Task<IActionResult> GetTeacherAssignedSubject(int gradeId)
        {
            var response = await _mediator.Send(new GetTeacherAssignedSubjectQuery(gradeId));

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var response = await _mediator.Send(new GetAllSubjectsQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var response = await _mediator.Send(new GetAllTeachersQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllLevelHeads")]
        public async Task<IActionResult> GetAllLevelHeads()
        {
            var response = await _mediator.Send(new GetAllLevelHeadsQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllDepartmentHeads")]
        public async Task<IActionResult> GetAllDepartmentHeads()
        {
            var response = await _mediator.Send(new GetAllDepartmentHeadsQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllSystemRoles")]
        public async Task<IActionResult> GetAllSystemRoles()
        {
            var response = await _mediator.Send(new GetAllSystemRolesQuery());

            return Ok(response);
        }

    }
}
