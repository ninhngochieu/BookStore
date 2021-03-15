import { Component, OnInit } from '@angular/core';
import { SeoService } from 'src/app/core/services/seo.service';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(public service: HomeService, private seoService: SeoService) {}

  ngOnInit(): void {
    this.service.init();
    this.seoService.init();
  }
}
