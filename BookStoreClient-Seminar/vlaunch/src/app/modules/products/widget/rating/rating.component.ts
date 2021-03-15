import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss'],
})
export class RatingComponent implements OnInit {
  constructor() {}

  @Input()
  ratingData;

  @Output() onChangePage: EventEmitter<number> = new EventEmitter<number>();

  ngOnInit(): void {
    console.log(this.ratingData);
    
  }

  changePage(page) {
    this.onChangePage.emit(page);
  }
}
