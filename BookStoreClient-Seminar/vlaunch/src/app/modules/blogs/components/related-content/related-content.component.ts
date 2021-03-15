import { Component, OnInit } from '@angular/core';
import { BlogService } from '../../blog.service';

@Component({
  selector: 'app-related-content',
  templateUrl: './related-content.component.html',
  styleUrls: ['./related-content.component.scss'],
})
export class RelatedContentComponent implements OnInit {
  constructor(public blogService: BlogService) {}

  discounts$;
  mostView$;

  ngOnInit(): void {
    this.discounts$ = this.blogService.getArticleDiscount();
    this.mostView$ = this.blogService.getArticleMostView();
  }
}
