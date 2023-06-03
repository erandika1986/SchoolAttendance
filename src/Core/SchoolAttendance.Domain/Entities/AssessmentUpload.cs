using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentUpload : BaseAuditableEntity
    {
        public int AssessmentId { get; set; }
        public string FileName { get; set; }
        public string SavedFileName { get; set; }
        public string SavedFilePath { get; set; }
        public int VersionNo { get; set; }

        public virtual Assessment Assessment { get; set; }
    }
}
