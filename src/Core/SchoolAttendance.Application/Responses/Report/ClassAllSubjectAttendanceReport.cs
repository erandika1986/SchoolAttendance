using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{

  public class ClassAllSubjectAttendanceReportContainer
  {
    public ClassAllSubjectAttendanceReportContainer()
    {
      Students = new List<ClassAllSubjectAttendanceReport>();
    }


    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int TotalStudent { get; set; }
    public List<ClassAllSubjectAttendanceReport> Students { get; set; }
  }
  public class ClassAllSubjectAttendanceReport
  {

    public ClassAllSubjectAttendanceReport()
    {
      Days = new List<TimeTableDay>();
    }

    public int StudentId { get; set; }
    public string IndexNo { get; set; }
    public string StudentName { get; set; }
    public List<TimeTableDay> Days { get; set; }
  }

  public class TimeTableDay
  {
    public TimeTableDay()
    {
      Subjects = new List<DaySubject>();
    }

    public int DayId { get; set; }
    public string DayName { get; set; }
    public string Date { get; set; }
    public List<DaySubject> Subjects { get; set; }
  }

  public class DaySubject
  {
    public DateTime StartTime { get; set; }
    public bool NotConducted { get; set; }
    public int SubjectId { get; set; }
    public string Name { get; set; }
    public bool IsPresent { get; set; }
  }
}
