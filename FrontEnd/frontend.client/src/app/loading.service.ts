import { Injectable, signal } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({ providedIn: 'root' })
export class LoadingService {
  private activeRequests = signal(0);

  constructor(private spinner: NgxSpinnerService
    ) {}

  show() {
    this.activeRequests.update(v => v + 1);
    this.spinner.show();
  }

  hide() {
    this.activeRequests.update(v => Math.max(0, v - 1));
    if (this.activeRequests() === 0) {
      this.spinner.hide();
    }
  }
}
