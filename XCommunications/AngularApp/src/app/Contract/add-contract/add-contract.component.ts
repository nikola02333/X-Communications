import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { ContractService } from '../../Services/contractService/contract.service';
import { Contract } from '../../Models/Contract';
import { NgForm } from '@angular/forms';
import { FormGroup } from '@angular/forms';
@Component({
  selector: 'app-add-contract',
  templateUrl: './add-contract.component.html',
  styleUrls: ['./add-contract.component.css']
})
export class AddContractComponent implements OnInit {

  constructor( private contractService: ContractService,private toastService : ToastrService) { }

  submitted= false;
  valid = false;
  Contract: Contract;
  formControls = new FormGroup({});
  
  ngOnInit() {
    this.formControls = this.contractService.form;
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
       this.Contract = new Contract(  this.formControls.value.id,
                                   this.formControls.value.customerId,
                                   this.formControls.value.workerId,
                                   this.formControls.value.tarif,
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
