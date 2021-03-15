import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(public authService: AuthService, private router: Router) {}

  username: any = '';
  password: any = '';

  ngOnInit(): void {
    // window.sessionStorage.removeItem('optType');
    this.authService.errorMess = null;
  }

  async login(): Promise<any> {
    this.authService.submitted = true;
    const data = {
      username: this.username,
      password: this.password,
    };
    await this.authService.login(data);
  }

  // public resetPassword(): any {
  //   window.sessionStorage.setItem('otpType', 'resetPassword');
  //   this.router.navigate(['/otp']);
  // }
}
