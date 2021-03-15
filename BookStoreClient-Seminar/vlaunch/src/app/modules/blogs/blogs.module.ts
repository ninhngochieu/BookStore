import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SpinnerComponent } from 'src/app/components/spinner/spinner.component';
import { PipesModule } from 'src/app/core/pipe/pipes.module';
import { ShareModule } from '../share.module';
import { BlogDetailComponent } from './blog-detail/blog-detail.component';
import { BlogsComponent } from './blog-list/blog-list.component';
import { RelatedContentComponent } from './components/related-content/related-content.component';

@NgModule({
  declarations: [
    RelatedContentComponent,
    BlogsComponent,
    BlogDetailComponent,
    SpinnerComponent,
  ],
  imports: [CommonModule, RouterModule, PipesModule, ShareModule],
})
export class BlogsModule {
  constructor() {}
}
