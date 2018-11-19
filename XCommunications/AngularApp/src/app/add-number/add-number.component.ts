import { Component, OnInit } from '@angular/core';
import { ListNumbersService } from '../Services/numberService/list-numbers.service';

import {Number} from "src/app/Models/Number";
@Component({
  selector: 'app-add-number',
  templateUrl: './add-number.component.html',
  styleUrls: ['./add-number.component.css']
})
export class AddNumberComponent implements OnInit {

  constructor(private  numberService: ListNumbersService) { }
  number : Number;
  submitted: boolean;
  showSuccessMessage: boolean;
  formControls = this.numberService.form.controls;

  ngOnInit() {
  }

  onSubmit()
  {
     this.submitted = true;
     if (this.numberService.form.valid)
     {
       this.number = new Number(1,true,this.numberService.form.value.cc,
                                    this.numberService.form.value.ndc,
                                    this.numberService.form.value.sn
                                     ); // false = free
     
       this.numberService.postNumber(this.number).subscribe( x=> console.log(x)); //

     }
   }
}
