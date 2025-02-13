import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ObservableLike } from 'rxjs';
import { LoginRequest, LoginResponse, User } from '../models/user.model';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient,
              private cookieService: CookieService) { }

  // method ที่คืนค่าแปลงเป็น Observable ของ User หรือ undefined
  user() : Observable<User | undefined> {
    return this.$user.asObservable(); // ทำให้ผู้ใช้สามารถติดตามการเปลี่ยนแปลงของ user ได้ แต่ไม่สามารถเปลี่ยนแปลงค่าได้โดยตรง (read-only) ซึ่งเป็นการปกป้องข้อมูลที่ดี
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/auth/login`, {
        email: request.email,
        password: request.password
      });
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete("Authorization", '/');
    this.$user.next(undefined);
  }
  
  setUser(user: User): void {
    this.$user.next(user); // update value in BehaviorSubject
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-roles', user.roles.join(',')); // ('key', value)
  }

  getUser(): User | undefined {
    const email = localStorage.getItem('user-email');
    const roles = localStorage.getItem('user-roles');

    if (email && roles) {
      const user: User = {
        email: email,
        roles: roles?.split(',')
      };

      return user;
    }
    return undefined;
  }
}
