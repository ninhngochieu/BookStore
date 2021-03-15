import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/modules/auth/auth.service';
import { CartService } from 'src/app/modules/cart/cart.service';
import { HomeService } from 'src/app/modules/home/home.service';
import { ProfileService } from 'src/app/modules/user/profile/profile.service';

@Component({
  selector: 'app-header-navbar',
  templateUrl: './header-navbar.component.html',
  styleUrls: ['./header-navbar.component.scss'],
})
export class HeaderNavbarComponent implements OnInit, OnDestroy {
  constructor(
    private cartService: CartService,
    public authService: AuthService,
    public profileService: ProfileService,
    public homeService: HomeService,
    private router: Router
  ) {}

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  keyword = '';
  subscription = new Subscription();
  productViewed;
  count = 0;
  showMenu = false;

  ngOnInit(): void {
    this.homeService.getMenu();
    this.cartService.init();
    if (this.authService.isLogin) {
      this.profileService.getProfile();
      this.authService.getProductViewed();
    }

    this.subscription.add(
      this.cartService.cart$.subscribe((cart) => {
        this.count = cart.quantity;
      })
    );

    this.subscription.add(
      this.authService.productViewed$.subscribe((products) => {
        this.productViewed = products;
      })
    );
  }

  closeMenu(value) {
    if (value === true) {
      this.showMenu = false;
    }
  }

  search() {
    if (!this.keyword || this.keyword == '') {
      this.router.navigate(['/search']);
      return;
    }

    this.router.navigate(['/search'], {
      queryParams: { keyword: this.keyword.split(' ').join('-') },
    });
  }
}
