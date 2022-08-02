
using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Interfaces
{
  public interface ISubjectService
  {
    PaginatedItemsViewModel<SubjectViewModel> GetSubjectList(string searchText, int currentPage, int pageSize, bool status);
    Task<ResponseViewModel> SaveSubject(SubjectViewModel vm);
    Task<ResponseViewModel> DeleteSubject(int id);
  }
}
