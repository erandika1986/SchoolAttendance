﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentType : BaseEntity
    {
        public AssessmentType()
        {
            Assessments = new HashSet<Assessment>();
        }

        public string Name { get; set; }
        public AssessmentLevel? AssessmentLevel { get; set; }

        public virtual ICollection<Assessment> Assessments { get; set; }
    }
}
