import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SeoService } from 'src/app/core/services/seo.service';
import { Cart } from 'src/app/models/cart';
import { CartLine } from 'src/app/models/cart-line';
import { CartService } from './cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit, OnDestroy {
  constructor(public service: CartService, private seoService: SeoService) {}

  subscription = new Subscription();
  cart: Cart;
  summary: any;

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.service.getCartDetail();
    this.subscription.add(
      this.service.cart$.subscribe((cart) => {
        this.cart = cart;
      })
    );

    this.subscription.add(
      this.service.cartSummary$.subscribe((sum) => {
        this.summary = sum;
      })
    );

    this.seoService.updateTitle('Giỏ hàng');
  }

  deleteCartLine(line: CartLine): void {
    if (!line) return;
    this.service.deleteCartProduct(line.id);
  }

  updateQuantity(line: CartLine, type) {
    let data = {
      quantity: line.quantity,
    };
    if (type === 'add') {
      data.quantity = line.quantity + 1;
    } else if (type === 'minus') {
      data.quantity = line.quantity - 1;
    } else if (typeof (type * 1) === 'number') {
      data.quantity = type;
    }

    if (data.quantity < 1) {
      this.deleteCartLine(line);
      return;
    }

    this.service.updateQuantityCartLine(line.id, data);
  }
}
