using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
    public class AttendanceService : IAttendanceService
    {
        public AttendanceService()
        {

        }

        public SubjectAttendaceViewModel GetSubjectAttendaceViewModel(SubjectAttendance subjectAttendance)
        {
            var response = new SubjectAttendaceViewModel();

            response.Id = subjectAttendance.Id;
            response.ClassId = subjectAttendance.ClassId;
            response.GradeId = subjectAttendance.Class.GradeId;
            response.Day = subjectAttendance.Date.Day;
            response.Month = subjectAttendance.Date.Month;
            response.StartHour = subjectAttendance.StartTime.Hour;
            response.StartMin = subjectAttendance.StartTime.Minute;
            response.EndHour = subjectAttendance.EndTime.Hour;
            response.EndMin = subjectAttendance.EndTime.Minute;
            response.SubjectId = subjectAttendance.SubjectId;
            response.Year = subjectAttendance.Date.Year;
            response.LessonDetails = subjectAttendance.LessonDetails;
            response.SoftwareName = subjectAttendance.UsedSoftwareName;

            response.TimeSlotId = subjectAttendance.TimeSlotId.HasValue ? subjectAttendance.TimeSlotId.Value : 0;

            var studentList = subjectAttendance.StudentSubjectAttendances.OrderBy(x => x.Student.FullName).ToList();

            foreach (var item in studentList)
            {
                var student = new StudentAttendanceViewModel()
                {
                    IsPresent = item.IsAttended,
                    StudentId = item.StudentId,
                    Gender = item.Student.Gender,
                    ImagePath = item.Student.Gender == "M" ? "assets/images/student-m.png" : "assets/images/student-f.png",
                    IndexNo = item.Student.Username,
                    StudentName = item.Student.FullName
                };

                response.StudentsAttendance.Add(student);
            }

            return response;
        }

    }
}
