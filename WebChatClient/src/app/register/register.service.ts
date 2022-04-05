import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';
import { BaseurlService } from '../services/baseurl.service';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient, private baseUrl: BaseurlService) { }

  registerNewUser(newUser: User){
    return this.http.post(`${this.baseUrl.getBaseUrl()}/api/login/register`, newUser);
  }
}
