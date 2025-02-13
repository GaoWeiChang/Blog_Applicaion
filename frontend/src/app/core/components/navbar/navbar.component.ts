import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/features/blog-post/auth/models/user.model';
import { AuthService } from 'src/app/features/blog-post/auth/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {

  user?: User;
  constructor(private authService: AuthService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.authService.user()
      .subscribe({
        next: (response) => {
          this.user = response
        }
      })

      // when web refreshed, make sure user still log in
      this.user = this.authService.getUser();
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }  
}
