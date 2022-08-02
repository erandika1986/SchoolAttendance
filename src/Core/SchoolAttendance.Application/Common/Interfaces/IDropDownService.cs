
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
    List<DropDownViewModel> GetAssignedClassForLoggedInUser(int academicYear, int gradeId, string userName);
    List<DropDownViewModel> GetAssignedClassSubjectForLoggedInUser(int classId, string userName);
    List<DropDownViewModel> GetAssignedClassForTeacher(int academicYear, int gradeId, int teacherId);
    List<DropDownViewModel> GetAssignedClassSubjectForTeacher(int classId, int teacherId);
    List<DropDownViewModel> GetTeachGradesForLoggedInUser(int academicYear, string userName);
    List<CheckBoxViewModel> GetAllSubjects();
    List<DropDownViewModel> GetAllTeachers();
    List<DropDownViewModel> GetAllLevelHeads();
    List<DropDownViewModel> GetAllDepartmentHeads();
    List<DropDownViewModel> GetAllSystemRoles();
    List<DropDownViewModel> GetTeacherAssignedSubject(int gradeId, string userName);
    List<DropDownViewModel> GetTeacherAssignedSubjectForSelectedGrade(int gradeId, string userName);
    List<DropDownViewModel> GetSubjectClasses(int gradeId, int subjectId, int lessonId, string userName);
  }
}
