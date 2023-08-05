using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Extension.Assessment
{
    public static class AssessmentExtension
    {
        public static AssessmentViewModel ToVm(this SchoolAttendance.Domain.Entities.Assessment assessment, AssessmentViewModel vm = null)
        {
            if (assessment == null)
            {
                vm = new AssessmentViewModel();
            }

            vm.Name = assessment.Name;
            vm.AcademicYearId = assessment.AcademicYearId.Value;
            vm.GradeId = assessment.GradeId.Value;
            vm.SubjectId = assessment.SubjectId.Value;
            vm.AssessmentTypeId = assessment.AssessmentTypeId.Value;
            vm.AssessmentConductBy = assessment.AssessmentConductBy.Value;
            vm.Status = assessment.Status;

            vm.ApprovedBy = assessment.ApprovedBy.Value;
            vm.PublishedOn = assessment.PublishedOn.Value.ToLongDateString();
            vm.CompletedOn = assessment.CompletedOn.Value.ToLongDateString();

            vm.AssignedClasses = assessment.AssessmentClasses.Select(x => x.ClassId).ToList();



            return vm;
        }

        public static SchoolAttendance.Domain.Entities.Assessment ToModel(this AssessmentViewModel vm, SchoolAttendance.Domain.Entities.Assessment assessment =null)
        {
            if (assessment == null)
            {
                assessment = new Domain.Entities.Assessment();
            }

            assessment.Name = vm.Name;
            assessment.AcademicYearId = vm.AcademicYearId;
            assessment.GradeId = vm.GradeId;
            assessment.SubjectId = vm.SubjectId;
            assessment.AssessmentTypeId = vm.AssessmentTypeId;
            assessment.AssessmentConductBy = vm.AssessmentConductBy;
            assessment.Status = vm.Status;

            foreach (var classId in vm.AssignedClasses)
            {
                assessment.AssessmentClasses.Add(new AssessmentClass() { ClassId = classId });
            }

            return assessment;
        }
    }
}
