import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import firebase from 'firebase/app';
import 'firebase/auth';
import { BehaviorSubject } from 'rxjs';
import { AlertService } from 'src/app/core/services/alert.service';
import { HttpService } from '../../core/services/http.service';
import {TokenService} from '../../core/services/token.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private http: HttpService,
    private alertService: AlertService,
    private router: Router,
    private  tokenService: TokenService
  ) {
    this.checkLogin();
    this.phoneRegister = window.sessionStorage.getItem('phoneRegister');
  }

  phoneRegister;
  confirmationResult;
  redirectUrl: string;
  profile: object;
  isLogin = false;
  errorMess: string;
  submitted = false;
  registerToken: string;

  productViewed$ = new BehaviorSubject([]);

  checkLogin(): void {
    var token = localStorage.getItem('token');

    if (!token || token == null || token == '') {
      this.isLogin = false;
    } else {
      this.isLogin = true;
    }
  }

  getOpt(phone: string, appVerifier: any) {
    var self = this;
    this.errorMess = null;
    let phoneNumber;
    if (phone.indexOf('+84') < 0) {
      phoneNumber = '+84' + phone.trim().substring(1);
    } else {
      phoneNumber = phone.trim();
    }

    firebase
      .auth()
      .signInWithPhoneNumber(phoneNumber, appVerifier)
      .then(function (confirmationResult) {
        // SMS sent. Prompt user to type the code from the message, then sign the
        // user in with confirmationResult.confirm(code).
        self.phoneRegister = phone;
        self.confirmationResult = confirmationResult;
        self.router.navigate(['/register']);
        self.submitted = false;
      })
      .catch(function (error) {
        // Error; SMS not sent
        // ...
        console.log(error);
        if (error.code == 'auth/invalid-phone-number') {
          self.errorMess = 'Số điện thoại không đúng';
        }
        self.submitted = false;
      });
  }

  register(code: string) {
    let self = this;
    this.errorMess = null;
    if (!this.confirmationResult) {
      this.errorMess = 'Vui lòng quay lại bước nhập số điện thoại';
      this.submitted = false;
      return;
    }

    this.confirmationResult
      .confirm(code)
      .then(function (result) {
        // User signed in successfully.
        var user = result.user;
        self.submitted = false;
        self.registerToken = user.ya;

        self.router.navigate(['/password']);
        // ...
      })
      .catch(function (error) {
        // User couldn't sign in (bad verification code?)
        // ...
        console.log(error);
        if (error.code == 'auth/invalid-verification-code')
          [(self.errorMess = 'Mã OTP không đúng.')];
        self.submitted = false;
      });
  }

  async login(data: object): Promise<any> {
    // this.errorMess = null;
    // const url = 'auth/login';
    // this.http.post(url, data).subscribe((res) => {
    //   if (res && res.success) {
    //     this.isLogin = true;
    //     localStorage.setItem('token', res.data.access);
    //     localStorage.setItem('refreshToken', res.data.refresh);
    //     this.getProductViewed();
    //     this.router.navigateByUrl('/user/profile');
    //   } else {
    //     this.errorMess = res.error_message;
    //     this.alertService.errorAlert(res);
    //   }
    //
    //   this.submitted = false;
    // });
    this.errorMess = null;
    const url = 'userauth/login';
    this.http.postHandle(url, data).subscribe((res) => {
      if (res && res.success) {
        this.isLogin = true;
        // localStorage.setItem('token', res.data.access);
        // localStorage.setItem('refreshToken', res.data.refresh);
        this.tokenService.setToken(res.data.access);
        this.tokenService.setRefreshToken(res.data.refresh);
        this.getProductViewed();
        this.router.navigateByUrl('/user/profile');
      } else {
        this.errorMess = res.error_message;
        this.alertService.errorAlert(res);
      }

      this.submitted = false;
    });
  }

  logout(): void {
    this.isLogin = false;
    window.localStorage.removeItem('refreshToken');
    window.localStorage.removeItem('token');
    this.router.navigateByUrl('/');
  }

  getProductViewed() {
    const url = 'product-viewed';
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.productViewed$.next(res.data.results.slice(0, 10));
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  registerBackendAccount(data) {
    const url = 'auth/register';
    this.http.post(url, data).subscribe((res) => {
      if (res && res.success) {
        this.router.navigate(['/user', 'profile']);
      } else {
        this.alertService.errorAlert(res);
        this.errorMess = res.error_message;
      }

      this.submitted = false;
    });
  }

  resetBackendAccount(data){
    const url = 'auth/reset-password';
    this.http.post(url, data).subscribe((res) => {
      if (res && res.success) {
        this.router.navigate(['/user', 'profile']);
      } else {
        this.alertService.errorAlert(res);
        this.errorMess = res ? res.error_message : 'Lỗi hệ thống';
      }

      this.submitted = false;
    });
  }

  registerAccount(data: { password: any; username: any }): any {
    const url = 'UserAuth/register';
    this.http.postHandle(url, data).subscribe(res => {
      if (res.success){
        this.alertService.successAlert('Dang ki thanh cong, xin hay dang nhap tai khoan moi!');
        setTimeout(() => this.router.navigate(['/login']), 3000);
      }else {
        this.errorMess = res.error_message;
        this.alertService.errorAlert(res);
      }
    });
  }
}
