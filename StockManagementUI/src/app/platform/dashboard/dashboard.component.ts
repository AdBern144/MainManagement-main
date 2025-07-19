import { Component, OnInit } from '@angular/core';
import { UserService } from '../../core/services/user.service';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {

  public user: any;
  public tokenTimeValid: number = 0;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.user = this.userService.getUser();
    this.tokenTimeValid = this.userService.getTokenTimeValid();
  }
}