import { Component, OnInit } from '@angular/core';
import { ContractService } from '../../Services/contractService/contract.service';
import { Contract } from '../../Models/Contract';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-list-all-contract',
  templateUrl: './list-all-contract.component.html',
  styleUrls: ['./list-all-contract.component.css']
})
export class ListAllContractComponent implements OnInit {

  constructor(private serviceContract: ContractService, private toastService : ToastrService) { }

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
        this.serviceContract.deleteContract(this.selectedContract.id).subscribe();

      
        this.getListContract();
        if(this.listContract.length==0)
        {
          this.toastService.info('There are no Contract left');
        }
        
      }
      onClickEdit()
      {
        this.serviceContract.updateContract(this.selectedContract=this.selectedContract).subscribe();
        this.getListContract();
      }

}
