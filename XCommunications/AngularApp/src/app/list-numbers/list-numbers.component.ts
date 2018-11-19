import { Component, OnInit } from '@angular/core';
import { Number } from '../Models/Number';
import { ListNumbersService } from '../Services/numberService/list-numbers.service';
@Component({
  selector: 'app-list-numbers',
  templateUrl: './list-numbers.component.html',
  styleUrls: ['./list-numbers.component.css']
})
export class ListNumbersComponent implements OnInit {

  constructor(private serviceNumber:ListNumbersService ) { }
 
  private  listNumbers:  Array<object> = [];  
  ngOnInit() {
    this.getListNumbers();

  }

  selectedNumber : Number;

  numbers : Number[] =[
    new Number(11,true,2,222,643800276),
    new Number(11,true,1234,2222,643800276)
  ];

 getListNumbers()
{
  this.serviceNumber.getAllNumbers().subscribe(

    (data:  Array<object>) => {

    this.listNumbers  =  data;
    console.log(data);
});

}
  onSelect(number: Number): void {
    this.selectedNumber = number;
  }

  RowSelected(number:any){
    this.selectedNumber=number;
    }
    RowSelectedDelete(number:any){
     this.serviceNumber.deleteNumber(number).subscribe();
      }

}
