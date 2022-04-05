import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { User } from '../register/user';
import { BaseurlService } from '../services/baseurl.service';
import { UserLogged } from './user-logged';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  token: string = '';
  username: string = ''

  constructor(private http: HttpClient, private baseUrl: BaseurlService) { }

  loginUser(newUser: User){
    return this.http.post(`${this.baseUrl.getBaseUrl()}/api/login/auth`, newUser, {observe: 'response'}).pipe(
      tap((res) => {
        const resUser = res.body as UserLogged;
        console.log(resUser.token)
        this.token = resUser.token;
        this.username = resUser.user.username;
      })
    );
  }

  getToken(){
    return this.token;
  }

  getUsername(){
    return this.username;
  }
}
