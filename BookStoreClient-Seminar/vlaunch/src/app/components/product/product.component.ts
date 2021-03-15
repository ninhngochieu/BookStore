import { Component, Input, OnInit } from '@angular/core';
import { DEFAULT_PRODUCT } from 'src/app/data';
import { Product } from 'src/app/models/product';
import { CartService } from 'src/app/modules/cart/cart.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit {
  constructor(private cartService: CartService) {}

  loading: boolean = true;

  onLoad() {
    this.loading = false;
  }

  ngOnInit(): void {}

  @Input()
  product = DEFAULT_PRODUCT;

  addProductToCart(product: Product) {
    if (!product.id) return;
    let variant;
    
    try {
      variant = product.variants[0];
    } catch (e) {
      console.error('Variants error ===>', e);
      variant = null;
    }
    if (!variant) return;
    
    const data = {
      product_id: product.id,
      quantity: 1,
      variant_id: variant.id,
    };
    
    this.cartService.addProductToCart(data);
  }
}
