import { Component } from '@angular/core';
import { UserServiceService } from '../app/Services/userService/user-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private server :UserServiceService) {
  
    }
   
  title = 'App';
}
