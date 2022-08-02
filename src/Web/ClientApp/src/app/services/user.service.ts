import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { upload, Upload } from "../models/common/upload";
import { UserModel} from '../models/admin/user.model';
import { StudentModel } from '../models/admin/student.model';
import { DownloadFileModel} from '../models/admin/download.file.model';
import { PaginatedUserModel} from '../models/admin/paginated.user.model';
import { PaginatedStudentModel} from '../models/admin/paginated.student.model';
import { environment } from 'src/environments/environment';
import { ResponseModel } from '../models/common/response.model';
import { StudentListDropDownMasterData } from '../models/admin/student.list.dropdown.master.data.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  getUsersList(searchText:string,currentPage:number,pageSize:number): Observable<PaginatedUserModel> {
    return this.httpClient.
      get<PaginatedUserModel>(environment.apiUrl + 'User/getUsersList', {
        params: new HttpParams()
            .set('searchText', searchText)
            .set('currentPage', currentPage.toString())
            .set('pageSize', pageSize.toString())

    });
  }

  getUserById(id: number): Observable<UserModel> {
    return this.httpClient.get<UserModel>(environment.apiUrl +'User/getUserById/'+ id);
  }

  saveUser(model: UserModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'User/saveUser', model);
  }


  deleteUser(id:number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(environment.apiUrl +'User/saveUser/'+ id);
  }

  getStudentList(searchText:string,currentPage:number,pageSize:number,academicYearId:number,gradeId:number,classId:number): Observable<PaginatedStudentModel> {
    return this.httpClient.
      get<PaginatedStudentModel>(environment.apiUrl + 'User/getStudentList', {
        params: new HttpParams()
            .set('searchText', searchText)
            .set('currentPage', currentPage.toString())
            .set('pageSize', pageSize.toString())
            .set('academicYearId', academicYearId.toString())
            .set('gradeId', gradeId.toString())
            .set('classId', classId.toString())
    });
  }

  saveStudent(model: StudentModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'User/saveStudent', model);
  }

  deleteStudent(id:number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(environment.apiUrl +'User/deleteStudent/'+ id);
  }

  uploadClassStudents(data: FormData): Observable<Upload> {
    return this.httpClient.post(environment.apiUrl +'User/uploadClassStudents', data,{reportProgress: true,observe: 'events'}).pipe(upload());
  }

  getStudentDropdownsMasterData(): Observable<StudentListDropDownMasterData> {
    return this.httpClient.
      get<StudentListDropDownMasterData>(environment.apiUrl + 'User/getStudentDropdownsMasterData');
  }

  getStudentById(id:number): Observable<StudentModel> {
    return this.httpClient.
      get<StudentModel>(environment.apiUrl + 'User/getStudentById/'+ id);
  }

  UpdateUserPassword(model: UserModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'User/UpdateUserPassword', model);
  }
}
