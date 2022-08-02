using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class LessonUnitTestTopicQuestionViewModel
  {
    public int Id { get; set; }
    public int SequenceNo { get; set; }
    public decimal Score { get; set; }

    public int AcademicYearId { get; set; }
    public int GradeId { get; set; }
    public int SubjectId { get; set; }

    public MCQQuestionViewModel MCQQuestion { get; set; }
    public OpenEndedQuestionViewModel OpenEndedQuestion { get; set; }

  }
}
