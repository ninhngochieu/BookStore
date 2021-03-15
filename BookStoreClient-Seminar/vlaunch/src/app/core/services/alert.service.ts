import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { Alert } from '../../models/alert';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  constructor(private router: Router) {}

  alert: Alert = {};
  public alertStream$ = new Subject();

  errorAlert(res): void {
    if (res && res.error_code === 'token_not_valid') {
      localStorage.clear();
      window.location.reload();
    }

    this.alert.type = 'error';
    this.alert.message = res ? res.error_message : 'Lỗi hệ thống';
    this.alert.error_code = res ? res.error_code : null;

    // console.error(this.alert.message);
    this.open();
  }

  successAlert(message, type?: string): void {
    this.alert.message = message;
    this.alert.type = 'success';
    if (type) this.alert.extra_type = type;
    this.open();
  }

  confirmAlert(message: string, callback: Function) {
    this.alert.message = message;
    this.alert.type = 'confirm';
    this.alert.callback = callback;
  }

  private open() {
    this.alertStream$.next(this.alert);
    this.clear();
  }

  private clear(): void {
    this.alert = {};
  }
}
