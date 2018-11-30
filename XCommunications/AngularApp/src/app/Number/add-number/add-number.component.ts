import { Component, OnInit } from '@angular/core';
import { ListNumbersService } from '../../Services/numberService/list-numbers.service';
import { Number } from "src/app/Models/Number";
import { ToastrService } from 'ngx-toastr';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-add-number',
  templateUrl: './add-number.component.html',
  styleUrls: ['./add-number.component.css']
})
export class AddNumberComponent implements OnInit {

  constructor(private numberService: ListNumbersService, 
              private toastService: ToastrService,
              private route:ActivatedRoute,
              private router:Router) { }
  number: Number;
  submitted= false;
  valid = false;
  formControls = this.numberService.form.controls;



  ngOnInit() {
   // this.formControls = this.numberService.form.controls;
  }

  onSubmit() {
    this.submitted = true;

    this.validate();
    this.makeInstance();
    this.postNumber();
  
  }
validate()
  {
    
    if( this.numberService.form.valid)
    {
      this.valid=true;
    }
  }
   makeInstance()
    {
       if(this.numberService.form.valid)
       {
        this.number = new Number( this.numberService.form.value.id,
          false,
          this.numberService.form.value.cc,
          this.numberService.form.value.ndc,
          this.numberService.form.value.sn );
       }
    }
    postNumber()
    {
      if (this.valid) {
      this.numberService.postNumber(this.number).subscribe(
        response => {
          console.log(response);
        },
        err=> {
          console.log(err);
        },
        ()=>{
          this.toastService.success('Inserted successfully','X-Communications');
        }); 
      this.submitted = false;
      this.numberService.form.reset();
      }
    }
}


