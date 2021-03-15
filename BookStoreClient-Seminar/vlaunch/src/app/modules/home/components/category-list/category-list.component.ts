import { Component, Input, OnInit } from '@angular/core';
import { HomeService } from '../../home.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss'],
})
export class CategoryListComponent implements OnInit {
  constructor() { }
  ngOnInit(): void {

  }

  @Input()
  categories;


}
