import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { take } from 'rxjs/operators';
import { ProductService } from 'src/app/modules/products/product.service';

@Component({
  selector: 'bread-crumb',
  templateUrl: './bread-crumb.component.html',
  styleUrls: ['./bread-crumb.component.scss'],
})
export class BreadCrumbComponent implements OnInit, AfterViewInit {
  constructor(public productService: ProductService) {}

  ngAfterViewInit(): void {
    this.setBackground();
  }

  @Input()
  data: [];

  @ViewChild('breadcrumb')
  breadcrumb: ElementRef;

  background;

  ngOnInit(): void {
    this.productService
      .getBreadcrumbBanner()
      .pipe(take(1))
      .subscribe((banner) => {
        this.background = banner;
        this.setBackground();
      });
  }

  setBackground() {
    var url = '/assets/images/breadcrum.png';
    if (this.background && this.background.items[0]) {
      url = this.background.items[0].image_xl;
    }
    this.breadcrumb.nativeElement.setAttribute(
      'style',
      `background-image: url(${url})`
    );
  }
}
