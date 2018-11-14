import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  goBack(){

    console.log('caos');
  }
  onSubmit()
  {
    console.log('pozdrav');
  }
}
