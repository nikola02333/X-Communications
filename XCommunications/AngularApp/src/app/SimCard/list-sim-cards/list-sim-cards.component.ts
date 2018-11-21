import { Component, OnInit } from '@angular/core';
import { SimCard } from '../../Models/SimCard';
import { SimCardServiceService } from '../../Services/simCardService/sim-card-service.service';

@Component({
  selector: 'app-list-sim-cards',
  templateUrl: './list-sim-cards.component.html',
  styleUrls: ['./list-sim-cards.component.css']
})
export class ListSimCardsComponent implements OnInit {

  constructor(private simCardService: SimCardServiceService ) { }

  cards: SimCard[]=[];
  selectedsimCard : SimCard;

  onSelect(simCard: SimCard): void {
    this.selectedsimCard = simCard;
  }

  onClickDelete()
  {
    this.simCardService.deleteSimCard(this.selectedsimCard).subscribe();
    this.getAllCards();
  }

  onClickUpdate()
  {
    this.simCardService.updateSimCard(this.selectedsimCard).subscribe();
    this.getAllCards();
  }

  ngOnInit() {
    this.getAllCards();
  }

  getAllCards()
  {
    this.simCardService.getAllsimCards().subscribe((data:Array<SimCard>) =>
    {
      this.cards=data;
      console.log(data);
    });
  }
}
