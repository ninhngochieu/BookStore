import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { HomeService } from 'src/app/modules/home/home.service';

@Component({
  selector: 'app-product-list-suggestion',
  templateUrl: './product-list-suggestion.component.html',
  styleUrls: ['./product-list-suggestion.component.scss'],
})
export class ProductListSuggestionComponent implements OnInit {
  title = 'Sản phẩm mới nhất';
  products;
  constructor(public homeService: HomeService) {}

  ngOnInit(): void {
    this.homeService.newestProducts$.pipe(take(1)).subscribe((products) => {
      this.products = products;
    });
  }
}
