import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../Models/user';
import { TokenService } from '../token.service';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {


  private url = 'https://localhost:7245/Login';
  constructor(private httpClient: HttpClient) {

  }

  getHeaders(): HttpHeaders {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })
    return headers;
  }

  public Login(user: User) {
    return this.httpClient.post<User>(this.url, user, { headers: this.getHeaders() });
  }

  public Register(user: User) {
  return this.httpClient.post<User>(`${this.url}/Register`, user, { headers: this.getHeaders() });
  }

}
