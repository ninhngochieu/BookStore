import { typeWithParameters } from '@angular/compiler/src/render3/util';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/modules/auth/auth.service';
import { ProfileService } from 'src/app/modules/user/profile/profile.service';
import { ProductService } from '../../product.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
})
export class CommentComponent implements OnInit, OnDestroy {
  constructor(
    public service: ProductService,
    public authService: AuthService,
    public profileService: ProfileService,
  ) {}
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  content = '';
  submitted = false;
  commentData;
  profile: any;

  @Input()
  id;

  subscription = new Subscription();

  ngOnInit(): void {
    this.service.getComments(this.id);

    this.subscription.add(
      this.service.comments$.subscribe((res) => {
        this.commentData = res;
      })
    );

    this.profileService.getProfile();

    this.subscription.add(
      this.profileService.profile$.subscribe((profile) => {
        this.profile = profile;
      })
    );
  }

  loadMore(page) {
    this.service.getComments(this.id, page);
  }

  submitForm() {
    this.submitted = true;
    var data = {
      name: this.profile.full_name,
      username: this.profile.phone,
      content: this.content,
    };
    if (this.content.length < 1) return;
    this.service.postComment(this.id, data).subscribe((res) => {
      this.submitted = false;
      if (res) {
        this.service.getComments(this.id);
      }
    });
  }
}
