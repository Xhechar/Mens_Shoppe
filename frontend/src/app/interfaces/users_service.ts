import { Observable } from "rxjs";
import { Res } from "./res";
import { User } from "./user";

export interface UsersServices {
  getUsers(): Observable<Res<User[] | null>>;
  getUser(id: string): Observable<Res<User | null>>;
  isAdmin(id: string): Observable<Res<boolean>>;
}
