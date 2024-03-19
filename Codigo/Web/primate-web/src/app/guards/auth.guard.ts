import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { LoginAuth } from '../interfaces/login-auth';

@Injectable({
  providedIn: 'root'
})
export class UserToken   {

  canActivate(currentUser: LoginAuth, userId: string): boolean {
    return true;
  }
  canMatch(currentUser: LoginAuth): boolean {
    return true;
  }  
}

const canActivateTeam: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot,
) => {
  return inject(AuthService).canActivate(inject(UserToken), route.params['id']);
};
