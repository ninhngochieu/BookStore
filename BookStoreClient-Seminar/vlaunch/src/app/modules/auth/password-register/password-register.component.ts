import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-password',
  templateUrl: './password-register.component.html',
  styleUrls: ['./password-register.component.scss'],
})
export class PasswordRegisterComponent implements OnInit, OnDestroy {
  constructor(public authService: AuthService) {}

  username = '';
  password = '';
  confirmPassword = '';
  token = '';
  type;

  ngOnDestroy(): void {
    window.sessionStorage.removeItem('otpType');
  }

  ngOnInit(): void {
    this.username = this.authService.phoneRegister;
    this.token = this.authService.registerToken;
    this.type = window.sessionStorage.getItem('otpType');
  }

  registerAccount(): any {
    this.authService.submitted = true;
    const data = {
      username: this.username,
      password: this.password,
      confirm_password: this.confirmPassword,
      token: this.token,
    };

    if (this.type) {
      this.authService.resetBackendAccount(data);
    } else {
      this.authService.registerBackendAccount(data);
    }
  }
}
