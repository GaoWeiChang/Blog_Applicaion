import { Component } from '@angular/core';
import { RegisterRequest } from '../models/user.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  model: RegisterRequest;

  constructor(private authService: AuthService,
    private router: Router
  ){ 
    this.model = {
      email: '',
      password: ''
    };
  }

  onSubmit(): void {
    this.authService.register(this.model)
      .subscribe({
        next: () => {
          // redirect back to home
          this.router.navigateByUrl('/login');
        }
      });
  }
}
