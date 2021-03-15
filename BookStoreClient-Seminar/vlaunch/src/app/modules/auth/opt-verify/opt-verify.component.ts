import { AfterViewInit, Component, OnInit } from '@angular/core';
import firebase from 'firebase/app';
import { AuthService } from '../auth.service';
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';

@Component({
  selector: 'app-opt-verify',
  templateUrl: './opt-verify.component.html',
  styleUrls: ['./opt-verify.component.scss'],
})
export class OptVerifyComponent implements OnInit, AfterViewInit {
  constructor(public authService: AuthService, private formBuilder: FormBuilder) {}

  // recaptchaVerifier: firebase.auth.RecaptchaVerifier;
  type;
  registerForm: any;
  username: any;
  password: any;
  confirmPassword: any;
  ngAfterViewInit(): void {
    // this.recaptchaVerifier = new firebase.auth.RecaptchaVerifier(
    //   'recaptcha-container'
    // );
  }

  ngOnInit(): void {
    // this.authService.errorMess = null;
    // this.type = window.sessionStorage.getItem('otpType');
    this.registerForm = this.formBuilder.group({
      username: new FormControl('', [Validators.required, Validators.minLength(4)]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    }, {validators: this.checkPasswords});
    this.username = this.registerForm.get('username');
    this.password = this.registerForm.get('password');
    this.confirmPassword = this.registerForm.get('confirmPassword');
  }
  checkPasswords(group: FormGroup): any { // here we have the 'passwords' group
    const password = group.get('password').value;
    const confirmPassword = group.get('confirmPassword').value;

    return password === confirmPassword ? null : { notSame: true };
  }
  getOtp(): any{
    // this.authService.submitted = true;
    // window.sessionStorage.setItem('phoneRegister', this.username);
    // this.authService.getOpt(this.username, this.recaptchaVerifier);
  }

  register(): any {
    this.authService.registerAccount({
      username: this.registerForm.get('username').value,
      password: this.registerForm.get('password').value
    });
  }
}
