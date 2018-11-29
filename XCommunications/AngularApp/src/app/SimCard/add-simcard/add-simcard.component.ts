import { SimCard } from '../../Models/SimCard';
import { Component, OnInit } from '@angular/core';
import { SimCardServiceService } from '../../Services/simCardService/sim-card-service.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-add-simcard',
  templateUrl: './add-simcard.component.html',
  styleUrls: ['./add-simcard.component.css']
})
export class AddSimcardComponent implements OnInit {

  constructor(private simCardService: SimCardServiceService, private toastService: ToastrService) { }

  simcard: SimCard;
  submitted = false;
  valid = false;
  showSuccessMessage: boolean;
  formControls = new FormGroup({});

  ngOnInit() {
    this.formControls = this.simCardService.form;
  }

  onSubmit() {
    this.submitted = true;
    this.validate();
    this.makeInstance();
    this.postSimCard();
  }

  validate() {
    if (this.formControls.valid) 
    {
      this.valid = true;
    }
  }

  makeInstance(){
    if(this.valid)
    {
        this.simcard = new SimCard(this.formControls.value.imsi,
                                   this.formControls.value.iccid,
                                   this.formControls.value.pin,
                                   this.formControls.value.puk,
                                   false);
    }
  }

  postSimCard()
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
  }
}
