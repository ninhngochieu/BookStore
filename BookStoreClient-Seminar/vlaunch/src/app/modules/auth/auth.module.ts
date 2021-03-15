import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login/login.component';
import { ShareModule } from '../share.module';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { OptVerifyComponent } from './opt-verify/opt-verify.component';
import { PasswordRegisterComponent } from './password-register/password-register.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, OptVerifyComponent, PasswordRegisterComponent],
  imports: [
    CommonModule,
    ShareModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class AuthModule {}
