import { Res } from './res';
import { UserDetails, UserLogin, UserPasswords, UserRegister } from './auth';
import { Observable } from 'rxjs';

export interface AuthServices {
  register(
    user_register: UserRegister
  ): Observable<Res<{ role: 'user' | 'admin'; token: string } | null>>;
  login(
    user_login: UserLogin
  ): Observable<Res<{ role: 'user' | 'admin'; token: string } | null>>;
  updateDetails(user_details: UserDetails): Observable<Res<null>>;
  updatePassword(user_passwords: UserPasswords): Observable<Res<null>>;
}
