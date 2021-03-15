import { Component, Input, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { Category } from 'src/app/models/category';
import { HomeService } from 'src/app/modules/home/home.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
})
export class ProductListComponent implements OnInit {
  constructor(
    private service: HomeService,
  ) {}

  @Input()
  category: Category = new Category('Danh mục');

  openMenu = false;
  products$;
  categorySubs$;

  customOptions: OwlOptions = {
    loop: false,
    mouseDrag: true,
    touchDrag: true,
    dots: false,
    navSpeed: 700,
    margin: 15,
    autoWidth: true,
    autoHeight: true,
    responsive: {
      0: {
        items: 2,
      },
      300: {
        items: 2,
      },
      650: {
        items: 3,
      },
      800: {
        items: 4,
      },
      1000: {
        items: 5,
        touchDrag: false,
        mouseDrag: false,
      },
    },
    nav: false,
  };
  ngOnInit(): void {
    if (this.category.id) {
      this.products$ = this.service.getTopProduct(this.category.id);
      this.categorySubs$ = this.service.getCategorySub(this.category.id);
    }
  }
}
