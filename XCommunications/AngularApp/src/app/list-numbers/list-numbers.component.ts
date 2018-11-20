import { Component, OnInit } from '@angular/core';
import { Number } from '../Models/Number';
import { ListNumbersService } from '../Services/numberService/list-numbers.service';
@Component({
  selector: 'app-list-numbers',
  templateUrl: './list-numbers.component.html',
  styleUrls: ['./list-numbers.component.css']
})
export class ListNumbersComponent implements OnInit {

  constructor(private serviceNumber: ListNumbersService) { }

  private listNumbers: Array<object> = [];
  ngOnInit() {
    this.getListNumbers();

  }

  selectedNumber: Number;


  getListNumbers() {
    this.serviceNumber.getAllNumbers().subscribe(

      (data: Array<object>) => {

        this.listNumbers = data;
        console.log(data);
      });
  }
  onSelect(number: Number): void {
    this.selectedNumber = number;
  }

  onClickDelete() {
    this.serviceNumber.deleteNumber(this.selectedNumber.id).subscribe();
    this.getListNumbers();
  }

  onClickEdit() {
    debugger
    this.serviceNumber.updateNumber(this.selectedNumber = this.selectedNumber).subscribe();
    this.getListNumbers();
  }

}
