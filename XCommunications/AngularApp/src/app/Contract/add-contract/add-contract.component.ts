import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { ContractService } from '../../Services/contractService/contract.service';
import { Contract } from '../../Models/Contract';
import { NgForm } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-add-contract',
  templateUrl: './add-contract.component.html',
  styleUrls: ['./add-contract.component.css']
})
export class AddContractComponent implements OnInit {

  constructor( private contractService: ContractService,
                private toastService : ToastrService,
                private route:ActivatedRoute,
                private router:Router) { }

  submitted= false;
  valid = false;
  Contract: Contract;
  formControls =  this.contractService.form.controls;

  ngOnInit() {
    this.formControls = this.contractService.form.controls;
  }
  
  onSubmit()
  {

    this.submitted = true;

    this.validate();
    this.makeInstance();
    this.postNumber();

    this.submitted = true;
    
    
  }
  makeInstance()
  {
     if(this.valid)
     {
       this.Contract = new Contract(  this.contractService.form.value.id,
                                      this.contractService.form.value.customerId,
                                      this.contractService.form.value.workerId,
                                      this.contractService.form.value.tarif,
                                );}
  }
  postNumber()
  {
    this.contractService.postContract(this.Contract).subscribe(
      response => {
        console.log(response);
      },
      err=> {
        console.log(err);
      },
      ()=>{
        this.toastService.success('Inserted successfully','X-Communications');
      }); 
  }
  validate()
  {
    if( this.formControls.valid)
    {
      this.valid=true;
    }
  }
}
