import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { AlertService } from 'src/app/core/services/alert.service';
import { HttpService } from 'src/app/core/services/http.service';
import { Article } from 'src/app/models/article';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  constructor(
    private http: HttpService,
    private alertService: AlertService,
    private router: Router
  ) {}

  articles$ = new Subject();
  article$ = new Subject<Article>();
  newestArticle$ = new Subject<Article[]>();

  init() {
    this.getArticles();
  }

  getArticles(page?: number, limit: number = 10) {
    let url = 'article?limit=' + limit;
    if (page) {
      url = url + '&page=' + page;
    }

    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        if (limit === 3) {
          this.newestArticle$.next(res.data.results);
          return;
        }

        this.articles$.next(res.data);
      } else {
        this.alertService.errorAlert(res);
      }
    });
  }

  getArticlesDetail(id: any) {
    const url = 'article/' + id;
    this.http.get(url).subscribe((res) => {
      if (res && res.success) {
        this.article$.next(res.data);
      } else {
        // this.alertService.errorAlert(res);
        this.router.navigate(['404']);
      }
    });
  }

  getArticleMostView() {
    const url = 'tag/' + environment.ARTICLE_MOST_VIEW + '/articles';
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          return [...res.data].slice(0, 5);
        } else {
          this.alertService.errorAlert(res);
          return [];
        }
      })
    );
  }

  getArticleDiscount() {
    const url = 'tag/' + environment.ARTICLE_DISCOUNT + '/articles';
    return this.http.get(url).pipe(
      map((res) => {
        if (res && res.success) {
          return [...res.data].slice(0, 5);
        } else {
          this.alertService.errorAlert(res);
          return [];
        }
      })
    );
  }
}
