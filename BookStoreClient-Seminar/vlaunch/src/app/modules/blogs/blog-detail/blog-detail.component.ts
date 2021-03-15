import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SeoService } from 'src/app/core/services/seo.service';
import { Article } from 'src/app/models/article';
import { BlogService } from '../blog.service';

@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class BlogDetailComponent implements OnInit, OnDestroy {
  constructor(
    private route: ActivatedRoute,
    public service: BlogService,
    private seoService: SeoService
  ) {}
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  article: Article;
  newestArticles: Article[];
  breadcrumbData = [{ name: 'Tin tức', link: ['/blog'] }];

  subscription = new Subscription();

  ngOnInit(): void {
    this.subscription.add(
      this.route.paramMap.subscribe((paramMap) => {
        let slug = paramMap.get('slug');
        let pos = slug.lastIndexOf('-');
        let result = pos != -1 ? slug.substring(pos + 1) : null;
        this.service.getArticlesDetail(result);
        this.service.getArticles(1, 3);
        this.breadcrumbData = [{ name: 'Tin tức', link: ['/blog'] }];
      })
    );

    this.subscription.add(
      this.service.newestArticle$.subscribe((articles) => {
        this.newestArticles = articles;
      })
    );

    this.subscription.add(
      this.service.article$.subscribe((article) => {
        this.article = article;
        this.breadcrumbData.push({
          name: article.title,
          link: ['/blog', article.slug + '-' + article.id],
        });

        this.setupSeo(this.article);
      })
    );
  }

  private setupSeo(item: Article) {
    let seoData = [];
    this.seoService.updateTitle(item.seo_title ? item.seo_title : item.title);
    seoData.push({
      property: 'og:title',
      content: item.seo_title ? item.seo_title : item.title,
    });

    seoData.push({
      name: 'description',
      content: item.seo_description ? item.seo_description : item.title,
    });
    seoData.push({
      property: 'og:description',
      content: item.seo_description ? item.seo_description : item.title,
    });
    seoData.push({
      property: 'og:image',
      content: item.image_lg,
    });
    seoData.push({
      property: 'og:url',
      content: window.location.href,
    });

    if (seoData.length > 0) {
      this.seoService.updateMeta(seoData);
    }
  }

  pageUrlEncode(): string {
    return encodeURI(window.location.href);
  }
}
