import { SimCard } from './../Models/SimCard';
import { Component, OnInit } from '@angular/core';
import { SimCardServiceService } from '../Services/simCardService/sim-card-service.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-simcard',
  templateUrl: './add-simcard.component.html',
  styleUrls: ['./add-simcard.component.css']
})
export class AddSimcardComponent implements OnInit {

  constructor(private simCardService: SimCardServiceService,private toastService : ToastrService) { }

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
                                    this.simCardService.form.value.puk
                                     );      
       this.simCardService.post(this.simcard).subscribe(
        response => {
             console.log(response);
        },
        err => {
             console.log(err);
        },
        () => {
          this.toastService.success('Inserted successfully','X-Communications');}); 

     }
     this.submitted=false;
       this.simCardService.form.reset();

   }
}
