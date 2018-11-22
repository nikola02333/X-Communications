import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { ContractService } from '../../Services/contractService/contract.service';
import { Contract } from '../../Models/Contract';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-contract',
  templateUrl: './add-contract.component.html',
  styleUrls: ['./add-contract.component.css']
})
export class AddContractComponent implements OnInit {

  constructor( private contractService: ContractService,private toastService : ToastrService) { }

  submitted: boolean;
  Contract: Contract;
  formControls = this.contractService.form.controls;
  ngOnInit() {
  }
  
  onSubmit(form:NgForm)
  {
    this.submitted = true;
    if(this.contractService.form.valid)
    {
      this.Contract = new Contract(form.value.id,form.value.customerId,form.value.workerId,form.value.tariff);
      
      this.contractService.postContract(this.Contract).subscribe(
        response => {
             console.log(response);
        },
        err => {
             console.log(err);
        },
        () => {
          this.toastService.success('Inserted successfully','X-Communications');});  
          this.submitted=false;
          form.resetForm();
    }
  }
}
