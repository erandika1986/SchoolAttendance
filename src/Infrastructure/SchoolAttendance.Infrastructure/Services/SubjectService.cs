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
  public class SubjectService : ISubjectService
  {

    private readonly ISchoolAttendanceContext db;
    private readonly ILogger<ISubjectService> logger;
    private readonly IConfiguration config;
    private readonly ICoreDataService coreDataService;
    private readonly IDropDownService dropDownService;

    public SubjectService(ISchoolAttendanceContext db, ILogger<ISubjectService> logger, IConfiguration config, ICoreDataService coreDataService, IDropDownService dropDownService)
    {
      this.db = db;
      this.logger = logger;
      this.config = config;
      this.coreDataService = coreDataService;
      this.dropDownService = dropDownService;
    }

    public async Task<ResponseViewModel> DeleteSubject(int id)
    {
      var response = new ResponseViewModel();

      try
      {
        var subject = db.Subjects.FirstOrDefault(x => x.Id == id);

        subject.IsActive = false;

        foreach (var item in subject.ClassSubjects)
        {
          item.IsActive = false;
        }

        foreach (var item in subject.GradeSubjects)
        {
          item.IsActive = false;
        }

        db.Subjects.Update(subject);

        await db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Subject has been deactivated from the system.";
      }
      catch(Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while deactivating from the system.";
      }


      return response;
    }

    public PaginatedItemsViewModel<SubjectViewModel> GetSubjectList(string searchText, int currentPage, int pageSize,bool status)
    {
      int totalRecordCount = 0;
      double totalPages = 0;
      int totalPageCount = 0;
      var vms = new List<SubjectViewModel>();

      var subjects = db.Subjects
        .Where(x=>x.IsActive==status)
        .OrderBy(s => s.Name);

      if(!string.IsNullOrEmpty(searchText))
      {
        subjects = subjects.Where(x=>x.Name.Contains(searchText))
          .OrderBy(s => s.Name);
      }

      totalRecordCount = subjects.Count();
      totalPages = (double)totalRecordCount / pageSize;
      totalPageCount = (int)Math.Ceiling(totalPages);

      var subjectList = subjects.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

      subjectList.ForEach(s =>
      {
        vms.Add(s.ToVm());

      });

      var container = new PaginatedItemsViewModel<SubjectViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, vms);

      return container;
    }

    public async Task<ResponseViewModel> SaveSubject(SubjectViewModel vm)
    {
      var response = new ResponseViewModel();

      try
      {
        var existingSubject = db.Subjects.FirstOrDefault(x => x.Name.Trim().ToLower() == vm.Name.Trim().ToLower() && x.Id != vm.Id);
        if(existingSubject!=null)
        {
          response.IsSuccess = false;
          response.Message = "Subject name already registered. Please enter different subject name.";

          return response;
        }

        var subject = db.Subjects.FirstOrDefault(x => x.Id == vm.Id);

        if(subject==null)
        {
          subject = new Subject()
          {
            Name = vm.Name,
            Description=vm.Description,
            Medium = vm.Medium,
            IsActive=true
          };

          if(vm.DepartmentHeadId>0)
          {
            subject.DepartmentHeadId = vm.DepartmentHeadId;
          }

          db.Subjects.Add(subject);
          response.Message = "New Subject has been saved successfully.";
        }
        else
        {
          subject.Name = vm.Name;
          subject.Description = vm.Description;
          vm.Medium = vm.Medium;

          if(vm.DepartmentHeadId>0)
          {
            subject.DepartmentHeadId = vm.DepartmentHeadId;
          }

          db.Subjects.Update(subject);

          response.Message = "Subject has been updated successfully.";
        }

        await db.SaveChangesAsync();
        response.IsSuccess = true;
      }
      catch(Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving from the system.";
      }

      return response;
    }
  }
}
