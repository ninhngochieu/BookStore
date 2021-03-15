import { Component, OnInit } from '@angular/core';
import { generateUrl } from 'src/app/core/utils';
import { Category } from 'src/app/models/category';
import { HomeService } from 'src/app/modules/home/home.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent implements OnInit {
  constructor(public homeService: HomeService) { }
  currentYear;
  categories$;
  menu;

  ngOnInit(): void {
    this.categories$ = this.homeService.getFooterCategories();
    this.homeService.menu$.subscribe(res => {
      this.menu = res;
    });
    let date = new Date();
    this.currentYear = date.getFullYear();
  }

  onSelect(event): void {
    const target = event.target as HTMLElement;

    if (target.localName != 'h3') {
      let parent = target.parentElement;
      parent.classList.toggle('show');
    } else {
      event.target.classList.toggle('show');
    }
  }

  getRouterUrl(item): any {
    return generateUrl(item);
  }
}
