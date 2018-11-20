import { Component, OnInit } from '@angular/core';
import { ListNumbersService } from '../Services/numberService/list-numbers.service';

import { Number } from "src/app/Models/Number";
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-add-number',
  templateUrl: './add-number.component.html',
  styleUrls: ['./add-number.component.css']
})
export class AddNumberComponent implements OnInit {

  constructor(private numberService: ListNumbersService, private toastService: ToastrService) { }
  number: Number;
  submitted: boolean;
  showSuccessMessage: boolean;
  formControls = this.numberService.form.controls;

  ngOnInit() {
  }

  onSubmit() {
    this.submitted = true;

    if (this.numberService.form.valid) {
      this.number = new Number(this.numberService.form.value.id, false, this.numberService.form.value.cc, this.numberService.form.value.ndc, this.numberService.form.value.sn);

      this.numberService.postNumber(this.number).subscribe(
        response => {
          console.log(response);
        },
        err => {
          console.log(err);
        },
        () => {
          this.toastService.success('Inserted successfully', 'X-Communications');
        });
    }
    this.submitted = false;
    this.numberService.form.reset();
  }

}
