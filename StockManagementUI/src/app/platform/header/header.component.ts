import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { MatTooltip } from '@angular/material/tooltip';
import {MatBadgeModule} from '@angular/material/badge';


@Component({
  selector: 'app-header',
  imports: [MatIconModule, MatButtonModule, MatTooltip, MatBadgeModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login'])
    };
  goToPersonalInfo(): void {
    this.router.navigate(['/platform/personal-info']);
  }
}