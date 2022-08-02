using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class BaseQuestionViewModel
  {
    public int Id { get; set; }
    public string Question { get; set; }
    public string QuestionRT { get; set; }
    public int QuestionType { get; set; }
    public int OwnerId { get; set; }
    public int AcdemicYearId { get; set; }
    public int GradeId { get; set; }
    public int SubjectId { get; set; }
  }
}
