import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SeoService } from 'src/app/core/services/seo.service';
import { Cart, CartSummary } from 'src/app/models/cart';
import { District } from 'src/app/models/location';
import { AuthService } from '../../auth/auth.service';
import { CartService } from '../../cart/cart.service';
import { AddressFormService } from '../../user/address-form/address-form.service';
declare let $: any;

@Component({
  selector: 'app-checkout-form',
  templateUrl: './checkout-form.component.html',
  styleUrls: ['./checkout-form.component.scss'],
})
export class CheckoutFormComponent implements OnInit, OnDestroy, AfterViewInit {
  constructor(
    private route: ActivatedRoute,
    public cartService: CartService,
    public service: AddressFormService,
    private fb: FormBuilder,
    public authService: AuthService,
    private seoService: SeoService
  ) {}

  @ViewChildren('provincesSelect') provincesSelect: QueryList<any>;
  @ViewChildren('districtsSelect') districtsSelect: QueryList<any>;
  @ViewChild('addressSelect') addressSelect: any;

  ngAfterViewInit(): void {
    this.provincesSelect.changes.subscribe((t) => {
      $('.provinceSelectPicker').selectpicker('refresh');
    });

    this.districtsSelect.changes.subscribe((t) => {
      $('.districtSelectPicker').selectpicker('refresh');
    });
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private subscription = new Subscription();
  public cart: Cart;
  public summary: CartSummary;
  public addresses;
  districts: District[];
  shipping_fee = 0;

  addressForm = this.fb.group({
    full_name: [null, Validators.required],
    email: [null, Validators.required],
    username: [null, Validators.required],
    district_id: [null, Validators.required],
    province_id: [null, Validators.required],
    street_address: [null, Validators.required],
  });

  ngOnInit(): void {
    this.subscription.add(
      this.route.paramMap.subscribe((paramMap) => {
        this.cartService.cartToken = paramMap.get('token');
        this.cartService.getCartDetail();
        this.service.getProvinces();
      })
    );

    this.subscription.add(
      this.cartService.cart$.subscribe((cart) => {
        if (!cart) return;
        this.cart = cart;
      })
    );

    this.subscription.add(
      this.cartService.shipping_free$.subscribe((price) => {
        this.shipping_fee = price;
      })
    );

    this.subscription.add(
      this.cartService.cartSummary$.subscribe((sum) => {
        this.summary = sum;
      })
    );

    this.subscription.add(
      this.service.districts$.subscribe((districts) => {
        this.districts = districts;
      })
    );

    this.subscription.add(
      this.service.addresses$.subscribe((addresses) => {
        this.addresses = addresses;
        if (addresses && addresses['addresses'].length > 0) {
          this.selectAddress(addresses['addresses'][0].id);
        }
      })
    );

    this.subscription.add(
      this.service.address$.subscribe((address) => {
        this.addressForm.setValue({
          full_name: address['full_name'],
          email: address['email'],
          username: address['phone'],
          district_id: address['district_id'],
          province_id: address['province_id'],
          street_address: address['street_address'],
        });

        this.changeProvince(address['province_id']);
        $('.provinceSelectPicker').selectpicker('refresh');
      })
    );

    if (this.authService.isLogin) {
      this.service.getAddresses();
    }

    this.seoService.updateTitle('Mua hàng');
  }

  selectAddress(id?: any) {
    let value;
    if (this.addressSelect) {
      value = this.addressSelect.nativeElement.value;
    }

    this.service.id = id ? id : value;
    this.service.getAddressDetail();
  }

  changeProvince(id) {
    let provinceID = +id;
    this.districts = [];
    this.service.getDistrict(provinceID);
    this.cartService.getShippingPrice({
      district_id: this.addressForm.value['district_id'],
      province_id: this.addressForm.value['province_id'],
    });
  }

  createOrder() {
    let data = this.addressForm.value;
    data.district_id = data.district_id * 1;
    data.province_id = data.province_id * 1;
    data.shipping_fee = this.shipping_fee;

    this.cartService.createOrder(data);
  }

  addVoucher(voucherInput) {
    let data = { voucher_code: voucherInput.value };
    this.cartService.addVoucher(data);
  }
}
