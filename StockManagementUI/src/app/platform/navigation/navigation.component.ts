import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../core/services/user.service';
import { User } from '../../core/dataContracts/userModel';

@Component({
  selector: 'app-navigation',
  imports: [RouterModule],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss',
  standalone: true
})
export class NavigationComponent {
  public user: User;

  constructor(private router: Router, private userService: UserService)
  { 
    this.user = this.userService.getUser();
  }

  public loggoff(): void {
    window.localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}