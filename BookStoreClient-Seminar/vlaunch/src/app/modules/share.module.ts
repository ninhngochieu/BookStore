import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { BreadCrumbComponent } from '../components/bread-crumb/bread-crumb.component';
import { CategoryTreeComponent } from '../components/category-tree/category-tree.component';
import { PaginationComponent } from '../components/pagination/pagination.component';
import { ProductListComponent } from '../components/product-list/product-list.component';
import { ProductComponent } from '../components/product/product.component';
import { PipesModule } from '../core/pipe/pipes.module';

@NgModule({
  declarations: [
    BreadCrumbComponent,
    ProductComponent,
    ProductListComponent,
    PaginationComponent,
    CategoryTreeComponent,
  ],
  exports: [
    BreadCrumbComponent,
    ProductComponent,
    ProductListComponent,
    PaginationComponent,
    CategoryTreeComponent,
  ],
  imports: [CommonModule, CarouselModule, PipesModule, RouterModule],
})
export class ShareModule {}
