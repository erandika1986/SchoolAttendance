import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseModel } from '../models/common/response.model';
import { PaginatedClassModel } from "../models/class/paginated.class.model";
import { ClassModel } from '../models/class/class.model';
import { ClassMasterDataModel } from "../models/class/class.master.data.model";
import { ClassSubjectModel } from '../models/class/class.subject.model';

@Injectable({
  providedIn: 'root'
})
export class ClassService {

  constructor(private httpClient: HttpClient) { }

  getClassList(searchText:string,currentPage:number,pageSize:number,academicYearId:number,gradeId:number): Observable<PaginatedClassModel> {
    return this.httpClient.get<PaginatedClassModel>(environment.apiUrl +'Class/getClassList', {
      params: new HttpParams()
          .set('searchText', searchText)
          .set('currentPage', currentPage.toString())
          .set('pageSize', pageSize.toString())
          .set('academicYearId', academicYearId.toString())
          .set('gradeId', gradeId.toString())
  });
  }

  getClassDetail(gradeId:number,classId:number): Observable<ClassModel> {
    return this.httpClient.get<ClassModel>(environment.apiUrl +'Class/getClassDetail/'+ gradeId +"/"+classId);
  }

  getClassSubjectsForSelectedGrade(gradeId:number): Observable<ClassSubjectModel[]> {
    return this.httpClient.get<ClassSubjectModel[]>(environment.apiUrl +'Class/getClassSubjectsForSelectedGrade/'+ gradeId );
  }

  saveClassDetail(model: ClassModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'Class/saveClassDetail', model);
  }

  getClassMasterData(): Observable<ClassMasterDataModel> {
    return this.httpClient.get<ClassMasterDataModel>(environment.apiUrl +'Class/getClassMasterData');
  }

  deleteClass(id:number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(environment.apiUrl +'Class/deleteClass/'+ id);
  }
}
