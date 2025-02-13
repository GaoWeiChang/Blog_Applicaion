import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/features/blog-post/auth/models/user.model';
import { AuthService } from 'src/app/features/blog-post/auth/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent {

  user?: User;
  constructor(private authService: AuthService,
    private router: Router) {
  }
}
