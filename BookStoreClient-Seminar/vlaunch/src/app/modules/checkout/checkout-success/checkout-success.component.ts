import { Component, OnInit } from '@angular/core';
import { SeoService } from 'src/app/core/services/seo.service';

@Component({
  selector: 'app-checkout-success',
  templateUrl: './checkout-success.component.html',
  styleUrls: ['./checkout-success.component.scss'],
})
export class CheckoutSuccessComponent implements OnInit {
  title: string = 'ADIVIET';

  constructor(private seoService: SeoService) {}

  ngOnInit(): void {
    this.seoService.init();
  }
}
