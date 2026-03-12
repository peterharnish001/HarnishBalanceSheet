import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgxSpinnerComponent } from 'ngx-spinner';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  styleUrl: './app.component.css',
  imports: [RouterOutlet, NgxSpinnerComponent]
})
export class AppComponent implements OnInit {
  constructor(private authService: MsalService) {}

  ngOnInit(): void {
    this.authService.handleRedirectObservable().subscribe();
  }

  loginRedirect() {
    this.authService.loginRedirect();
  }

  logoutRedirect() {
    this.authService.logoutRedirect();
  }

  isLoggedIn(): boolean {
    return this.authService.instance.getAllAccounts().length > 0;
  }
}
