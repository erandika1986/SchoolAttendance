using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class SubjectAttendaceViewModel
  {

    public SubjectAttendaceViewModel()
    {
      StudentsAttendance = new List<StudentAttendanceViewModel>();
    }

    public int Id { get; set; }
    public int GradeId { get; set; }
    public int ClassId { get; set; }
    public int SubjectId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public int StartHour { get; set; }
    public int StartMin { get; set; }
    public int EndHour { get; set; }
    public int EndMin { get; set; }

    public bool IsExtraClass { get; set; }
    public string LessonDetails { get; set; }
    public string  SoftwareName { get; set; }
    public int TimeSlotId { get; set; }

    public List<StudentAttendanceViewModel> StudentsAttendance { get; set; }
  }

  public class StudentAttendanceViewModel
  {
    public int StudentId { get; set; }
    public string IndexNo { get; set; }
    public string StudentName { get; set; }
    public string Gender { get; set; }
    public string ImagePath { get; set; }
    public bool IsPresent { get; set; }
  }
}
