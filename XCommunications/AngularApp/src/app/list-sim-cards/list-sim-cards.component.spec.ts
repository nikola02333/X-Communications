import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSimCardsComponent } from './list-sim-cards.component';

describe('ListSimCardsComponent', () => {
  let component: ListSimCardsComponent;
  let fixture: ComponentFixture<ListSimCardsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListSimCardsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListSimCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
