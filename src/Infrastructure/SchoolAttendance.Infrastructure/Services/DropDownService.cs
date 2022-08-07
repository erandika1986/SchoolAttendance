using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
  public class DropDownService: IDropDownService
  {

    private readonly ISchoolAttendanceContext db;
    private readonly ILogger<IDropDownService> logger;
    private readonly ICoreDataService coreDataService; 

    public DropDownService(ISchoolAttendanceContext db, ILogger<DropDownService> logger, ICoreDataService coreDataService)
    {
      this.db = db;
      this.logger = logger;
      this.coreDataService = coreDataService;
    }

    public List<DropDownViewModel> GetAllAcademicYears()
    {
      return db.AcademicYears
        .Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name })
        .OrderBy(x => x.Name)
        .ToList();
    }

    public List<DropDownViewModel> GetAllAcademicLevels()
    {
      return db.Grades.Where(g=>g.IsActive==true)
        .Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name })
        .OrderBy(x=>x.Id)
        .ToList();
    }

    public List<DropDownViewModel> GetClassesForSelectedGrade(int academicYearId,int gradeId)
    {
      var classes = db.Grades.Where(g => g.IsActive == true && g.Id == gradeId)
        .SelectMany(cl => cl.Classes).Where(t =>t.AcademicYear == academicYearId)
        .Distinct()
        .Select(d => new DropDownViewModel() { Id = d.Id, Name = d.Name })
        .OrderBy(x => x.Name)
        .ToList();

      return classes;
    }

    public List<DropDownViewModel> GetTeachGradesForLoggedInUser(int academicYear, string userName)
    {
      var loggedInUser = coreDataService.GetLoggedInUserByUserName(userName);
      var roles = loggedInUser.UserRoles.Select(x => x.RoleId).ToList();

      var response = new List<DropDownViewModel>();

      if(roles.Any(x=>x == (int)SystemRole.Admin) || roles.Any(x => x == (int)SystemRole.Principle) || roles.Any(x => x == (int)SystemRole.VicePrinciple) || roles.Any(x => x == (int)SystemRole.DepartmentHead))
      {
        response.AddRange(GetAllAcademicLevels());
      }
      else if(roles.Any(x => x == (int)SystemRole.LevelHead))
      {
        var assignGrades = db.Grades.Where(x => x.LevelHeadId == loggedInUser.Id)
          .Select(g => new DropDownViewModel() { Id = g.Id, Name = g.Name }).ToList();

        var grades = db.ClassSubjects.AsEnumerable().Where(x => x.Class.AcademicYear == academicYear && !assignGrades.Any(g=>g.Id== x.Class.GradeId) && x.SubjectTeacherId == loggedInUser.Id)
          .Select(a => a.Class.Grade).Distinct().Select(g => new DropDownViewModel() { Id = g.Id, Name = g.Name }).ToList().Union(assignGrades).ToList();

        response.AddRange(grades);
      }
      else if(roles.Any(x => x == (int)SystemRole.Teacher))
      {
        var grades = db.ClassSubjects.Where(x => x.Class.AcademicYear == academicYear && x.SubjectTeacherId == loggedInUser.Id)
          .Select(a => a.Class.Grade).Distinct().Select(g => new DropDownViewModel() { Id = g.Id, Name = g.Name }).ToList();

        response.AddRange(grades);
      }



      return response;
    }

    public List<DropDownViewModel> GetAssignedClassForLoggedInUser(int academicYear, int gradeId, string userName)
    {
      var loggedInUser = coreDataService.GetLoggedInUserByUserName(userName);

      var response = new List<DropDownViewModel>();

      var roles = loggedInUser.UserRoles.Select(x => x.RoleId).ToList();

      var allClasses = db.Grades.Where(g => g.IsActive == true && g.Id == gradeId)
        .SelectMany(cl => cl.Classes).Where(t => t.AcademicYear == academicYear).ToList();

      if (!roles.Contains((int)SystemRole.Teacher))
      {

        var classes = allClasses
          .Distinct()
          .Select(d => new DropDownViewModel() { Id = d.Id, Name = d.Name })
          .OrderBy(x => x.Name)
          .ToList();

        response.AddRange(classes);

      }
      else if (roles.Contains((int)SystemRole.Teacher))
      {
        var classTeachers = new List<DropDownViewModel>();

        classTeachers = allClasses.Where(t => t.ClassTeacherId == loggedInUser.Id)
          .Select(d => new DropDownViewModel() { Id = d.Id, Name = d.Name }).ToList();

        var subjectTechearClasses = allClasses
          .Where(x => x.ClassSubjects.Any(x => x.SubjectTeacherId == loggedInUser.Id)).Where(cl=> !classTeachers.Any(t=> t.Id==cl.Id))
          .Select(d => new DropDownViewModel() { Id = d.Id, Name = d.Name }).ToList().Union(classTeachers).Distinct().ToList();

        response.AddRange(subjectTechearClasses);
      }

      return response;
    }

    public List<DropDownViewModel> GetAssignedClassSubjectForLoggedInUser(int classId, string userName)
    {
      var loggedInUser = coreDataService.GetLoggedInUserByUserName(userName);

      var response = new List<DropDownViewModel>();
      if (classId > 0)
      {
        var roles = loggedInUser.UserRoles.Select(x => x.RoleId).ToList();

        var classObj = db.Classes.FirstOrDefault(x => x.Id == classId);
        var matchingLevelHead = loggedInUser.Grades.FirstOrDefault(x => x.Id == classObj.GradeId);

        var subjects = db.ClassSubjects.Where(x=>x.ClassId==classId).ToList();
        if (roles.Contains((int)SystemRole.Admin) || roles.Contains((int)SystemRole.Principle) || roles.Contains((int)SystemRole.VicePrinciple) || matchingLevelHead !=null|| classObj.ClassTeacherId == loggedInUser.Id)
        {

        }
        else if ((roles.Contains((int)SystemRole.DepartmentHead) || roles.Contains((int)SystemRole.Teacher)))
        {
          subjects = subjects.Where(x => x.SubjectTeacherId == loggedInUser.Id).ToList();
        }

        var assignedSubject = subjects.Distinct()
          .Select(s => new DropDownViewModel() { Id = s.Subject.Id, Name = s.Subject.Name })
          .OrderBy(x => x.Name)
          .ToList();

        response.AddRange(assignedSubject);
      }

      return response;
    }

    public List<DropDownViewModel> GetAssignedClassForTeacher(int academicYear, int gradeId, int teacherId)
    {
      var classes = db.Grades.Where(g => g.IsActive == true && g.Id == gradeId)
        .SelectMany(cl => cl.Classes).Where(t => t.ClassTeacherId == teacherId && t.AcademicYear == academicYear)
        .Distinct()
        .Select(d => new DropDownViewModel() { Id = d.Id, Name = d.Name })
        .OrderBy(x => x.Name)
        .ToList();

      return classes;
    }

    public List<DropDownViewModel> GetAssignedClassSubjectForTeacher(int classId, int teacherId)
    {
      var assignedSubject = db.ClassSubjects.Where(x => x.ClassId == classId && x.SubjectTeacherId == teacherId)
        .Select(s => new DropDownViewModel() { Id = s.Subject.Id, Name = s.Subject.Name })
        .OrderBy(x => x.Name)
        .ToList();

      return assignedSubject;
    }

    public List<CheckBoxViewModel> GetAllSubjects()
    {
      var allSubjects = db.Subjects.Select(x => new CheckBoxViewModel() { Id = x.Id, Name = x.Name }).OrderBy(s => s.Name).ToList();

      return allSubjects;
    }

    public List<DropDownViewModel> GetAllTeachers()
    {
      var teacherId = (int)SystemRole.Teacher;
      var allTeachers = db.Users.Where(x => x.IsActive == true && x.UserRoles.Any(r=>r.RoleId== teacherId)).OrderBy(x=>x.FullName)
        .Select(u => new DropDownViewModel() { Id = u.Id, Name = u.FullName }).ToList();

      return allTeachers;
    }

    public List<DropDownViewModel> GetAllLevelHeads()
    {
      var levelHeadId = (int)SystemRole.LevelHead;
      var allTeachers = db.Users.Where(x => x.IsActive == true && x.UserRoles.Any(r => r.RoleId == levelHeadId)).OrderBy(x => x.FullName)
        .Select(u => new DropDownViewModel() { Id = u.Id, Name = u.FullName }).ToList();

      return allTeachers;
    }

    public List<DropDownViewModel> GetAllDepartmentHeads()
    {
      var departmentHeadId = (int)SystemRole.DepartmentHead;
      var allTeachers = db.Users.Where(x => x.IsActive == true && x.UserRoles.Any(r => r.RoleId == departmentHeadId)).OrderBy(x => x.FullName)
        .Select(u => new DropDownViewModel() { Id = u.Id, Name = u.FullName }).ToList();

      return allTeachers;
    }


    public List<DropDownViewModel> GetAllSystemRoles()
    {
      var response = new List<DropDownViewModel>();

      foreach (SystemRole role in (SystemRole[])Enum.GetValues(typeof(SystemRole)))
      {
        if (role != SystemRole.Parent && role != SystemRole.Student)
        {
          response.Add(new DropDownViewModel() { Id = (int)role, Name = EnumHelper.GetEnumDescription(role) });
        }
      }

      return response;
    }

    public List<DropDownViewModel> GetTeacherAssignedSubject(int gradeId, string userName)
    {
      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      var response = db.ClassSubjects.Where(x => x.SubjectTeacherId == currentUser.Id)
        .Select(x => x.Subject).Distinct()
        .Select(s => new DropDownViewModel() { Id = s.Id, Name = s.Name }).ToList();

      return response;
    }

    public List<DropDownViewModel> GetTeacherAssignedSubjectForSelectedGrade(int gradeId, string userName)
    {
      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      var response = db.ClassSubjects.Where(x => x.SubjectTeacherId == currentUser.Id && x.Class.GradeId==gradeId)
        .Select(x => x.Subject).Distinct()
        .Select(s => new DropDownViewModel() { Id = s.Id, Name = s.Name }).ToList();

      return response;
    }

    public List<DropDownViewModel> GetSubjectClasses(int gradeId, int subjectId,int lessonId, string userName)
    {

      var loggedInUser = coreDataService.GetLoggedInUserByUserName(userName);

      var roles = loggedInUser.UserRoles.Select(x => x.RoleId).ToList();

      var lesson = db.Lessons.FirstOrDefault(x => x.Id == lessonId);

      var currentAcademicYear = db.AcademicYears.FirstOrDefault(x => x.IsCurrentYear == true);

      var classes = db.ClassSubjects.Where(x => x.SubjectId == subjectId &&
      x.Class.GradeId == gradeId &&
      x.Class.AcademicYear == currentAcademicYear.Id &&
      x.SubjectTeacherId == lesson.LessonOwnerId).Select(cl => new DropDownViewModel()
      {
        Id = cl.ClassId,
        Name = cl.Class.Name
      }).ToList();

      return classes;
    }
  }
}
