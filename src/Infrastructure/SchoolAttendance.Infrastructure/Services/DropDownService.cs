using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Helpers;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
    public class DropDownService : IDropDownService
    {

        private readonly ISchoolAttendanceContext _db;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<IDropDownService> _logger;

        public DropDownService(
            ISchoolAttendanceContext db,
            IUserQueryRepository userQueryRepository,
            ICurrentUserService currentUserService,
            ILogger<DropDownService> logger)
        {
            this._db = db;
            this._userQueryRepository = userQueryRepository;
            this._currentUserService = currentUserService;
            this._logger = logger;
        }

        public List<DropDownViewModel> GetAllAcademicYears()
        {
            return _db.AcademicYears
              .Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name })
              .OrderBy(x => x.Name)
              .ToList();
        }

        public List<DropDownViewModel> GetAllAcademicLevels()
        {
            return _db.Grades.Where(g => g.IsActive == true)
              .Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name })
              .OrderBy(x => x.Id)
              .ToList();
        }

        public List<DropDownViewModel> GetClassesForSelectedGrade(int academicYearId, int gradeId)
        {
            var classes = _db.Grades.Where(g => g.IsActive == true && g.Id == gradeId)
              .SelectMany(cl => cl.Classes).Where(t => t.AcademicYear == academicYearId)
              .Distinct()
              .Select(d => new DropDownViewModel() { Id = d.Id, Name = d.Name })
              .OrderBy(x => x.Name)
              .ToList();

            return classes;
        }

        public async Task<List<DropDownViewModel>> GetTeachGradesForLoggedInUser(int academicYear, CancellationToken cancellationToken)
        {
            var loggedInUser = await _userQueryRepository
                .GetById(_currentUserService.UserId.Value, cancellationToken);

            var roles = loggedInUser
                .UserRoles
                .Select(x => x.RoleId)
                .ToList();

            var response = new List<DropDownViewModel>();

            if (roles.Any(x => x == (int)SystemRole.Admin) || 
                roles.Any(x => x == (int)SystemRole.Principle) ||
                roles.Any(x => x == (int)SystemRole.VicePrinciple) || 
                roles.Any(x => x == (int)SystemRole.DepartmentHead))
            {
                response.AddRange(GetAllAcademicLevels());
            }
            else if (roles.Any(x => x == (int)SystemRole.LevelHead))
            {
                var assignGrades = _db.Grades
                    .Where(x => x.LevelHeadId == loggedInUser.Id)
                    .Select(g => 
                        new DropDownViewModel() 
                        { 
                            Id = g.Id, 
                            Name = g.Name 
                        })
                    .ToList();

                var grades = _db
                    .ClassSubjects.AsEnumerable()
                    .Where(x => 
                                x.Class.AcademicYear == academicYear && 
                                !assignGrades.Any(g => g.Id == x.Class.GradeId) && 
                                x.SubjectTeacherId == loggedInUser.Id)
                  .Select(a => a.Class.Grade)
                  .Distinct()
                  .Select(g => 
                    new DropDownViewModel() 
                    { 
                        Id = g.Id, 
                        Name = g.Name 
                    })
                  .ToList()
                  .Union(assignGrades)
                  .ToList();

                response.AddRange(grades);
            }
            else if (roles.Any(x => x == (int)SystemRole.Teacher))
            {
                var grades = _db
                    .ClassSubjects
                    .Where(x => 
                        x.Class.AcademicYear == academicYear && 
                        x.SubjectTeacherId == loggedInUser.Id)
                    .Select(a => a.Class.Grade)
                    .Distinct()
                    .Select(g => 
                        new DropDownViewModel() 
                        { 
                            Id = g.Id, 
                            Name = g.Name })
                    .ToList();

                response.AddRange(grades);
            }



            return response;
        }

        public async Task<List<DropDownViewModel>> GetAssignedClassForLoggedInUser(int academicYear, int gradeId, CancellationToken cancellationToken)
        {
            var response = new List<DropDownViewModel>();

            var loggedInUser = await _userQueryRepository
                .GetById(_currentUserService.UserId.Value, cancellationToken);


            var roles = loggedInUser
                .UserRoles
                .Select(x => x.RoleId)
                .ToList();

            var allClasses = _db
                .Grades
                .Where(g => g.IsActive == true && g.Id == gradeId)
                .SelectMany(cl => cl.Classes)
                .Where(t => t.AcademicYear == academicYear)
                .ToList();

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

                var classTeachers = allClasses
                    .Where(t => t.ClassTeacherId == loggedInUser.Id)
                    .Select(d => 
                        new DropDownViewModel() 
                        { 
                            Id = d.Id, 
                            Name = d.Name 
                        })
                    .ToList();

                var subjectTechearClasses = allClasses
                    .Where(x => 
                                x.ClassSubjects.Any(x => x.SubjectTeacherId == loggedInUser.Id))
                                .Where(cl => !classTeachers.Any(t => t.Id == cl.Id))
                    .Select(d => 
                            new DropDownViewModel() 
                            { 
                                Id = d.Id, 
                                Name = d.Name 
                            })
                    .ToList()
                    .Union(classTeachers)
                    .Distinct()
                    .ToList();

                response.AddRange(subjectTechearClasses);
            }

            return response;
        }

        public async Task<List<DropDownViewModel>> GetAssignedClassSubjectForLoggedInUser(int classId, CancellationToken cancellationToken)
        {
            var loggedInUser = await _userQueryRepository
                .GetById(_currentUserService.UserId.Value, cancellationToken);

            var response = new List<DropDownViewModel>();
            if (classId > 0)
            {
                var roles = loggedInUser
                    .UserRoles
                    .Select(x => x.RoleId)
                    .ToList();

                var classObj = _db
                    .Classes
                    .FirstOrDefault(x => x.Id == classId);

                var matchingLevelHead = loggedInUser
                    .Grades
                    .FirstOrDefault(x => x.Id == classObj.GradeId);

                var subjects = _db
                    .ClassSubjects
                    .Where(x => x.ClassId == classId)
                    .ToList();

                if (roles.Contains((int)SystemRole.Admin) || 
                    roles.Contains((int)SystemRole.Principle) || 
                    roles.Contains((int)SystemRole.VicePrinciple) || 
                    matchingLevelHead != null || 
                    classObj.ClassTeacherId == loggedInUser.Id
                    )
                {

                }
                else if (
                    (roles.Contains((int)SystemRole.DepartmentHead) || 
                    roles.Contains((int)SystemRole.Teacher)))
                {
                    subjects = subjects
                        .Where(x => x.SubjectTeacherId == loggedInUser.Id)
                        .ToList();
                }

                var assignedSubject = subjects.Distinct()
                  .Select(s => new DropDownViewModel() 
                    { 
                      Id = s.Subject.Id, 
                      Name = s.Subject.Name 
                  })
                  .OrderBy(x => x.Name)
                  .ToList();

                response.AddRange(assignedSubject);
            }

            return response;
        }

        public List<DropDownViewModel> GetAssignedClassForTeacher(int academicYear, int gradeId, int teacherId)
        {
            var classes = _db.Grades.Where(g => g.IsActive == true && g.Id == gradeId)
              .SelectMany(cl => cl.Classes).Where(t => t.ClassTeacherId == teacherId && t.AcademicYear == academicYear)
              .Distinct()
              .Select(d => new DropDownViewModel() { Id = d.Id, Name = d.Name })
              .OrderBy(x => x.Name)
              .ToList();

            return classes;
        }

        public List<DropDownViewModel> GetAssignedClassSubjectForTeacher(int classId, int teacherId)
        {
            var assignedSubject = _db.ClassSubjects.Where(x => x.ClassId == classId && x.SubjectTeacherId == teacherId)
              .Select(s => new DropDownViewModel() { Id = s.Subject.Id, Name = s.Subject.Name })
              .OrderBy(x => x.Name)
              .ToList();

            return assignedSubject;
        }

        public List<CheckBoxViewModel> GetAllSubjects()
        {
            var allSubjects = _db.Subjects.Select(x => new CheckBoxViewModel() { Id = x.Id, Name = x.Name }).OrderBy(s => s.Name).ToList();

            return allSubjects;
        }

        public List<DropDownViewModel> GetAllTeachers()
        {
            var teacherId = (int)SystemRole.Teacher;
            var allTeachers = _db.Users.Where(x => x.IsActive == true && x.UserRoles.Any(r => r.RoleId == teacherId)).OrderBy(x => x.FullName)
              .Select(u => new DropDownViewModel() { Id = u.Id, Name = u.FullName }).ToList();

            return allTeachers;
        }

        public List<DropDownViewModel> GetAllLevelHeads()
        {
            var levelHeadId = (int)SystemRole.LevelHead;
            var allTeachers = _db.Users.Where(x => x.IsActive == true && x.UserRoles.Any(r => r.RoleId == levelHeadId)).OrderBy(x => x.FullName)
              .Select(u => new DropDownViewModel() { Id = u.Id, Name = u.FullName }).ToList();

            return allTeachers;
        }

        public List<DropDownViewModel> GetAllDepartmentHeads()
        {
            var departmentHeadId = (int)SystemRole.DepartmentHead;
            var allTeachers = _db.Users.Where(x => x.IsActive == true && x.UserRoles.Any(r => r.RoleId == departmentHeadId)).OrderBy(x => x.FullName)
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

        public List<DropDownViewModel> GetTeacherAssignedSubject(int gradeId)
        {
            var response = _db.ClassSubjects.Where(x => x.SubjectTeacherId == _currentUserService.UserId.Value)
              .Select(x => x.Subject).Distinct()
              .Select(s => new DropDownViewModel() { Id = s.Id, Name = s.Name }).ToList();

            return response;
        }

        public List<DropDownViewModel> GetTeacherAssignedSubjectForSelectedGrade(int gradeId)
        {
            var response = _db.ClassSubjects.Where(x => x.SubjectTeacherId == _currentUserService.UserId.Value && x.Class.GradeId == gradeId)
              .Select(x => x.Subject).Distinct()
              .Select(s => new DropDownViewModel() { Id = s.Id, Name = s.Name }).ToList();

            return response;
        }

        public async Task<List<DropDownViewModel>> GetSubjectClasses(int gradeId, int subjectId, int lessonId, CancellationToken cancellationToken)
        {

            var loggedInUser = await _userQueryRepository
                .GetById(_currentUserService.UserId.Value, cancellationToken);

            var roles = loggedInUser
                .UserRoles
                .Select(x => x.RoleId)
                .ToList();

            var lesson = _db
                .Lessons
                .FirstOrDefault(x => x.Id == lessonId);

            var currentAcademicYear = _db
                .AcademicYears
                .FirstOrDefault(x => x.IsCurrentYear == true);

            var classes = _db
                .ClassSubjects
                .Where(x => 
                        x.SubjectId == subjectId &&
                        x.Class.GradeId == gradeId &&
                        x.Class.AcademicYear == currentAcademicYear.Id &&
                        x.SubjectTeacherId == lesson.LessonOwnerId)
                .Select(cl => new DropDownViewModel()
                {
                    Id = cl.ClassId,
                    Name = cl.Class.Name
                })
                .ToList();

            return classes;
        }
    }
}
