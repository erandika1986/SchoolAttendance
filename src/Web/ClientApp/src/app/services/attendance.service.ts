import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedAttendanceModel} from '../models/attendance/paginated.attendance.mode';
import { AttendanceFilterModel } from '../models/attendance/attendance.filter.model';
import { SubjectAttendaceModel } from '../models/attendance/subject.attendace.model';
import { ResponseModel } from '../models/common/response.model';
import { AttendanceListFilterMasterData } from '../models/attendance/attendance.list.filter.master.data.model';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {

  constructor(private httpClient: HttpClient) { }

  getMySubjectAttendance(searchText:string,currentPage:number,pageSize:number,academicYearId:number,gradeId:number,classId:number,subjectId:number): Observable<PaginatedAttendanceModel> {
    return this.httpClient.
      get<PaginatedAttendanceModel>(environment.apiUrl + 'Attendance/getMySubjectAttendance', {
        params: new HttpParams()
            .set('searchText', searchText)
            .set('currentPage', currentPage.toString())
            .set('pageSize', pageSize.toString())
            .set('academicYearId', academicYearId.toString())
            .set('gradeId', gradeId.toString())
            .set('classId', classId.toString())
            .set('subjectId', subjectId.toString())
    });
  }

  getAttendanceListTeacherDropdownMasterData(): Observable<AttendanceListFilterMasterData> {
    return this.httpClient.
      get<AttendanceListFilterMasterData>(environment.apiUrl + 'Attendance/getAttendanceListTeacherDropdownMasterData');
  }

  getStartAndEndTime(filter: AttendanceFilterModel): Observable<SubjectAttendaceModel> {
    return this.httpClient.post<SubjectAttendaceModel>(environment.apiUrl +'Attendance/getStartAndEndTime', filter);
  }

  getAttendanceDetailForSubjectClassById(id: number): Observable<SubjectAttendaceModel> {
    return this.httpClient.get<SubjectAttendaceModel>(environment.apiUrl +'Attendance/getAttendanceDetailForSubjectClassById/'+id);
  }

  getAttendanceDetailForClassSubject(filter: AttendanceFilterModel): Observable<SubjectAttendaceModel> {
    return this.httpClient.post<SubjectAttendaceModel>(environment.apiUrl +'Attendance/getAttendanceDetailForClassSubject', filter);
  }

  saveAttendanceDetailForClassSubject(model: SubjectAttendaceModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'Attendance/saveAttendanceDetailForClassSubject', model);
  }
}
