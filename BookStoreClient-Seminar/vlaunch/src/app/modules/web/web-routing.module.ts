import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { customMatcher } from 'src/app/core/utils';
import { AuthGuard } from '../auth/auth.guard';
import { LoginComponent } from '../auth/login/login.component';
import { LoginGuard } from '../auth/login/login.guard';
import { OptVerifyComponent } from '../auth/opt-verify/opt-verify.component';
import { PasswordRegisterComponent } from '../auth/password-register/password-register.component';
import { RegisterComponent } from '../auth/register/register.component';
import { BlogDetailComponent } from '../blogs/blog-detail/blog-detail.component';
import { BlogsComponent } from '../blogs/blog-list/blog-list.component';
import { CartComponent } from '../cart/cart.component';
import { CheckoutSuccessComponent } from '../checkout/checkout-success/checkout-success.component';
import { HomeComponent } from '../home/home.component';
import { ProductDetailComponent } from '../products/product-detail/product-detail.component';
import { ProductListPageComponent } from '../products/product-list-page/product-list-page.component';
import { AddressFormComponent } from '../user/address-form/address-form.component';
import { AddressComponent } from '../user/address/address.component';
import { OrderDetailComponent } from '../user/order-detail/order-detail.component';
import { OrderComponent } from '../user/order/order.component';
import { PasswordComponent } from '../user/password/password.component';
import { ProductRatingComponent } from '../user/product-rating/product-rating.component';
import { ProfileComponent } from '../user/profile/profile.component';
import { UserComponent } from '../user/user.component';
import { WebComponent } from './web.component';

const webRoutes: Routes = [
  {
    path: '',
    component: WebComponent,
    children: [
      { path: '', component: HomeComponent },
      {
        matcher: (url) => customMatcher(url, ['p']),
        component: ProductDetailComponent,
      },
      {
        path: 'search',
        component: ProductListPageComponent,
      },
      {
        matcher: (url) => customMatcher(url, ['c', 'co']),
        component: ProductListPageComponent,
      },
      { path: 'blog', component: BlogsComponent },
      { path: 'blog/:slug', component: BlogDetailComponent },
      { path: 'cart', component: CartComponent },
      {
        path: 'user',
        component: UserComponent,
        canActivate: [AuthGuard],
        children: [
          { path: 'profile', component: ProfileComponent },
          { path: 'password', component: PasswordComponent },
          { path: 'address', component: AddressComponent },
          { path: 'address/:id', component: AddressFormComponent },
          { path: 'order', component: OrderComponent },
          { path: 'order/:code', component: OrderDetailComponent },
          { path: 'order/:code/rating/:id', component: ProductRatingComponent },
        ],
      },
      {
        canActivate: [LoginGuard],
        component: LoginComponent,
        path: 'login',
      },
      {
        canActivate: [LoginGuard],
        component: RegisterComponent,
        path: 'register',
      },
      {
        canActivate: [LoginGuard],
        component: OptVerifyComponent,
        path: 'otp',
      },
      {
        canActivate: [LoginGuard],
        component: PasswordRegisterComponent,
        path: 'password',
      },
      { path: 'thankyou', component: CheckoutSuccessComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(webRoutes)],
  exports: [RouterModule],
})
export class WebRoutingModule {}
