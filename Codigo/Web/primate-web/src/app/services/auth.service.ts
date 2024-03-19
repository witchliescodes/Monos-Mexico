import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Ilogin } from '../interfaces/ilogin';
import { LoginAuth } from '../interfaces/login-auth';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, Subscription, delay, map, of, tap } from 'rxjs';
import { Iregister } from '../interfaces/iregister';
import { UserToken } from '../guards/auth.guard';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  private timer: Subscription = new Subscription;
  private _user = new BehaviorSubject<LoginAuth | undefined>(undefined);
  user$: Observable<LoginAuth | undefined> = this._user.asObservable();
  private userName: string = '';


  constructor
    (private router: Router,
      private http: HttpClient
    ) { }

  login(request: Ilogin): Observable<LoginAuth> {

    return this.http
      .post<LoginAuth>(`${environment.apiUri}Auth/Login`, request, this.httpOptions)
      .pipe(map((result) => {
        this.userName = result.userName;
        this._user.next(result);
        this.setLocalStorage(result);
        this.startTokenTimer();
        return result;
      }));
  }

  register(register: Iregister): Observable<LoginAuth> {

    return this.http
      .post<LoginAuth>(`${environment.apiUri}Auth/Register`, register, this.httpOptions)
      .pipe(
        map((result) => {
          return result;
        })
      );
  }

  canActivate(token: UserToken, route: any): boolean | import("@angular/router").UrlTree
    | Observable<boolean
      | import("@angular/router").UrlTree>
    | Promise<boolean | import("@angular/router").UrlTree> {
    return this.user$.pipe(
      map((user) => {
        if (user) {
          return true;
        } else {
          this.router.navigate(['login']);
          return false;
        }
      })
    );
  }

  // miembros privados
  private storageEventListener(event: StorageEvent) {
    if (event.storageArea === localStorage) {
      if (event.key === 'logout-event') {
        this.stopTokenTimer();
        this._user.next(undefined);
      }
      if (event.key === 'login-event') {
        this.stopTokenTimer();
        this.http.get<LoginAuth>(`${environment.apiUri}user`).subscribe((x) => {
          this._user.next(x);
        });
      }
    }
  }

  setLocalStorage(x: LoginAuth) {
    localStorage.setItem('access_token', x.accesToken);
    localStorage.setItem('refresh_token', x.refreshToken);
    localStorage.setItem('login-event', 'login' + Math.random());
  }

  clearLocalStorage() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.setItem('logout-event', 'logout' + Math.random());
  }

  private getTokenRemainingTime() {
    const accessToken = localStorage.getItem('access_token');
    if (!accessToken) {
      return 0;
    }
    const jwtToken = JSON.parse(atob(accessToken.split('.')[1]));
    const expires = new Date(jwtToken.exp * 1000);
    return expires.getTime() - Date.now();
  }

  private startTokenTimer() {
    const timeout = this.getTokenRemainingTime();
    this.timer = of(true)
      .pipe(
        delay(timeout),
        tap(() => this.refreshToken())
      )
      .subscribe();
  }

  private stopTokenTimer() {
    this.timer?.unsubscribe();
  }

  refreshToken() {
    const refreshToken = localStorage.getItem('refresh_token');
    if (!refreshToken) {
      this.clearLocalStorage();
      return of(null);
    }

    return this.http
      .post<LoginAuth>(`${environment.apiUri}refresh-token`, { refreshToken })
      .pipe(
        map((response) => {
          this._user.next(response);
          this.setLocalStorage(response);
          this.startTokenTimer();
          return response;
        })
      );
  }
  // miembros privados
}
