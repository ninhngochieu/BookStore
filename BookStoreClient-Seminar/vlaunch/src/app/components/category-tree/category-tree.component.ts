import { Component, OnInit } from '@angular/core';
import { HomeService } from 'src/app/modules/home/home.service';

@Component({
  selector: 'app-category-tree',
  templateUrl: './category-tree.component.html',
  styleUrls: ['./category-tree.component.scss'],
})
export class CategoryTreeComponent implements OnInit {
  constructor(public homeService: HomeService) {}

  categoryTree$;

  ngOnInit(): void {
    this.categoryTree$ = this.homeService.getCategoryTree();
    this.homeService.getCategoryTreeBanner();
  }

  onSelect(event): void {
    const target = event.target as HTMLElement;

    if (target.localName == 'i') {
      let parent = target.parentElement;
      parent.classList.toggle('show');
    }
  }
}
