import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from '../Models/user';
import { LoginServiceService } from '../Services/login-service.service';
import { Subject, takeUntil } from 'rxjs';
import { TokenService } from '../token.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit, OnDestroy {
  isLoginScreen: boolean = true;
  private readonly unsubscribe$ = new Subject();
  constructor(private loginService: LoginServiceService, private router: Router) {

  }
  user: User = new User();
  ngOnInit() {
  }

  ngOnDestroy() {
    this.unsubscribe$.next(null);
  }

  onLoginClick() {
    this.loginService.Login(this.user)
      .pipe(takeUntil(this.unsubscribe$)).subscribe((n) => {
        console.log(`Logged in with user ${this.user.UserName}`);
        localStorage.setItem('token',n.token);
        this.router.navigate(['dashboard']);
      });
  }
  onSetRegisterMode() {
    this.user = new User();
    this.isLoginScreen = false;
  }
  onUnsetRegisterMode() {
    this.user = new User();
    this.isLoginScreen = true;
  }
  onRegisterClick(){
    this.loginService.Register(this.user)
      .pipe(takeUntil(this.unsubscribe$)).subscribe((n) => {
        if(n){
          this.onUnsetRegisterMode();
        }
        else{
          alert('Some error occoured');
        }
      },e =>{
        console.error(e);
        alert('Some error occoured');
      });
  }
}
