import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { generateUrl } from 'src/app/core/utils';
import { HomeService } from 'src/app/modules/home/home.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  constructor(public service: HomeService) {}

  menu: any;

  ngOnInit(): void {
    this.service.menu$.pipe(take(1)).subscribe((menu) => {
      this.menu = menu;
    });
  }

  getRouterUrl(item): any {
    return generateUrl(item);
  }
}
