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
    public class QuestionOpenEndedTeacherAnswerCommandRepository :
        CommandRepository<QuestionOpenEndedTeacherAnswer>, 
        IQuestionOpenEndedTeacherAnswerCommandRepository
    {
        public QuestionOpenEndedTeacherAnswerCommandRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }
    }
}
