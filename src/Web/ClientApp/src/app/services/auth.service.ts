import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {LoginModel} from "../models/common/login.model";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  login(loginModel: LoginModel): Observable<any> {
    return this.httpClient.post<any>(environment.apiUrl + 'Auth/login', loginModel);
  }
}
