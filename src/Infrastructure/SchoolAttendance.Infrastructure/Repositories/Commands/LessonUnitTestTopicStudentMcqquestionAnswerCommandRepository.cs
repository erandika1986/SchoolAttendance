﻿using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Infrastructure.Data;
using SchoolAttendance.Infrastructure.Repositories.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Command
{
    public class LessonUnitTestTopicStudentMcqquestionAnswerCommandRepository :
        CommandRepository<LessonUnitTestTopicStudentMcqquestionAnswer>, 
        ILessonUnitTestTopicStudentMcqquestionAnswerCommandRepository
    {
        public LessonUnitTestTopicStudentMcqquestionAnswerCommandRepository(SchoolAttendanceContext context) 
            : base(context)
        {
            
        }
    }
}
