import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterService } from './register.service';
import { User } from './user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  username: string = '';
  password: string = '';

  constructor(private router: Router,
    private register: RegisterService) { }

  ngOnInit(): void {
  }

  Register() {
    console.log(this.username);
    console.log(this.password);
      const newUser = {
        username: this.username,
        password: this.password,
        role: "user"
      } as User;

      this.register.registerNewUser(newUser).subscribe(
        () => {
          this.router.navigate(['/']);
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
