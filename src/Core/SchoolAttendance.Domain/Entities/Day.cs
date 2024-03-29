﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class Day : BaseEntity
    {
        public Day()
        {
            ClassSubjectTimeTables = new HashSet<ClassSubjectTimeTable>();
        }

        public string Name { get; set; }

        public virtual ICollection<ClassSubjectTimeTable> ClassSubjectTimeTables { get; set; }
    }
}
