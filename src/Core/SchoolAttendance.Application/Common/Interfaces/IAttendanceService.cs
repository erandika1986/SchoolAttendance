using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Interfaces
{
	public interface IAttendanceService
	{
    AttendanceFilterViewModel GetStartAndEndTime(AttendanceFilterViewModel filter);
    AttendanceListFilterMasterData GetAttendanceListTeacherDropdownMasterData(string userName);
    PaginatedItemsViewModel<BasicAttendanceViewModel> GetMySubjectAttendance(string searchText, int currentPage, int pageSize, int academicYearId, int gradeId, int classId, int subjectId, string userName);
    SubjectAttendaceViewModel GetAttendanceDetailForSubjectClassById(int id, string userName);
    SubjectAttendaceViewModel GetAttendanceDetailForClassSubject(AttendanceFilterViewModel filter, string userName);
    Task<ResponseViewModel> SaveAttendanceDetailForClassSubject(SubjectAttendaceViewModel vm);
  }
}
