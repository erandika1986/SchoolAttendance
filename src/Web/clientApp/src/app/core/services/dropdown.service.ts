import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CheckBoxModel } from '../models/common/check.box.model';
import { DropDownModel} from "../models/common/drop.down.modal";

@Injectable({
  providedIn: 'root'
})
export class DropdownService {

  constructor(private httpClient: HttpClient) { }

  getAllAcademicYears(): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAllAcademicYears' );
  }

  getAllAcademicLevels(): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAllAcademicLevels' );
  }

  getTeachGradesForLoggedInUser(academicYear:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getTeachGradesForLoggedInUser/'+academicYear );
  }

  getClassesForSelectedGrade(academicYear:number,gradeId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getClassesForSelectedGrade/'+academicYear+"/"+gradeId );
  }

  getAssignedClassForLoggedInUser(academicYear:number,gradeId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAssignedClassForLoggedInUser/'+academicYear+"/"+gradeId );
  }

  getAssignedClassSubjectForLoggedInUser(classId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAssignedClassSubjectForLoggedInUser/'+classId);
  }

  getAssignedClassForTeacher(academicYear:number,gradeId:number,teacherId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAssignedClassForTeacher/'+academicYear+"/"+gradeId+"/"+teacherId);
  }

  getAssignedClassSubjectForTeacher(classId:number,teacherId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAssignedClassSubjectForTeacher/'+classId+"/"+teacherId);
  }

  getAllSubjects(): Observable<CheckBoxModel[]> {
    return this.httpClient.
      get<CheckBoxModel[]>(environment.apiUrl + 'DropDown/getAllSubjects');
  }

  getAllTeachers(): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAllTeachers');
  }

  getAllLevelHeads(): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAllLevelHeads');
  }

  getAllDepartmentHeads(): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAllDepartmentHeads');
  }

  getAllSystemRoles(): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getAllSystemRoles');
  }

  getTeacherAssignedSubject(gradeId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getTeacherAssignedSubject/'+gradeId);
  }

  getTeacherAssignedSubjectForSelectedGrade(gradeId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getTeacherAssignedSubjectForSelectedGrade/'+gradeId);
  }

  getSubjectClasses(gradeId:number,subjectId:number,lessonId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDown/getSubjectClasses/'+gradeId+'/'+subjectId+'/'+lessonId);
  }
}
