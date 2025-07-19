import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavigationComponent } from './navigation/navigation.component';
import { HeaderComponent } from "./header/header.component";

@Component({
  selector: 'app-platform',
  imports: [RouterOutlet, NavigationComponent, HeaderComponent],
  templateUrl: './platform.component.html',
  styleUrl: './platform.component.scss'
})
export class PlatformComponent {

}
