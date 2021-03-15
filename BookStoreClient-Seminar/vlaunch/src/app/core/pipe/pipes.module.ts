import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import {DefaultImagePipe, FormatPricePipe, OrderStatusPipe, ShortBriefPipe, UrlPipe} from './pipes';

@NgModule({
  declarations: [DefaultImagePipe, ShortBriefPipe, FormatPricePipe, OrderStatusPipe, UrlPipe],
  exports: [DefaultImagePipe, ShortBriefPipe, FormatPricePipe, OrderStatusPipe, UrlPipe],
  imports: [CommonModule],
})
export class PipesModule {}
