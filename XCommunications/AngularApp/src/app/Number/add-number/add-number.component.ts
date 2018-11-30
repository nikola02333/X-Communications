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
  formControls = new FormGroup({});



  ngOnInit() {
    this.formControls = this.numberService.form;
  }

  onSubmit() {
    this.submitted = true;

    this.validate();
    this.makeInstance();
    this.postNumber();
  
  }
validate()
  {
    if( this.formControls.valid)
    {
      this.valid=true;
    }
  }
   makeInstance()
    {
       if(this.valid)
       {
         this.number = new Number( this.formControls.value.id,
                                  false,
                                  this.formControls.value.cc,
                                  this.formControls.value.ndc,
                                  this.formControls.value.sn );
       }
    }
    postNumber()
    {
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


