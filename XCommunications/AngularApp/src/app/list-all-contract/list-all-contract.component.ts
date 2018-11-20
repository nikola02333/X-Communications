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
    this.getListContract();
  }
  private  listContract:  Array<object> = [];  


  getListContract()
{
  this.serviceContract.getAllContract().subscribe(

    (data:  Array<object>) => {

    this.listContract  =  data;
    console.log(data);
});
}
  onSelect(contract: Contract): void {
    this.selectedContract = contract;
  }

      onClickDelete()
      {
        debugger
        this.serviceContract.deleteContract(this.selectedContract.id).subscribe();
        this.getListContract();
      }
      onClickEdit()
      {
        this.serviceContract.updateContract(this.selectedContract=this.selectedContract).subscribe();
        this.getListContract();
      }

}
