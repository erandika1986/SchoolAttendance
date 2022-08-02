using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class AssessmentSectionViewModel
  {
    public AssessmentSectionViewModel()
    {
      Questions = new List<AssessmentSectionQuestionViewModel>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int ContentType { get; set; }
    public string Instruction { get; set; }
    public string SectionContent { get; set; }
    public List<AssessmentSectionQuestionViewModel> Questions { get; set; }
  }
}
