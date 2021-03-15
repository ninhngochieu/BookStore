import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { PipesModule } from 'src/app/core/pipe/pipes.module';
import { ShareModule } from '../share.module';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductListPageComponent } from './product-list-page/product-list-page.component';
import { CommentComponent } from './widget/comment/comment.component';
import { RatingComponent } from './widget/rating/rating.component';

@NgModule({
  declarations: [
    ProductDetailComponent, 
    ProductListPageComponent, 
    CommentComponent, 
    RatingComponent, 
  ],
  exports: [],
  imports: [
    ShareModule,
    CommonModule,
    CarouselModule,
    RouterModule,
    PipesModule,
    FormsModule,
  ],
})
export class ProductsModule {}
