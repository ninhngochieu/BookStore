import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { AlertService } from 'src/app/core/services/alert.service';
import { HttpService } from 'src/app/core/services/http.service';
import { Cart, CartSummary } from 'src/app/models/cart';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(
    private http: HttpService,
    private alertService: AlertService,
    private router: Router
  ) {}

  cartToken: string;
  cart$ = new Subject<Cart>();
  cartSummary$ = new Subject<CartSummary>();
  shipping_free$ = new Subject<number>();

  init() {
    this.getCartDetail();
  }

  checkCart() {
    let localCartToken = window.localStorage.getItem('cartToken');
    if (!localCartToken || localCartToken == '') {
      this.createCart();
    } else if (this.cartToken != localCartToken) {
      this.cartToken = localCartToken;
    }
  }

  getCartSummary() {
    this.checkCart();
    const url = 'cart/' + this.cartToken + '/summary';
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.cartSummary$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  addVoucher(data) {
    this.checkCart();
    const url = 'cart/' + this.cartToken + '/add-voucher';
    this.http.put(url, data).subscribe((res) => {
      if (res && res.success) {
        this.cart$.next(res.data);
        this.getCartSummary();
        this.alertService.successAlert('Áp dụng voucher thành công');
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getCartDetail() {
    if (!this.cartToken) {
      this.checkCart();
      return;
    }

    const url = 'cart/' + this.cartToken;
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.cart$.next(res.data);
        this.getCartSummary();
      } else {
        this.alertService.errorAlert(res);
        window.localStorage.removeItem('cartToken');
      }
    });
  }

  createCart() {
    const url = 'cart';
    this.http.post(url).subscribe((res) => {
      if (res && res.success) {
        this.cart$.next(res.data);
        this.cartToken = res.data.token;
        window.localStorage.setItem('cartToken', res.data.token);
        this.getCartDetail();
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  addProductToCart(data) {
    this.checkCart();
    const url = 'cart/' + this.cartToken + '/add-to-cart';

    this.http.post(url, data).subscribe((res) => {
      if (res && res.success) {
        this.alertService.successAlert(
          'Thêm sản phẩm vào giỏ hàng thành công.',
          'cart'
        );
        this.cart$.next(res.data);
        this.getCartSummary();
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  deleteCartProduct(id: number) {
    this.checkCart();
    const url = 'cart/' + this.cartToken + '/line/' + id;
    this.http.delete(url).subscribe((res) => {
      if (res && res.success) {
        this.cart$.next(res.data);
        this.getCartSummary();
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  updateQuantityCartLine(id: number, data: any) {
    this.checkCart();
    const url = 'cart/' + this.cartToken + '/line/' + id;
    this.http.put(url, data).subscribe((res) => {
      if (res && res.success) {
        this.cart$.next(res.data);
        this.getCartSummary();
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  createOrder(data) {
    const url = 'cart/' + this.cartToken + '/create-order';
    this.http.post(url, data).subscribe((res) => {
      if (res && res.success) {
        this.cartToken = null;
        window.localStorage.removeItem('cartToken');
        this.router.navigate(['/thankyou']);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getShippingPrice(data) {
    const url = 'shipping-fee';
    this.http.post(url, data).subscribe((res) => {
      if (res && res.success) {
        this.shipping_free$.next(res.data.price);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }
}
