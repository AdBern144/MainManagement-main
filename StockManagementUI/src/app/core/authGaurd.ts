import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(): boolean | UrlTree {
    const token = localStorage.getItem('token');
    if (token) {
      // Toegang toegestaan als token aanwezig is
      return true;
    } else {
      // Geen token, dus doorverwijzen naar login pagina
      return this.router.createUrlTree(['/login']);
    }
  }
}