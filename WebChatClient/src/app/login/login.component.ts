import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { User } from '../register/user';
import { LoginService } from './login.service';
import { UserLogged } from './user-logged';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string = '';
  password: string = '';

  constructor(private router: Router,
    private login: LoginService) { }

  ngOnInit(): void {
  }

  Login() {
    console.log(this.username);
    console.log(this.password);
      const userLog = {
        username: this.username,
        password: this.password,
        role: "user"
      } as User;

      this.login.loginUser(userLog)
      .subscribe(
        () => {
          this.router.navigate(['/chat']);
        },
        (error) => {
          console.log(error);
        }
      );
    }

}
