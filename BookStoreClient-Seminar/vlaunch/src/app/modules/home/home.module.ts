import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { ProductListSuggestionComponent } from 'src/app/components/product-list-suggestion/product-list-suggestion.component';
import { PipesModule } from 'src/app/core/pipe/pipes.module';
import { ShareModule } from '../share.module';
import { CategoriesBannerComponent } from './components/categories-banner/categories-banner.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { HomeBannerComponent } from './components/home-banner/home-banner.component';
import { TrendingBannerComponent } from './components/trending-banner/trending-banner.component';
import { HomeComponent } from './home.component';

@NgModule({
  declarations: [
    HomeComponent,
    HomeBannerComponent,
    CategoriesBannerComponent,
    CategoryListComponent,
    TrendingBannerComponent,
    ProductListSuggestionComponent,
  ],
  imports: [CommonModule, CarouselModule, PipesModule, RouterModule, ShareModule],
})
export class HomeModule {}
