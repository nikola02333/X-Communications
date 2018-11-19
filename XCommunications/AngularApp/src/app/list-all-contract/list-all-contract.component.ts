import { Component, OnInit } from '@angular/core';
import { ContractService } from '../Services/contractService/contract.service';
import { Contract } from '../Models/contract';

@Component({
  selector: 'app-list-all-contract',
  templateUrl: './list-all-contract.component.html',
  styleUrls: ['./list-all-contract.component.css']
})
export class ListAllContractComponent implements OnInit {

  constructor(private serviceContract: ContractService) { }

  selectedContract : Contract;
  ngOnInit() {
  }
  contract : Contract[] =[
    new Contract(1,"contract1","tarriff1"),
    new Contract(2,"contract2","tarriff2")
  ];

  onSelect(contract: Contract): void {
    this.selectedContract = contract;
  }

  RowSelected(conrtact:Contract){
    this.selectedContract=  conrtact;
    }
    RowSelectedDelete(conrtact:Contract){
     this.serviceContract.deleteContract(conrtact).subscribe();
      }

}
