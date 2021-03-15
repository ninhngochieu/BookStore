import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { SeoService } from 'src/app/core/services/seo.service';
import { cutStringWithLength, generateUrl, reverseString } from 'src/app/core/utils';
import { PRICE_FILTER_VALUE, PRODUCT_PARAM_CODE } from 'src/app/data';
import { Category } from 'src/app/models/category';
import { HomeService } from 'src/app/modules/home/home.service';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-list-page',
  templateUrl: './product-list-page.component.html',
  styleUrls: ['./product-list-page.component.scss'],
})
export class ProductListPageComponent implements OnInit, OnDestroy {
  constructor(
    public productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
    private homeService: HomeService,
    private seoService: SeoService
  ) {
    this.router.onSameUrlNavigation = 'reload';
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  isDragging: boolean;
  customOptions: OwlOptions = {
    loop: false,
    mouseDrag: true,
    touchDrag: true,
    dots: false,
    navSpeed: 700,
    margin: 15,
    responsive: {
      0: {
        items: 2,
      },
      300: {
        items: 3,
      },
      650: {
        items: 4,
      },
      800: {
        items: 5,
        touchDrag: false,
        mouseDrag: false,
      },
    },
    nav: false,
  };

  public productData;
  public categoryRoots: Category[] = [];
  private currentParam = {};
  private filterUrl;
  private subscription = new Subscription();
  public discountBanner;
  public filterRadioValue = 0;
  public childCategory: Category[];

  ngOnInit(): void {
    this.subscription.add(
      this.router.events.subscribe((event) => {
        if (event instanceof NavigationEnd) {
          this.reloadCurrentRoute();
        }
      })
    );

    this.subscription.add(
      this.route.queryParams.subscribe((params) => {
        this.seoService.init();
        delete this.currentParam['page'];
        for (let key in params) {
          let value = params[key];
          if (key === 'keyword') {
            value = value.split('-').join(' ');
          }

          this.currentParam[key] = value;
        }

        this.setFilterByParams(this.currentParam);

        if (!this.currentParam['keyword']) {
          let url = this.router.url;
          this.extractUrl(url);
        } else {
          this.filterUrl = 'search'
          this.productService.getProductList(this.currentParam);
          this.seoService.updateTitle(this.currentParam['keyword'] + '- AdivietSports Việt Nam - Mua sắm thể thao');
        }
      })
    );

    this.homeService.categoryRoot$.pipe(take(1)).subscribe((categoryRoots) => {
      this.categoryRoots = categoryRoots;
    });

    this.productService
      .getBanner()
      .pipe(take(1))
      .subscribe((banner) => (this.discountBanner = banner));

    this.subscription.add(
      this.productService.products$.subscribe((productData) => {
        this.productData = productData;
      })
    );

    this.homeService.getCategoryRoot(5);
  }

  changePage(page) {
    this.currentParam['page'] = page;
    if (this.filterUrl) {
      this.router.navigate(['/' + this.filterUrl], {
        queryParams: this.currentParam,
      });

      return;
    }
    this.router.navigate(['/search'], {
      queryParams: this.currentParam,
    });
  }

  private setFilterByParams(params) {
    if (!params['max_price'] && !params['min_price']) {
      this.filterRadioValue = 0;
      return;
    }

    if (params['max_price'] <= 100000) {
      this.filterRadioValue = 1;
    } else if (params['min_price'] >= 1000000) {
      this.filterRadioValue = 6;
    } else {
      for (let key in PRICE_FILTER_VALUE) {
        if (PRICE_FILTER_VALUE[key].min_price == params.min_price) {
          this.filterRadioValue = parseInt(key);
          break;
        }
      }
    }
  }

  private extractUrl(url) {
    if (reverseString(url).indexOf('?') > 0) {
      let index = reverseString(url).indexOf('?');
      url = reverseString(reverseString(url).slice(index + 1));
    }

    this.filterUrl = url;
    let arr = url.split('-');
    let code = arr[arr.length - 1];
    if (code === '/search') {
      this.productService.getProductList(this.currentParam);
    } else {
      this.setParamsByCode(code);
    }
  }

  private setParamsByCode(code) {
    let arrCode = code.split('');
    let type = '';
    let id = '';

    for (let i = 0; i < arrCode.length; i++) {
      let value = arrCode[i] * 1;
      if (!Number.isInteger(value)) {
        type += arrCode[i];
      } else {
        id += arrCode[i];
      }
    }

    if (type.length === 0 || id.length === 0) return;
    let key = PRODUCT_PARAM_CODE[type];
    this.currentParam[key] = id;
    this.productService.getProductList(this.currentParam);
    this.productService.getCategoryChild(id).subscribe(res => {
      this.childCategory = res;
    });
    this.productService.getCategoryDetail(id).subscribe(res => {
      this.setupSeo(res);
    });
  }

  slideIn(event): void {
    const target = event.target as HTMLElement;

    if (target.localName == 'div') {
      let parent = target.parentElement;
      parent.classList.toggle('show');
    } else {
      let parent = target.parentElement;
      parent = parent.parentElement;
      parent.classList.toggle('show');
    }
  }

  filterCategory(cate) {
    return ['/' + cate.slug + '-c' + cate.id];
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigateByUrl(currentUrl);
    });
  }

  filterPrice(target) {
    let value = target.value;
    let priceParams = PRICE_FILTER_VALUE[value];
    this.currentParam = {
      ...this.currentParam,
      ...priceParams,
    };

    if (!priceParams || !priceParams.min_price)
      delete this.currentParam['min_price'];
    if (!priceParams || !priceParams.max_price)
      delete this.currentParam['max_price'];

    this.router.navigate(['/' + this.filterUrl], {
      queryParams: this.currentParam,
    });
  }

  bannerLink(item) {
    return generateUrl(item);
  }

  private setupSeo(item: Category) {
    this.seoService.updateTitle(item.name + '- AdivietSports Việt Nam - Mua sắm thể thao');
    let seoData = [];

    seoData.push({
      property: 'og:title',
      content: item.name,
    });

    seoData.push({
      name: 'description',
      content: item.name
    });
    seoData.push({
      property: 'og:description',
      content: item.name,
    });
    seoData.push({
      property: 'og:image',
      content: item.avatar_sm,
    });
    seoData.push({
      property: 'og:url',
      content: window.location.href,
    });

    if (seoData.length > 0) {
      this.seoService.updateMeta(seoData);
    }
  }
}
