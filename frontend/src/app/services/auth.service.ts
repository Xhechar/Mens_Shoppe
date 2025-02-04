import { Injectable } from '@angular/core';
import { AuthServices } from '../interfaces/auth_service';
import { HttpClient } from '@angular/common/http';
import {
  UserDetails,
  UserLogin,
  UserPasswords,
  UserRegister,
} from '../interfaces/auth';
import { Observable } from 'rxjs';
import { Res } from '../interfaces/res';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements AuthServices {
  private api: string = 'http://localhost:3000/auth';

  constructor(private http: HttpClient) {}

  register(
    user_register: UserRegister
  ): Observable<Res<{ role: 'user' | 'admin'; token: string } | null>> {
    return this.http.post<
      Res<{ role: 'user' | 'admin'; token: string } | null>
    >(`${this.api}/register`, user_register);
  }

  login(
    user_login: UserLogin
  ): Observable<Res<{ role: 'user' | 'admin'; token: string } | null>> {
    return this.http.post<
      Res<{ role: 'user' | 'admin'; token: string } | null>
    >(`${this.api}/login`, user_login);
  }

  updateDetails(user_details: UserDetails): Observable<Res<null>> {
    return this.http.post<Res<null>>(
      `${this.api}/update_details`,
      user_details
    );
  }

  updatePassword(user_passwords: UserPasswords): Observable<Res<null>> {
    return this.http.post<Res<null>>(
      `${this.api}/update_password`,
      user_passwords
    );
  }
}
