import { SimCard } from './../Models/SimCard';
import { Component, OnInit } from '@angular/core';
import { SimCardServiceService } from '../Services/simCardService/sim-card-service.service';

@Component({
  selector: 'app-add-simcard',
  templateUrl: './add-simcard.component.html',
  styleUrls: ['./add-simcard.component.css']
})
export class AddSimcardComponent implements OnInit {

  constructor(private simCardService: SimCardServiceService) { }

  simcard : SimCard;
  submitted: boolean;
  showSuccessMessage: boolean;
  formControls = this.simCardService.form.controls;

  ngOnInit() {
  }
  onSubmit()
  {
     this.submitted = true;
     if (this.simCardService.form.valid)
     {
       this.simcard = new SimCard(this.simCardService.form.value.imsi,
                                    this.simCardService.form.value.iccid,
                                    this.simCardService.form.value.pin,
                                    this.simCardService.form.value.puk,
                                     false); 
     
       this.simCardService.post(this.simcard).subscribe( x=> console.log(x)); 

     }
   }
}
