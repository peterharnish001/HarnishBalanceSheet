import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { LoginRequestModel } from './login-reqest.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  public login(request: LoginRequestModel): Observable<HttpResponse<any>> {
    return this.http.post(environment.apiUrl + 'auth/login', request, { observe: 'response' });
  }
}
