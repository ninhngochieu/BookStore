import { Component, OnInit } from '@angular/core';
import firebase from 'firebase/app';
import { environment } from 'src/environments/environment';
import { SeoService } from './core/services/seo.service';
import { AuthService } from './modules/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(
    public authService: AuthService,
    private seoService: SeoService
  ) {}

  ngOnInit(): void {
    firebase.initializeApp(environment.firebaseConfig);
    this.seoService.init();
  }
}
