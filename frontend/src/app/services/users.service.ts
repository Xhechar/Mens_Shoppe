import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UsersServices } from '../interfaces/users_service';
import { Observable } from 'rxjs';
import { Res } from '../interfaces/res';
import { User } from '../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class UsersService implements UsersServices {
  api: string = 'http://localhost:3000/users';
  headers = new HttpHeaders({
    'Authorization': localStorage.getItem('authToken') || ""
  });
  constructor(private http: HttpClient) { }
  isAdmin(id: string): Observable<Res<boolean>> {
    throw new Error('Method not implemented.');
  }

  getUsers(): Observable<Res<User[] | null>> {
    return this.http.get<Res<User[] | null>>(`${this.api}/all`, { headers: this.headers });
  }

  getUser(userId: string): Observable<Res<User | null>> {
    return this.http.get<Res<User | null>>(`${this.api}/${userId}`, { headers: this.headers });
  }
}
