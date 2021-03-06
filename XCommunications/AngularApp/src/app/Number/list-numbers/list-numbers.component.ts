import { Component, OnInit } from '@angular/core';
import { Number } from '../../Models/Number';
import { ListNumbersService } from '../../Services/numberService/list-numbers.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-list-numbers',
  templateUrl: './list-numbers.component.html',
  styleUrls: ['./list-numbers.component.css']
})
export class ListNumbersComponent implements OnInit {

  constructor(private serviceNumber:ListNumbersService,private toastService : ToastrService ) { }
 
  private  listNumbers:  Array<object> = [];  
  ngOnInit() {
    this.getListNumbers();

  }

  selectedNumber : Number;

  getListNumbers()
  {
    this.serviceNumber.getAllNumbers().subscribe(
  
      (data:  Array<object>) => {
      this.listNumbers  =  data;
      if(this.listNumbers.length==0)
      {
        this.toastService.info('There are no numbers');
      }
      console.log(data);
  });
  }
    onSelect(number: Number): void {
      this.selectedNumber = number;
    }
  
        onClickDelete()
    {
      this.serviceNumber.deleteNumber(this.selectedNumber.id).subscribe( 
        response => {
        console.log(response);
    },
    err=> {
      this.toastService.error("Something went wrong");
    },
    () => {
      this.toastService.info("number deleted successfully ");
      this.getListNumbers();
    }
  
   );
    }
    onClickEdit()
    {
      this.serviceNumber.updateNumber(this.selectedNumber).subscribe( 
        response => {
        console.log(response);
    },
    err=> {
      this.toastService.error("Something went wrong");
    },
    () => {
      this.toastService.info("number edited successfully ");
      this.getListNumbers();
    }
  
   );
    }
}
