using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
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
    private readonly IDropDownService dropDownService;

    public DropDownController(ILogger<DropDownController> logger, IConfiguration config, IIdentityService identityService, IDropDownService dropDownService)
    {
      this.logger = logger;
      this.config = config;
      this.identityService = identityService;
      this.dropDownService = dropDownService;
    }

    [HttpGet]
    [Route("getAllAcademicLevels")]
    public List<DropDownViewModel> GetAllAcademicLevels()
    {
      var response = dropDownService.GetAllAcademicLevels();

      return response;
    }

    [HttpGet]
    [Route("getAllAcademicYears")]
    public List<DropDownViewModel> GetAllAcademicYears()
    {
      var response = dropDownService.GetAllAcademicYears();

      return response;
    }

    [HttpGet]
    [Route("getClassesForSelectedGrade/{academicYearId}/{gradeId}")]
    public List<DropDownViewModel> GetClassesForSelectedGrade(int academicYearId, int gradeId)
    {
      var response = dropDownService.GetClassesForSelectedGrade(academicYearId,gradeId);

      return response;
    }

    [HttpGet]
    [Route("getTeachGradesForLoggedInUser/{academicYear}")]
    public List<DropDownViewModel> GetTeachGradesForLoggedInUser(int academicYear)
    {
      var userName = identityService.GetUserName();

      var response = dropDownService.GetTeachGradesForLoggedInUser(academicYear, userName);

      return response;
    }


    [HttpGet]
    [Route("getAssignedClassForLoggedInUser/{academicYear}/{gradeId}")]
    public List<DropDownViewModel> GetAssignedClassForLoggedInUser(int academicYear, int gradeId)
    {
      var userName = identityService.GetUserName();

      var response = dropDownService.GetAssignedClassForLoggedInUser(academicYear, gradeId, userName);

      return response;
    }

    [HttpGet]
    [Route("getAssignedClassSubjectForLoggedInUser/{classId}")]
    public List<DropDownViewModel> GetAssignedClassSubjectForLoggedInUser(int classId)
    {
      var userName = identityService.GetUserName();

      var response = dropDownService.GetAssignedClassSubjectForLoggedInUser(classId, userName);

      return response;
    }

    [HttpGet]
    [Route("getSubjectClasses/{gradeId}/{subjectId}/{lessonId}")]
    public List<DropDownViewModel> GetSubjectClasses(int gradeId,int subjectId, int lessonId)
    {
      var userName = identityService.GetUserName();

      var response = dropDownService.GetSubjectClasses(gradeId, subjectId,lessonId, userName);

      return response;
    }

    [HttpGet]
    [Route("getAssignedClassForTeacher/{academicYear}/{gradeId}/{teacherId}")]
    public List<DropDownViewModel> GetAssignedClassForTeacher(int academicYear, int gradeId, int teacherId)
    {
      var response = dropDownService.GetAssignedClassForTeacher(academicYear, gradeId, teacherId);

      return response;
    }

    [HttpGet]
    [Route("getAssignedClassSubjectForTeacher/{classId}/{teacherId}")]
    public List<DropDownViewModel> GetAssignedClassSubjectForTeacher(int classId, int teacherId)
    {
      var response = dropDownService.GetAssignedClassSubjectForTeacher(classId, teacherId);

      return response;
    }

    [HttpGet]
    [Route("getTeacherAssignedSubjectForSelectedGrade/{gradeId}")]
    public List<DropDownViewModel> GetTeacherAssignedSubjectForSelectedGrade(int gradeId)
    {
      var userName = identityService.GetUserName();

      var response = dropDownService.GetTeacherAssignedSubjectForSelectedGrade(gradeId, userName);

      return response;
    }

    [HttpGet]
    [Route("getTeacherAssignedSubject/{gradeId}")]
    public List<DropDownViewModel> GetTeacherAssignedSubject(int gradeId)
    {
      var userName = identityService.GetUserName();

      var response = dropDownService.GetTeacherAssignedSubject(gradeId, userName);

      return response;
    }

    [HttpGet]
    [Route("getAllSubjects")]
    public List<CheckBoxViewModel> GetAllSubjects()
    {
      var response = dropDownService.GetAllSubjects();

      return response;
    }

    [HttpGet]
    [Route("getAllTeachers")]
    public List<DropDownViewModel> GetAllTeachers()
    {
      var response = dropDownService.GetAllTeachers();

      return response;
    }

    [HttpGet]
    [Route("getAllLevelHeads")]
    public List<DropDownViewModel> GetAllLevelHeads()
    {
      var response = dropDownService.GetAllLevelHeads();

      return response;
    }

    [HttpGet]
    [Route("getAllDepartmentHeads")]
    public List<DropDownViewModel> GetAllDepartmentHeads()
    {
      var response = dropDownService.GetAllDepartmentHeads();

      return response;
    }

    [HttpGet]
    [Route("getAllSystemRoles")]
    public List<DropDownViewModel> GetAllSystemRoles()
    {
      var response = dropDownService.GetAllSystemRoles();

      return response;
    }

  }
}
