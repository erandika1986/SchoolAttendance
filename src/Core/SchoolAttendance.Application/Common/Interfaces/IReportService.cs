
using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Interfaces
{
  public interface IReportService
  {
    ClassTeacheeDropDownMasterData GetTeacherClassMasterData(string userName);
    DownloadFileModel DownloadClassAttendanceForAllSubjects(AttendanceReportFilter filter);
    DownloadFileModel DownloadClassAttendanceForSelectedSubject(AttendanceReportFilter filter);
    DownloadFileModel GenerateZonalReportForSelectedClass(AttendanceReportFilter filter);
  }
}
