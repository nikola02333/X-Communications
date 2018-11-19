import { Component, OnInit } from '@angular/core';
import { SimCard } from '../Models/SimCard';
import { SimCardServiceService } from '../Services/simCardService/sim-card-service.service';

@Component({
  selector: 'app-list-sim-cards',
  templateUrl: './list-sim-cards.component.html',
  styleUrls: ['./list-sim-cards.component.css']
})
export class ListSimCardsComponent implements OnInit {

  constructor(private simCardService: SimCardServiceService ) { }

  selectedsimCard : SimCard;

  simCards : SimCard[] =[
    new SimCard(11,12,1234,1111,true),
    new SimCard(11,12,1234,2222,true)
  ];

  onSelect(simCard: SimCard): void {
    this.selectedsimCard = simCard;
  }
  onClickDelete()
  {
    this.simCardService.deleteSimCard(this.selectedsimCard).subscribe();
  }
  onClickUpdate()
  {
    this.simCardService.updateSimCard(this.selectedsimCard).subscribe();
  }
  ngOnInit() {
  }

}
