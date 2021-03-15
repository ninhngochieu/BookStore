import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-go-top-button',
  templateUrl: './go-top-button.component.html',
  styleUrls: ['./go-top-button.component.scss']
})
export class GoTopButtonComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  scrollToTop(event): void {
    event.preventDefault();
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }
}
