import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent implements OnInit {
  constructor() {}

  @Input()
  data: any;

  @Output() onPageChange: EventEmitter<number> = new EventEmitter<number>();

  ngOnInit(): void {}

  public changePage(page: number): void {
    if (page === this.data.current_page) return;
    this.onPageChange.emit(page);
  }
}
