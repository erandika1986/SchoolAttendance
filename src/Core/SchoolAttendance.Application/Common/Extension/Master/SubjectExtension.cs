
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class SubjectExtension
    {
        public static SubjectViewModel ToVm(this Subject model, SubjectViewModel vm = null)
        {
            if (vm == null)
            {
                vm = new SubjectViewModel();
            }

            vm.Id = model.Id;
            vm.DepartmentHeadId = model.DepartmentHeadId.HasValue ? model.DepartmentHeadId.Value : 0;
            vm.DepartmentHeadName = model.DepartmentHeadId.HasValue ? model.DepartmentHead.FullName : "";
            vm.Description = model.Description;
            vm.Medium = model.Medium;
            vm.Name = model.Name;
            vm.IsBasketSubject = model.IsBasketSubject;
            vm.IsParentSubject = model.IsParentSubject;
            if(vm.IsBasketSubject)
            {
                vm.ParentSubjectName = model.ParentSubject.Name;
                vm.ParentSubjectId = model.ParentSubject.Id;
            }
            else
            {
                vm.ParentSubjectId = 0;
            }

            vm.AssignedGrades = string.Join(",", model.GradeSubjects.Select(x => x.Grade.Name).ToList());

            return vm;
        }
    }
}
