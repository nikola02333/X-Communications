import { Component } from '@angular/core';
import { UserServiceService } from './Services/user-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private server :UserServiceService) {
  
    }
    /*onSave()
    {
      this.server.addUser
    }*/
  title = 'App';
}
