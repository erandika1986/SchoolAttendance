using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Interfaces
{
  public interface IClassService
  {
    PaginatedItemsViewModel<BasicClassDetailViewModel> GetClassList(string searchText, int currentPage, int pageSize, int academicYearId, int gradeId);
    ClassViewModel GetClassDetail(int gradeId, int classId);
    List<ClassSubjectViewModel> GetClassSubjectsForSelectedGrade(int gradeId);
    Task<ResponseViewModel> SaveClassDetail(ClassViewModel vm);
    ClassMasterDataViewModel GetClassMasterData();
    Task<ResponseViewModel> DeleteClass(int id);


  }
}
