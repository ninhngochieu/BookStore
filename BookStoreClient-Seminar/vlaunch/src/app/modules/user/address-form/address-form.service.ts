import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Subject } from 'rxjs';
import { AlertService } from 'src/app/core/services/alert.service';
import { HttpService } from 'src/app/core/services/http.service';
import { District, Province } from 'src/app/models/location';

@Injectable({
  providedIn: 'root',
})
export class AddressFormService {
  constructor(
    private http: HttpService,
    private alertService: AlertService,
    private router: Router
  ) {}

  provinces$ = new Subject<Province[]>();
  districts$ = new Subject<District[]>();
  address$ = new Subject();
  addresses$ = new BehaviorSubject(null);

  id: any;
  method = 'post';

  init() {
    this.getProvinces();

    if (this.method === 'put') {
      this.getAddressDetail();
    }
  }

  getProvinces() {
    const url = 'province?limit=100';
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.provinces$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getDistrict(id) {
    const url = 'province/' + id + '/district?limit=100';
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.districts$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  updateFormData(data) {
    let url = 'auth/address';
    if (this.method == 'put') {
      url = url + '/' + this.id;
      this.http.put(url, data).subscribe((res) => {
        if (res && res.success) {
          this.alertService.successAlert('Cập nhật địa chỉ thành công.');
          this.router.navigate(['/user', 'address']);
        } else {
          this.alertService.errorAlert(res);
        }
      });
    } else {
      this.http.post(url, data).subscribe((res) => {
        if (res && res.success) {
          this.alertService.successAlert('Cập nhật địa chỉ thành công.');
          this.router.navigate(['/user', 'address']);
        } else {
          this.alertService.errorAlert(res);
        }
      });
    }
  }

  getAddressDetail() {
    let url = 'auth/address/' + this.id;
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.address$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getAddresses() {
    let url = 'auth/address';
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.addresses$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  deleteAddress(id: number) {
    let url = 'auth/address/' + id;
    this.http.delete(url).subscribe((res) => {
      if (res && res.success) {
        this.getAddresses();
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }
}
