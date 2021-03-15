import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { AlertService } from 'src/app/core/services/alert.service';
import { HttpService } from 'src/app/core/services/http.service';
import { DEFAULT_PRODUCT } from 'src/app/data';
import { Category } from 'src/app/models/category';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class HomeService {
  constructor(private http: HttpService, private alertService: AlertService) { }

  categories$ = new Subject<Category[]>();
  categoryRoot$ = new Subject<Category[]>();
  slider$: Observable<any>;
  banner1x4$ = new Subject<any>();
  banner1x2$ = new Subject<any>();
  categoryBanner$ = new Subject<any>();
  menu$ = new Subject<any>();
  newestProducts$ = new Subject<any>();

  init() {
    this.getCategories();
    this.getCategoryRoot(5);
    this.getBanner1x4();
    this.getBanner1x2();
    this.getSlider();
    this.getMenu();
    this.getNewestProduct();
  }

  getCategories() {
    this.getCategoryRoot();
  }

  getFooterCategories() {
    const url = 'category-root?limit=' + environment.HOME_CATEGORY_ROOT_LIMIT;
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          return res.data.slice(0, 8);
        } else {
          this.alertService.errorAlert(res);
        }
      })
    );
  }

  getCategoryRoot(count?: number) {
    const url = 'category-root?limit=' + environment.HOME_CATEGORY_ROOT_LIMIT;
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        count
          ? this.categoryRoot$.next(res.data.slice(0, count))
          : this.categories$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getTopProduct(id: number) {
    const url = 'category/' + id + '/top-product';
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          let data = [...res.data];

          return data;
        } else {
          this.alertService.errorAlert(res);
          return null;
        }
      })
    );
  }

  getCategorySub(id: number) {
    const url = 'category/subs?parent_id=' + id;
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          let data = [...res.data].slice(0, 6);
          return data;
        } else {
          this.alertService.errorAlert(res);
          return null;
        }
      })
    );
  }

  getCategoryTree() {
    const url = 'category-tree';
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          let data = [...res.data];
          return data;
        } else {
          this.alertService.errorAlert(res);
          return [];
        }
      })
    );
  }

  getBanner1x4() {
    const url = 'banner/' + environment.BANNER_1X4;
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.banner1x4$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getBanner1x2() {
    const url = 'banner/' + environment.BANNER_1X2;
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.banner1x2$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getSlider() {
    const url = 'banner/' + environment.SLIDER;
    this.slider$ = this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          return res.data;
        } else {
          this.alertService.errorAlert(res);
          return null;
        }
      })
    );
  }

  getMenu() {
    const url = 'menu/' + environment.MENU_NAVBAR;
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.menu$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getNewestProduct() {
    const url = 'product-newest';
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.newestProducts$.next(res.data.results);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getDiscountBanner() {
    const url = 'banner/' + environment.DISCOUNT_BANNER;
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          return res.data;
        } else {
          this.alertService.errorAlert(res);
          return null;
        }
      })
    );
  }

  getCategoryTreeBanner() {
    const url = 'banner/' + environment.CATEGORY_BANNER;
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.categoryBanner$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }
}
