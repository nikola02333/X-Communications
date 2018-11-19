import { Component, OnInit } from '@angular/core';
import { ContractService } from '../Services/contractService/contract.service';
import { Contract } from '../Models/contract';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-contract',
  templateUrl: './add-contract.component.html',
  styleUrls: ['./add-contract.component.css']
})
export class AddContractComponent implements OnInit {

  constructor( private contractService: ContractService) { }

  selectedContract: Contract;

  ngOnInit() {
  }

  onSubmit(form:NgForm)
  {
    this.contractService.postContract(form.value);
  }
}
