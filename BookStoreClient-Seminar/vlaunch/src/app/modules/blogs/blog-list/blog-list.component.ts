import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SeoService } from 'src/app/core/services/seo.service';
import { BlogService } from '../blog.service';

@Component({
  selector: 'app-blogs',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.scss'],
})
export class BlogsComponent implements OnInit, OnDestroy {
  constructor(public service: BlogService, private seoService: SeoService) {}
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  subscription = new Subscription();
  articleData: any;

  ngOnInit(): void {
    this.service.init();

    this.subscription.add(
      this.service.articles$.subscribe((articleData) => {
        this.articleData = articleData;
      })
    );

    this.seoService.init();
    this.setupSeo();
  }

  private setupSeo() {
    this.seoService.updateTitle('Tin tức');
  }

  changePage(page) {
    this.service.getArticles(page);
  }
}
