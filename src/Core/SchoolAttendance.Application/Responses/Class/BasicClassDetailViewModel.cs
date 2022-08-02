using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class BasicClassDetailViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string ClassTeacherName { get; set; }
    public int TotalStudentCount { get; set; }

    public int AcademicYearId { get; set; }
    public int GradeId { get; set; }
  }
}
