
using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Interfaces
{
    public interface IDropDownService
    {
        List<DropDownViewModel> GetAllAcademicYears();
        List<DropDownViewModel> GetAllAcademicLevels();
        List<DropDownViewModel> GetClassesForSelectedGrade(int academicYearId, int gradeId);
        Task<List<DropDownViewModel>> GetAssignedClassForLoggedInUser(int academicYear, int gradeId, CancellationToken cancellationToken);
        Task<List<DropDownViewModel>> GetAssignedClassSubjectForLoggedInUser(int classId, CancellationToken cancellationToken);
        List<DropDownViewModel> GetAssignedClassForTeacher(int academicYear, int gradeId, int teacherId);
        List<DropDownViewModel> GetAssignedClassSubjectForTeacher(int classId, int teacherId);
        Task<List<DropDownViewModel>> GetTeachGradesForLoggedInUser(int academicYear, CancellationToken cancellationToken);
        List<CheckBoxViewModel> GetAllSubjects();
        List<DropDownViewModel> GetAllTeachers();
        List<DropDownViewModel> GetAllLevelHeads();
        List<DropDownViewModel> GetAllDepartmentHeads();
        List<DropDownViewModel> GetAllSystemRoles();
        List<DropDownViewModel> GetTeacherAssignedSubject(int gradeId);
        List<DropDownViewModel> GetTeacherAssignedSubjectForSelectedGrade(int gradeId);
        Task<List<DropDownViewModel>> GetSubjectClasses(int gradeId, int subjectId, int lessonId, CancellationToken cancellationToken);
    }
}
