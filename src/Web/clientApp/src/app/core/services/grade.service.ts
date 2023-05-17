import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseModel } from '../models/common/response.model';
import { GradeModel } from "../models/grade/grade.model";

@Injectable({
  providedIn: 'root'
})
export class GradeService {

  constructor(private httpClient: HttpClient) { }

  getGradeList(): Observable<GradeModel[]> {
    return this.httpClient.get<GradeModel[]>(environment.apiUrl +'Grade/getGradeList/');
  }

  saveGradeDetail(model: GradeModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'Grade/saveGradeDetail', model);
  }
}
