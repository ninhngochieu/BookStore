import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChildren,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Subscription } from 'rxjs';
import { District } from 'src/app/models/location';
import { AddressFormService } from './address-form.service';
declare let $: any;

@Component({
  selector: 'app-address-form',
  templateUrl: './address-form.component.html',
  styleUrls: ['./address-form.component.scss'],
})
export class AddressFormComponent implements OnInit, AfterViewInit, OnDestroy {
  constructor(
    private route: ActivatedRoute,
    public service: AddressFormService,
    private fb: FormBuilder
  ) {}

  addressForm = this.fb.group({
    name: [null, Validators.required],
    email: [null, Validators.required],
    phone: [null, Validators.required],
    district_id: [null, Validators.required],
    province_id: [null, Validators.required],
    street_address: [null, Validators.required],
  });

  @ViewChildren('provincesSelect') provincesSelect: QueryList<any>;
  @ViewChildren('districtsSelect') districtsSelect: QueryList<any>;

  districts: District[];
  subscriptions = new Subscription();

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  ngAfterViewInit(): void {
    this.provincesSelect.changes.subscribe((t) => {
      $('.provinceSelectPicker').selectpicker('refresh');
    });

    this.districtsSelect.changes.subscribe((t) => {
      $('.districtSelectPicker').selectpicker('refresh');
    });
  }

  ngOnInit(): void {
    this.subscriptions.add(
      this.route.paramMap.subscribe((params: ParamMap) => {
        const id = params.get('id');
        this.service.id = id;
        this.service.method = id === 'add' ? 'post' : 'put';
        this.service.init();
      })
    );

    this.subscriptions.add(
      this.service.districts$.subscribe((districts) => {
        this.districts = districts;
      })
    );

    this.subscriptions.add(
      this.service.address$.subscribe(({district_id, email, name, phone, province_id, street_address}) => {
        this.addressForm.setValue({
          name,
          email,
          phone,
          district_id,
          province_id,
          street_address,
        });

        this.changeProvince(province_id);
        $('.provinceSelectPicker').selectpicker('refresh');
      })
    );
  }

  changeProvince(id): any {
    const provinceID = +id;
    this.districts = [];
    this.service.getDistrict(provinceID);
  }

  submitAddress(): any {
    const data = this.addressForm.value;
    data.district_id = data.district_id * 1;
    data.province_id = data.province_id * 1;
    this.service.updateFormData(data);
  }
}