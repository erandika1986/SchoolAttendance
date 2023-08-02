using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicLevels;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicYears;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllTeachers;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
    public class ClassService : IClassService
    {
        private readonly ISchoolAttendanceContext db;
        private readonly ILogger<IClassService> logger;
        private readonly IConfiguration config;
        private readonly ICoreDataService coreDataService;
        private readonly IMediator _mediator;

        public ClassService(
            ISchoolAttendanceContext db,
            ILogger<IClassService> logger,
            IConfiguration config,
            ICoreDataService coreDataService,
            IMediator mediator)
        {
            this.db = db;
            this.logger = logger;
            this.config = config;
            this.coreDataService = coreDataService;
            this._mediator = mediator;
        }
        public async Task<ResponseViewModel> DeleteClass(int id)
        {
            var response = new ResponseViewModel();
            try
            {
                var matchingClass = db.Classes.FirstOrDefault(x => x.Id == id);

                db.Classes.Remove(matchingClass);

                await db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Class has been deleted successfully.";

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Class deletion failed since clss already used with other tables";
            }

            return response;

        }

        public ClassViewModel GetClassDetail(int gradeId, int classId)
        {
            var response = new ClassViewModel();

            var classObject = db.Classes.FirstOrDefault(x => x.Id == classId);

            if (classObject != null)
            {
                response.Id = classObject.Id;
                response.Name = classObject.Name;
                response.SelectedAcademicYearId = classObject.AcademicYear;
                response.SelectedClassTeacherId = classObject.ClassTeacherId;
                response.SelectedGradeId = classObject.GradeId;
            }

            var subjects = db.GradeSubjects.Where(x => x.GradeId == gradeId && x.IsActive == true).OrderBy(x => x.Subject.Name).ToList();

            foreach (var item in subjects)
            {
                var classSjectsVm = new ClassSubjectViewModel
                {
                    ClassId = classObject != null ? classObject.Id : 0,
                    SubjectId = item.SubjectId,
                    SubjectName = item.Subject.Name,
                    //SubjectTeacherId =
                    //      classObject != null ?
                    //      classObject.ClassSubjects.FirstOrDefault(x=>x.SubjectId==item.SubjectId)
                    //      .SubjectTeacherId.HasValue? classObject.ClassSubjects.
                    //      FirstOrDefault(x => x.SubjectId == item.SubjectId)
                    //      .SubjectTeacherId.Value:0:0,

                    SubjectTeachers = db.SubjectTeachers
                  .Where(x => x.SubjectId == item.SubjectId)
                  .Select(t => new DropDownViewModel() { Id = t.TeacherId, Name = t.Teacher.FullName })
                  .OrderBy(x => x.Name)
                  .ToList()
                };

                if (classObject != null)
                {
                    var classSubject = classObject.ClassSubjects.FirstOrDefault(x => x.SubjectId == item.SubjectId);

                    if (classSubject != null)
                    {
                        classSjectsVm.SubjectTeacherId = classSubject.SubjectTeacherId.HasValue ? classSubject.SubjectTeacherId.Value : 0;
                    }

                }

                response.ClassSubjects.Add(classSjectsVm); ;
            }

            return response;
        }

        public List<ClassSubjectViewModel> GetClassSubjectsForSelectedGrade(int gradeId)
        {
            var response = new List<ClassSubjectViewModel>();

            var subjects = db.GradeSubjects.Where(x => x.GradeId == gradeId && x.IsActive == true).OrderBy(x => x.Subject.Name).ToList();

            foreach (var item in subjects)
            {
                var classSjectsVm = new ClassSubjectViewModel
                {
                    ClassId = 0,
                    SubjectId = item.SubjectId,
                    SubjectName = item.Subject.Name,
                    SubjectTeacherId = 0,
                    SubjectTeachers = db.SubjectTeachers
                  .Where(x => x.SubjectId == item.SubjectId)
                  .Select(t => new DropDownViewModel() { Id = t.TeacherId, Name = t.Teacher.FullName })
                  .OrderBy(x => x.Name)
                  .ToList()
                };



                response.Add(classSjectsVm); ;
            }

            return response;
        }

        public async Task<ClassMasterDataViewModel> GetClassMasterData()
        {
            var response = new ClassMasterDataViewModel();

            var allTeachers = await _mediator.Send(new GetAllTeachersQuery());

            response.AcademicYears = db.AcademicYears.Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name }).OrderBy(x => x.Id).ToList();
            response.CurrentAcademicYearId = db.AcademicYears.FirstOrDefault(x => x.IsCurrentYear).Id;
            response.Grades = db.Grades.Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name }).ToList();
            response.AllTeachers.AddRange(allTeachers);

            return response;
        }

        public PaginatedItemsViewModel<BasicClassDetailViewModel> GetClassList(string searchText, int currentPage, int pageSize, int academicYearId, int gradeId)
        {
            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var vms = new List<BasicClassDetailViewModel>();

            var classes = db.Classes.Where(x => x.AcademicYear == academicYearId).OrderBy(cl => cl.Name);

            if (gradeId > 0)
            {
                classes = classes.Where(x => x.GradeId == gradeId).OrderBy(cl => cl.Name);
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                classes = classes.Where(x => x.Name.Contains(searchText)).OrderBy(cl => cl.Name);
            }

            totalRecordCount = classes.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var classList = classes.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            classList.ForEach(u =>
            {
                vms.Add(u.ToVm());

            });


            var container = new PaginatedItemsViewModel<BasicClassDetailViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, vms);

            return container;
        }

        public async Task<ResponseViewModel> SaveClassDetail(ClassViewModel vm)
        {
            var response = new ResponseViewModel();
            try
            {
                var classNameExsits = db.Classes
                  .FirstOrDefault(cl => cl.Name.Trim().ToLower() == vm.Name.Trim().ToLower() && cl.Id != vm.Id && cl.AcademicYear == vm.SelectedAcademicYearId && cl.GradeId == vm.SelectedGradeId);

                if (classNameExsits != null)
                {
                    response.IsSuccess = false;
                    response.Message = "Provided class name has already registerd. Please try with new class name.";

                    return response;
                }

                var matchingClass = db.Classes.FirstOrDefault(x => x.Id == vm.Id);

                if (matchingClass == null)
                {

                    matchingClass = new Class()
                    {
                        Name = vm.Name,
                        ClassTeacherId = vm.SelectedClassTeacherId,
                        GradeId = vm.SelectedGradeId,
                        AcademicYear = vm.SelectedAcademicYearId
                    };

                    matchingClass.ClassSubjects = new HashSet<ClassSubject>();

                    vm.ClassSubjects.ForEach(x =>
                    {
                        matchingClass.ClassSubjects.Add(new ClassSubject
                        {
                            SubjectId = x.SubjectId,
                            SubjectTeacherId = x.SubjectTeacherId
                        });
                    });

                    db.Classes.Add(matchingClass);

                    response.Message = "New class has been added successfully.";
                }
                else
                {
                    matchingClass.Name = vm.Name;
                    matchingClass.ClassTeacherId = vm.SelectedClassTeacherId;

                    db.Classes.Update(matchingClass);

                    vm.ClassSubjects.ForEach(x =>
                    {
                        var classSubject = matchingClass.ClassSubjects.FirstOrDefault(s => s.SubjectId == x.SubjectId);
                        if (classSubject == null)
                        {
                            matchingClass.ClassSubjects.Add(new ClassSubject
                            {
                                SubjectId = x.SubjectId,
                                SubjectTeacherId = x.SubjectTeacherId
                            });
                        }
                        else
                        {
                            classSubject.SubjectTeacherId = x.SubjectTeacherId;

                            db.ClassSubjects.Update(classSubject);
                        }

                    });

                    response.Message = "Existing class has been updated successfully.";
                }

                await db.SaveChangesAsync();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }

            return response;
        }
    }
}
