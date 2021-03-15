import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  constructor(public authService: AuthService) {}

  phone: any;
  code: any;

  ngOnInit(): void {
    this.phone = this.authService.phoneRegister;
  }

  register() {
    this.authService.submitted = true;
    this.authService.register(this.code);
  }
}
