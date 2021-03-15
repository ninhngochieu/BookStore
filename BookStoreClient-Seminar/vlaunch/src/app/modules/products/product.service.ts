import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { AlertService } from 'src/app/core/services/alert.service';
import { HttpService } from 'src/app/core/services/http.service';
import { Product } from 'src/app/models/product';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(
    private http: HttpService,
    private alertService: AlertService,
    private authService: AuthService
  ) {}

  url;
  product$ = new Subject<Product>();
  products$ = new BehaviorSubject(null);
  comments$ = new Subject();
  oldComment;
  variants = [];

  getProductDetail(id) {
    const url = 'product/' + id;

    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.product$.next(res.data);
        this.variants = res.data.variants;

        if (this.authService.isLogin) {
          this.authService.getProductViewed();
        }
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getBanner() {
    const url = 'banner/' + environment.PRODUCT_LIST_BANNER;
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

  getCategoryChild(id: any) {
    const url = 'category/subs?parent_id=' + id;
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          return res.data.slice(0, 5);
        } else {
          this.alertService.errorAlert(res);
          return null;
        }
      })
    );
  }

  getCategoryDetail(id: any) {
    const url = 'category/' + id;
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

  getBreadcrumbBanner() {
    const url = 'banner/' + environment.BREADCRUMB_BANNER;
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

  getProductList(params?: any) {
    let url = 'product?limit=' + environment.PRODUCT_LIST_LIMIT;
    if (params) {
      for (let key in params) {
        let value = params[key];
        url = url + '&' + key + '=' + value;
      }
    }

    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.products$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getNewestProduct() {
    let url = 'product?limit=5';
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          return res.data.results;
        } else {
          this.alertService.errorAlert(res);
        }
      })
    );
  }

  getBreadcrumb(id: number) {
    let url = 'category/bread-crums/' + id;
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          return [...res.data];
        } else {
          this.alertService.errorAlert(res);
          return null;
        }
      })
    );
  }

  getProductRating(id, page?: number) {
    let url = `product/${id}/ratings?limit=10`;
    if (page) {
      url += `&page=${page}`;
    }
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          return { ...res.data };
        } else {
          this.alertService.errorAlert(res);
          return null;
        }
      })
    );
  }

  getComments(id, page?: number) {
    let url = `product/${id}/comments?limit=10`;
    if (page) {
      url += `&page=${page}`;
    }
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        var result = res.data;
        if (page) {
          var oldList = this.oldComment.results;
          var newData = result.results.concat(oldList);
          result.results = newData;
        }
        this.comments$.next(result);
        this.oldComment = result;
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  postComment(id, data) {
    let url = `product/${id}/comments`;
    return this.http.post(url, data).pipe(
      map((res) => {
        if (res && res.success) {
          return { ...res.data };
        } else {
          this.alertService.errorAlert(res);
          return null;
        }
      })
    );
  }
}
