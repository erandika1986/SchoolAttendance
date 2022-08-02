import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AttendanceReportFilter } from '../models/report/attendance.report.filter.model';
import { ClassTeacheeDropDownMasterData } from "../models/report/class.teacher.dropdown.master.data.model";

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private httpClient: HttpClient) { }

  downloadClassAttendanceForAllSubjects(filter: AttendanceReportFilter): Observable<any> {
    return this.httpClient.post<any>(environment.apiUrl +'Report/downloadClassAttendanceForAllSubjects',filter,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }

  downloadClassAttendanceForSelectedSubject(filter: AttendanceReportFilter): Observable<any> {
    return this.httpClient.post<any>(environment.apiUrl +'Report/downloadClassAttendanceForSelectedSubject',filter,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }

  generateZonalReportForSelectedClass(filter: AttendanceReportFilter): Observable<any> {
    return this.httpClient.post<any>(environment.apiUrl +'Report/generateZonalReportForSelectedClass',filter,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }

  getTeacherClassMasterData(): Observable<ClassTeacheeDropDownMasterData> {
    return this.httpClient.
      get<ClassTeacheeDropDownMasterData>(environment.apiUrl + 'Report/getTeacherClassMasterData');
  }
}
