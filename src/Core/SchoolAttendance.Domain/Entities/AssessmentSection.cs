using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentSection
    {
        public AssessmentSection()
        {
            AssessmentSectionQuestions = new HashSet<AssessmentSectionQuestion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AssessmentId { get; set; }
        public int ContentType { get; set; }
        public string Instructions { get; set; }
        public string SectionContent { get; set; }

        public virtual Assessment Assessment { get; set; }
        public virtual ICollection<AssessmentSectionQuestion> AssessmentSectionQuestions { get; set; }
    }
}
