import { Component } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { LoginRequestModel } from './login-reqest.model';

@Component({
  selector: 'app-auth.component',
  standalone: true,
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css',
  imports: [FormsModule]
})
export class AuthComponent {
  public request: LoginRequestModel = new LoginRequestModel();
  constructor(private service: AuthService,
              private router: Router
  ) {}

  clickLogin(): void {
    this.service.login(this.request)
    .subscribe({
              next: (response: HttpResponse<any>) => {
                if (response && response.body.token) {
                  sessionStorage.setItem('auth_token', response.body.token);
                  this.router.navigate(['/index']);
                }
              }
            })
  }
}
