using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
  public class GradeService : IGradeService
  {
    private readonly ISchoolAttendanceContext db;
    private readonly ILogger<IGradeService> logger;
    private readonly IConfiguration config;
    private readonly ICoreDataService coreDataService;
    private readonly IDropDownService dropDownService;


    public GradeService(ISchoolAttendanceContext db, ILogger<IGradeService> logger, IConfiguration config, ICoreDataService coreDataService, IDropDownService dropDownService)
    {
      this.db = db;
      this.logger = logger;
      this.config = config;
      this.coreDataService = coreDataService;
      this.dropDownService = dropDownService;
    }
    public List<GradeViewModel> GetGradeList()
    {
      var response = new List<GradeViewModel>();

      var grades = db.Grades.Where(x => x.IsActive == true).ToList();

      foreach (var item in grades)
      {
        response.Add(new GradeViewModel()
        {
          GradeSubjectsText = string.Join(",", item.GradeSubjects.Select(x=>x.Subject.Name).ToList()),
          GradeSubjects = item.GradeSubjects.Select(x=>x.SubjectId).ToList(),
          Id=item.Id,
          LevelHeadId = item.LevelHeadId.HasValue?item.LevelHeadId.Value:0,
          LevelHeadName = item.LevelHeadId.HasValue?item.LevelHead.FullName:string.Empty,
          Name= item.Name
        });
      }

      return response;
    }

    public async Task<ResponseViewModel> SaveGradeDetail(GradeViewModel vm)
    {
      var response = new ResponseViewModel();
      try
      {
        var grade = db.Grades.FirstOrDefault(x => x.Id == vm.Id);

        grade.LevelHeadId = vm.LevelHeadId;

        var currentYear = db.AcademicYears.FirstOrDefault(x => x.IsCurrentYear);

        var gradeAllClasses = grade.Classes.Where(x => x.AcademicYear == currentYear.Id).ToList();

        var savedAssignedSubject = grade.GradeSubjects.Select(x => x.Subject).ToList();

        var deletedSubjects = (from d in savedAssignedSubject where !vm.GradeSubjects.Any(x => x == d.Id) select d).ToList();

        var assignedSubjects = gradeAllClasses.SelectMany(x => x.ClassSubjects).Select(x => x.SubjectId).Distinct().ToList();

        var alreadyAssignedDeletedSubject = assignedSubjects.Intersect(deletedSubjects.Select(x => x.Id).ToList()).ToList();

        if(alreadyAssignedDeletedSubject.Count>0)
        {
          response.IsSuccess = false;
          response.Message = "You are not allow to delete the subjects after they have assigned to the classes. Please remove";

          return response;
        }
        else
        {
          foreach (var item in deletedSubjects)
          {
            var gradeSubject = grade.GradeSubjects.FirstOrDefault(x => x.SubjectId == item.Id);

            db.GradeSubjects.Remove(gradeSubject);
          }
        }

        var newlyAddedSubjects = (from ins in vm.GradeSubjects where !savedAssignedSubject.Any(x => x.Id == ins) select ins).ToList();

        foreach (var item in newlyAddedSubjects)
        {
          grade.GradeSubjects.Add(new GradeSubject() { SubjectId = item });

          foreach (var cl in gradeAllClasses)
          {
            cl.ClassSubjects.Add(new ClassSubject() { IsActive = true, SubjectId = item });
          }
        }

        await db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Grade subject has been updated successfully.";

      }
      catch(Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while updating the grade subject.";
      }

      return response;
    }
  }
}
