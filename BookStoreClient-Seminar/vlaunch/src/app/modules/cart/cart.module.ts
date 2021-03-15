import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PipesModule } from 'src/app/core/pipe/pipes.module';
import { ShareModule } from '../share.module';
import { CartComponent } from './cart.component';

@NgModule({
  declarations: [CartComponent],
  imports: [CommonModule, RouterModule, ShareModule, PipesModule],
})
export class CartModule {}
