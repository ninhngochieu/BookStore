import {
  Component,
  ElementRef,
  OnInit,
  ViewChild
} from '@angular/core';
import { AlertService } from 'src/app/core/services/alert.service';
import { Alert } from 'src/app/models/alert';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss'],
})
export class AlertComponent implements OnInit {
  constructor(public alertService: AlertService) {}

  data: Alert;

  ngOnInit(): void {
    this.alertService.alertStream$.subscribe((alert) => {
      if (!alert) return;
      this.data = alert;
      this.triggerClick();
    });
  }

  @ViewChild('alertButton') alertButton: ElementRef<HTMLElement>;

  triggerClick() {
    let el: HTMLElement = this.alertButton.nativeElement;
    el.click();
  }
}
