import { Component, OnInit } from '@angular/core';
import { SimCard } from '../../Models/SimCard';
import { SimCardServiceService } from '../../Services/simCardService/sim-card-service.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-list-sim-cards',
  templateUrl: './list-sim-cards.component.html',
  styleUrls: ['./list-sim-cards.component.css']
})
export class ListSimCardsComponent implements OnInit {

  constructor(private simCardService: SimCardServiceService ,private toastService : ToastrService) { }

  cards: SimCard[]=[];
  selectedCard : SimCard;

  onSelect(simCard: SimCard): void {
    this.selectedCard = simCard;
  }

  onClickDelete()
  {
    this.simCardService.deleteSimCard(this.selectedCard).subscribe();
    this.getAllCards();
  }

  onClickUpdate()
  {
    this.simCardService.updateSimCard(this.selectedCard).subscribe();

    this.getAllCards();
    if(this.cards.length==0)
    {
      this.toastService.info('There are no Contract left');
    }
  
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
