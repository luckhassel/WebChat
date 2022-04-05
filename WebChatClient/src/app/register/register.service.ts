import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) { }

  registerNewUser(newUser: User){
    return this.http.post('https://localhost:44331/api/login/register', newUser);
  }
}
