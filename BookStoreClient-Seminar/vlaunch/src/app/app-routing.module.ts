import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { CheckoutFormComponent } from './modules/checkout/checkout-form/checkout-form.component';
import { WebComponent } from './modules/web/web.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', component: WebComponent },
  { path: 'checkout/:token', component: CheckoutFormComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled', relativeLinkResolution: 'legacy' }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
