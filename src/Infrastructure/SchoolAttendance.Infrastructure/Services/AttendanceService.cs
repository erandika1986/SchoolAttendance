using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ISchoolAttendanceContext db;
        private readonly ILogger<IAttendanceService> logger;
        private readonly ICoreDataService coreDataService;
        private readonly IDropDownService dropDownService;

        public AttendanceService(ISchoolAttendanceContext db, ILogger<IAttendanceService> logger, ICoreDataService coreDataService, IDropDownService dropDownService)
        {
            this.db = db;
            this.logger = logger;
            this.coreDataService = coreDataService;
            this.dropDownService = dropDownService;
        }

        public AttendanceListFilterMasterData GetAttendanceListTeacherDropdownMasterData(string userName)
        {
            var respones = new AttendanceListFilterMasterData();

            var loggedInUser = coreDataService.GetLoggedInUserByUserName(userName);

            var roles = loggedInUser.UserRoles.Select(x => x.RoleId).ToList();

            if (roles.Contains((int)SystemRole.Admin) || roles.Contains((int)SystemRole.Principle) || roles.Contains((int)SystemRole.VicePrinciple) || roles.Contains((int)SystemRole.DepartmentHead))
            {
                respones.Grades.AddRange(dropDownService.GetAllAcademicLevels());

            }
            else if (roles.Contains((int)SystemRole.LevelHead) || roles.Contains((int)SystemRole.Teacher))
            {
                var levelHeads = new List<DropDownViewModel>();
                if (roles.Contains((int)SystemRole.LevelHead))
                {
                    var userLevelHeads = loggedInUser.Grades.ToList();
                    if (userLevelHeads.Count > 0)
                    {
                        levelHeads = userLevelHeads.Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name }).ToList();
                    }
                }

                var subjectClasses = loggedInUser.ClassSubjects.Select(x => x.Class.Grade).Where(g => !levelHeads.Any(y => y.Id == g.Id)).Distinct().
                      Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name }).OrderBy(x => x.Id).ToList().Union(levelHeads).ToList();

                respones.Grades.AddRange(subjectClasses);


            }

            if (respones.Grades.Count > 0)
            {
                respones.SelectedGradeId = (int)respones.Grades.FirstOrDefault().Id;
            }

            respones.AcademicYears.AddRange(dropDownService.GetAllAcademicYears());

            respones.CurrentAcademicYear = db.AcademicYears.FirstOrDefault(x => x.IsCurrentYear == true).Id;

            return respones;
        }


        public AttendanceFilterViewModel GetStartAndEndTime(AttendanceFilterViewModel filter)
        {
            var date = new DateTime(filter.Year, filter.Month, filter.Day, 0, 0, 0);

            var timeTableRecord = db.ClassSubjectTimeTables.FirstOrDefault(x => x.ClassId == filter.ClassId && x.SubjectId == filter.SubjectId && x.DayId == (int)date.DayOfWeek);

            if (timeTableRecord != null)
            {
                filter.StartHour = timeTableRecord.StartTime.Hour;
                filter.StartMin = timeTableRecord.StartTime.Minute;
                filter.EndHour = timeTableRecord.EndTime.Hour;
                filter.EndMin = timeTableRecord.EndTime.Minute;
            }

            return filter;
        }

        public PaginatedItemsViewModel<BasicAttendanceViewModel> GetMySubjectAttendance(string searchText, int currentPage, int pageSize, int academicYearId, int gradeId, int classId, int subjectId, string userName)
        {
            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var vms = new List<BasicAttendanceViewModel>();

            var loggedInUser = coreDataService.GetLoggedInUserByUserName(userName);

            var roles = loggedInUser.UserRoles.Select(x => x.RoleId).ToList();

            var subjectAttendances = db.SubjectAttendances.AsEnumerable().OrderByDescending(x => x.Date);

            if (roles.Contains((int)SystemRole.Admin) || roles.Contains((int)SystemRole.Principle) || roles.Contains((int)SystemRole.VicePrinciple))
            {

            }
            else if (roles.Contains((int)SystemRole.LevelHead))
            {
                subjectAttendances = subjectAttendances.Where(x => x.Class.GradeId == gradeId).OrderByDescending(x => x.Date);
            }
            else if (roles.Contains((int)SystemRole.DepartmentHead))
            {
                subjectAttendances = subjectAttendances.Where(x => x.SubjectId == subjectId).OrderByDescending(x => x.Date);
            }
            else if (roles.Contains((int)SystemRole.Teacher))
            {
                var classSubjects = db.ClassSubjects.Where(x => x.SubjectTeacherId == loggedInUser.Id).ToList();
                //subjectAttendances = subjectAttendances.Where(x=> mySubjects.Any(y=>y.SubjectId==x.SubjectId && y.ClassId==x.ClassId)).OrderByDescending(x => x.Date);

                subjectAttendances = (from st in subjectAttendances join cs in classSubjects on new { st.ClassId, st.SubjectId } equals new { cs.ClassId, cs.SubjectId } select st).OrderByDescending(x => x.Date);
            }


            if (!string.IsNullOrEmpty(searchText))
            {
                subjectAttendances = subjectAttendances.Where(x => x.Class.Name.Contains(searchText))
                  .OrderByDescending(x => x.Date);
            }

            if (academicYearId > 0)
            {
                subjectAttendances = subjectAttendances.Where(x => x.Class.AcademicYear == academicYearId)
                  .OrderByDescending(x => x.Date);
            }

            if (gradeId > 0)
            {
                subjectAttendances = subjectAttendances.Where(x => x.Class.GradeId == gradeId)
                  .OrderByDescending(x => x.Date);
            }

            if (classId > 0)
            {
                subjectAttendances = subjectAttendances.Where(x => x.ClassId == classId)
                  .OrderByDescending(x => x.Date);
            }

            if (subjectId > 0)
            {
                subjectAttendances = subjectAttendances.Where(x => x.SubjectId == subjectId)
                  .OrderByDescending(x => x.Date);
            }

            totalRecordCount = subjectAttendances.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var attendanceList = subjectAttendances.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            attendanceList.ForEach(a =>
            {
                vms.Add(new BasicAttendanceViewModel()
                {
                    Id = a.Id,
                    ClassName = a.Class.Name,
                    SubjectName = a.Subject.Name,
                    SubjectTeacherName = a.Class.ClassSubjects.FirstOrDefault(x => x.SubjectId == a.SubjectId).SubjectTeacher.FullName,
                    TotalAttendedStudents = a.StudentSubjectAttendances.Where(x => x.IsAttended).Count(),
                    TotalAbsenceStudents = a.StudentSubjectAttendances.Where(x => x.IsAttended == false).Count(),
                    Date = a.Date.ToString("MMM/dd/yyyy"),
                    EndTime = a.EndTime.ToString("hh:mm tt"),
                    StartTime = a.StartTime.ToString("hh:mm tt")
                });

            });

            var container = new PaginatedItemsViewModel<BasicAttendanceViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, vms);

            return container;

        }

        public SubjectAttendaceViewModel GetAttendanceDetailForSubjectClassById(int id, string userName)
        {
            var subjectAttendance = db.SubjectAttendances.FirstOrDefault(x => x.Id == id);

            var response = GetSubjectAttendaceViewModel(subjectAttendance);

            response.StudentsAttendance = response.StudentsAttendance.OrderBy(x => x.StudentName).ToList();

            return response;
        }

        public SubjectAttendaceViewModel GetAttendanceDetailForClassSubject(AttendanceFilterViewModel filter, string userName)
        {
            var response = new SubjectAttendaceViewModel();

            var date = new DateTime(filter.Year, filter.Month, filter.Day, 0, 0, 0);

            var subjectAttendance = db.SubjectAttendances.FirstOrDefault(x => x.ClassId == filter.ClassId && x.SubjectId == filter.SubjectId && x.Date == date);

            if (subjectAttendance == null)
            {
                var timeTableRecord = db.ClassSubjectTimeTables.FirstOrDefault(x => x.ClassId == filter.ClassId && x.SubjectId == filter.SubjectId && x.DayId == (int)date.DayOfWeek);

                var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

                var currentLocalTime = DateTimeHelper.ConvertToLocalTime(DateTime.UtcNow, currentUser.TimeZoneId);

                var endLocalTime = currentLocalTime.AddMinutes(90);

                response.ClassId = filter.ClassId;
                response.GradeId = filter.GradeId;
                response.Day = filter.Day;
                response.Month = filter.Month;
                response.StartHour = timeTableRecord == null ? currentLocalTime.Hour : timeTableRecord.StartTime.Hour;
                response.StartMin = timeTableRecord == null ? currentLocalTime.Minute : timeTableRecord.StartTime.Minute;
                response.EndHour = timeTableRecord == null ? endLocalTime.Hour : timeTableRecord.EndTime.Hour;
                response.EndMin = timeTableRecord == null ? endLocalTime.Minute : timeTableRecord.EndTime.Minute;
                response.SubjectId = filter.SubjectId;
                response.Year = filter.Year;

                if (timeTableRecord != null)
                {
                    response.TimeSlotId = timeTableRecord.Id;
                }
                var studentClass = db.StudentClasses.Where(x => x.ClassId == filter.ClassId && x.IsActive == true).ToList();

                foreach (var item in studentClass)
                {
                    var student = new StudentAttendanceViewModel()
                    {
                        IsPresent = false,
                        StudentId = item.StudentId,
                        IndexNo = item.Student.Username,
                        Gender = item.Student.Gender,
                        ImagePath = item.Student.Gender == "M" ? "assets/images/student-m.png" : "assets/images/student-f.png",
                        StudentName = item.Student.FullName
                    };

                    response.StudentsAttendance.Add(student);
                }
            }
            else
            {
                response = GetSubjectAttendaceViewModel(subjectAttendance);
            }

            response.StudentsAttendance = response.StudentsAttendance.OrderBy(x => x.StudentName).ToList();

            return response;
        }

        public async Task<ResponseViewModel> SaveAttendanceDetailForClassSubject(SubjectAttendaceViewModel vm)
        {
            var response = new ResponseViewModel();

            try
            {
                var subjectAttendance = db.SubjectAttendances.FirstOrDefault(x => x.Id == vm.Id);

                if (subjectAttendance == null)
                {
                    subjectAttendance = new SubjectAttendance()
                    {
                        ClassId = vm.ClassId,
                        Date = new DateTime(vm.Year, vm.Month, vm.Day, 0, 0, 0),
                        EndTime = new DateTime(vm.Year, vm.Month, vm.Day, vm.EndHour, vm.EndMin, 0),
                        StartTime = new DateTime(vm.Year, vm.Month, vm.Day, vm.StartHour, vm.StartMin, 0),
                        SubjectId = vm.SubjectId,
                        IsExtraClass = vm.IsExtraClass,
                        LessonDetails = vm.LessonDetails,
                        IsReScheduleClass = false,
                        UsedSoftwareName = vm.SoftwareName,
                        ActualEnteredDate = DateTime.UtcNow
                    };

                    if (vm.TimeSlotId > 0)
                    {
                        subjectAttendance.TimeSlotId = vm.TimeSlotId;
                    }

                    subjectAttendance.StudentSubjectAttendances = new HashSet<StudentSubjectAttendance>();

                    vm.StudentsAttendance.ForEach(st =>
                    {
                        subjectAttendance.StudentSubjectAttendances.Add(new StudentSubjectAttendance()
                        {
                            IsAttended = st.IsPresent,
                            StudentId = st.StudentId,
                        });

                    });

                    db.SubjectAttendances.Add(subjectAttendance);
                    response.Message = "Attendance details added successfully.";
                }
                else
                {
                    subjectAttendance.Date = new DateTime(vm.Year, vm.Month, vm.Day, 0, 0, 0);
                    subjectAttendance.EndTime = new DateTime(vm.Year, vm.Month, vm.Day, vm.EndHour, vm.EndMin, 0);
                    subjectAttendance.StartTime = new DateTime(vm.Year, vm.Month, vm.Day, vm.StartHour, vm.StartMin, 0);
                    subjectAttendance.LessonDetails = vm.LessonDetails;
                    subjectAttendance.IsExtraClass = vm.IsExtraClass;
                    subjectAttendance.UsedSoftwareName = vm.SoftwareName;
                    //subjectAttendance.SubjectId = vm.SubjectId;

                    db.SubjectAttendances.Update(subjectAttendance);

                    foreach (var item in vm.StudentsAttendance)
                    {
                        var student = subjectAttendance.StudentSubjectAttendances.FirstOrDefault(x => x.StudentId == item.StudentId);
                        student.IsAttended = item.IsPresent;

                        db.StudentSubjectAttendances.Update(student);

                    }

                    response.Message = "Attendance details updated successfully.";
                }

                response.IsSuccess = true;


                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "An Exception has been occuured while saving the record. Please try again.";

            }

            return response;
        }

        private SubjectAttendaceViewModel GetSubjectAttendaceViewModel(SubjectAttendance subjectAttendance)
        {
            var response = new SubjectAttendaceViewModel();

            response.Id = subjectAttendance.Id;
            response.ClassId = subjectAttendance.ClassId;
            response.GradeId = subjectAttendance.Class.GradeId;
            response.Day = subjectAttendance.Date.Day;
            response.Month = subjectAttendance.Date.Month;
            response.StartHour = subjectAttendance.StartTime.Hour;
            response.StartMin = subjectAttendance.StartTime.Minute;
            response.EndHour = subjectAttendance.EndTime.Hour;
            response.EndMin = subjectAttendance.EndTime.Minute;
            response.SubjectId = subjectAttendance.SubjectId;
            response.Year = subjectAttendance.Date.Year;
            response.LessonDetails = subjectAttendance.LessonDetails;
            response.SoftwareName = subjectAttendance.UsedSoftwareName;

            response.TimeSlotId = subjectAttendance.TimeSlotId.HasValue ? subjectAttendance.TimeSlotId.Value : 0;

            var studentList = subjectAttendance.StudentSubjectAttendances.OrderBy(x => x.Student.FullName).ToList();

            foreach (var item in studentList)
            {
                var student = new StudentAttendanceViewModel()
                {
                    IsPresent = item.IsAttended,
                    StudentId = item.StudentId,
                    Gender = item.Student.Gender,
                    ImagePath = item.Student.Gender == "M" ? "assets/images/student-m.png" : "assets/images/student-f.png",
                    IndexNo = item.Student.Username,
                    StudentName = item.Student.FullName
                };

                response.StudentsAttendance.Add(student);
            }

            return response;
        }

    }
}
