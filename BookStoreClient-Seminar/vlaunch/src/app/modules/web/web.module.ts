import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FooterComponent } from 'src/app/components/footer/footer.component';
import { HeaderNavbarComponent } from 'src/app/components/header-navbar/header-navbar.component';
import { HeaderTopComponent } from 'src/app/components/header-top/header-top.component';
import { NavbarComponent } from 'src/app/components/navbar/navbar.component';
import { AuthModule } from '../auth/auth.module';
import { BlogsModule } from '../blogs/blogs.module';
import { CartModule } from '../cart/cart.module';
import { HomeModule } from '../home/home.module';
import { ProductsModule } from '../products/products.module';
import { WebRoutingModule } from './web-routing.module';
import { WebComponent } from './web.component';
import { UserModule } from '../user/user.module';
import { NavbarMobileComponent } from 'src/app/components/navbar-mobile/navbar-mobile.component';
import { PipesModule } from 'src/app/core/pipe/pipes.module';
import { FormsModule } from '@angular/forms';
import { GoTopButtonComponent } from 'src/app/components/go-top-button/go-top-button.component';
import {UrlPipe} from '../../core/pipe/pipes';

@NgModule({
  declarations: [
    HeaderNavbarComponent,
    HeaderTopComponent,
    NavbarComponent,
    FooterComponent,
    WebComponent,
    NavbarMobileComponent,
    GoTopButtonComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    WebRoutingModule,
    CartModule,
    HomeModule,
    CartModule,
    ProductsModule,
    BlogsModule,
    AuthModule,
    UserModule,
    PipesModule,
  ],
})
export class WebModule {}
