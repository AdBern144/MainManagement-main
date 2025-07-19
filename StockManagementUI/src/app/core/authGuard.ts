import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { jwtDecode } from 'jwt-decode'; // Zorg ervoor dat je de jwt-decode bibliotheek hebt geïnstalleerd
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(): boolean | UrlTree {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = jwtDecode(token) as { exp: number };
      const currentTime = Math.floor(Date.now() / 1000); // Huidige tijd in seconden

      if (decodedToken.exp && decodedToken.exp < currentTime) {
        localStorage.removeItem('token'); // Token is verlopen, verwijder het
        return this.router.createUrlTree(['/login']); // Doorverwijzen naar login pagina
      }
      // Toegang toegestaan als token aanwezig is
      return true;
    } else {
      // Geen token, dus doorverwijzen naar login pagina
      return this.router.createUrlTree(['/login']);
    }
  }
}