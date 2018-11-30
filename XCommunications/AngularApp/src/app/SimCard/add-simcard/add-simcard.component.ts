import { SimCard } from '../../Models/SimCard';
import { Component, OnInit } from '@angular/core';
import { SimCardServiceService } from '../../Services/simCardService/sim-card-service.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { disableBindings } from '@angular/core/src/render3';

@Component({
  selector: 'app-add-simcard',
  templateUrl: './add-simcard.component.html',
  styleUrls: ['./add-simcard.component.css']
})
export class AddSimcardComponent implements OnInit {

  constructor(private simCardService: SimCardServiceService, 
              private toastService: ToastrService,
              private route:ActivatedRoute,
              private router:Router) { }

  simcard: SimCard;
  submitted = false;
  valid = false;
  showSuccessMessage: boolean;
  formControls = this.simCardService.form.controls;

  ngOnInit() {
  }

  onSubmit() {

    this.submitted = true;

    if (this.simCardService.form.valid) {

    this.submitted = true;
    debugger
    this.validate();
    this.makeInstance();
    
    this.postSimCard();

    }
  }

  validate() {
    if (this.simCardService.form.valid) 
    {
      this.valid = true;
    }
  }
  makeInstance(){
    if(this.valid)
    {
          this.simcard = new SimCard(this.simCardService.form.value.imsi,
            this.simCardService.form.value.iccid,
            this.simCardService.form.value.pin,
            this.simCardService.form.value.puk,
        false);
    }
    
  }

  postSimCard()
  {
    if(this.valid)
    {
      
    this.simCardService.post(this.simcard).subscribe(
      response => {
           console.log(response);
      },
      err => {
           console.log(err);
      },
      () => {
        this.toastService.success('Inserted successfully','X-Communications');
      }); 
     this.submitted = false;
     this.simCardService.form.reset();
    }
  }
}
