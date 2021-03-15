import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output
} from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { generateUrl } from 'src/app/core/utils';
import { UserProfile } from 'src/app/models/user-profile';
import { AuthService } from 'src/app/modules/auth/auth.service';
import { HomeService } from 'src/app/modules/home/home.service';
import { ProfileService } from 'src/app/modules/user/profile/profile.service';

@Component({
  selector: 'app-navbar-mobile',
  templateUrl: './navbar-mobile.component.html',
  styleUrls: ['./navbar-mobile.component.scss'],
})
export class NavbarMobileComponent implements OnInit, OnDestroy {
  constructor(
    public homeService: HomeService,
    public profileService: ProfileService,
    public authService: AuthService,
    private router: Router
  ) { }

  private userRouteSub = Subscription.EMPTY;
  public profile: UserProfile;

  ngOnDestroy(): void {
    this.userRouteSub.unsubscribe();
  }

  ngOnInit(): void {
    this.userRouteSub = this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.closeMenu.emit(true);
      }
    });

    this.profileService.profile$.pipe(take(1)).subscribe(val => {
      this.profile = val;
    });
  }

  @Input()
  showMenu = false;

  @Output()
  closeMenu: EventEmitter<boolean> = new EventEmitter<boolean>();

  closeNavbarMobile(): void {
    this.closeMenu.emit(true);
  }

  getLinkUrl(item) {
    return generateUrl(item);
  }
}
