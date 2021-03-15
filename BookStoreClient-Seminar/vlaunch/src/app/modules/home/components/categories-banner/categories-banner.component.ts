import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { generateUrl } from 'src/app/core/utils';
import { HomeService } from '../../home.service';

@Component({
  selector: 'app-categories-banner',
  templateUrl: './categories-banner.component.html',
  styleUrls: ['./categories-banner.component.scss'],
})
export class CategoriesBannerComponent implements OnInit, OnDestroy {
  constructor(public service: HomeService) { }

  ngOnDestroy(): void {
    this.subscribe.unsubscribe();
  }

  banners: [];
  subscribe = new Subscription();

  ngOnInit(): void {
    this.subscribe.add(
      this.service.banner1x4$.subscribe((banners) => {
        if (banners) {
          this.banners = banners.items;
        }
      })
    );
  }

  getRouterUrl(item): any {
    return generateUrl(item);
  }
}
