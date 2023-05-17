import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseModel } from '../models/common/response.model';
import { SubjectModel } from "../models/subject/subject.model";
import { PaginatedSubjectModel } from "../models/subject/paginated.subject.model";

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private httpClient: HttpClient) { }

  getSubjectList(searchText:string,currentPage:number,pageSize:number,status:boolean): Observable<PaginatedSubjectModel> {
    return this.httpClient.get<PaginatedSubjectModel>(environment.apiUrl +'Subject/getSubjectList', {
      params: new HttpParams()
          .set('searchText', searchText)
          .set('currentPage', currentPage.toString())
          .set('pageSize', pageSize.toString())
          .set('status', status.toString())

  });
  }

  saveSubject(model: SubjectModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'Subject/saveSubject', model);
  }

  deleteSubject(id:number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(environment.apiUrl +'Subject/deleteSubject/'+ id);
  }
}
