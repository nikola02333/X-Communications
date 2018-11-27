// import { TestBed, async, ComponentFixture } from '@angular/core/testing';
// import { BrowserModule, By } from '@angular/platform-browser';
// import {} from 'jasmine';
// import { DebugElement } from '@angular/core';
// import { SimCardServiceService } from '../../Services/simCardService/sim-card-service.service';
// import { ReactiveFormsModule, FormsModule } from '@angular/forms';
// import { ListSimCardsComponent } from '../../SimCard/list-sim-cards/list-sim-cards.component';
// import { SimcardServiceMock } from '../../Mocks/simcard.service.mock';
// import { HttpClient, HttpClientModule } from '@angular/common/http';
// import { ToastrModule } from 'ngx-toastr';

// describe('ListSimCardsComponent', () => {
//     let comp: ListSimCardsComponent;
//     let fixture: ComponentFixture<ListSimCardsComponent>;
//     let debug: DebugElement;
//     let element: HTMLElement;

//     beforeEach(async(() => {
//         TestBed.configureTestingModule({
//           declarations: [
//             ListSimCardsComponent
//           ],
//           imports: [
//             ReactiveFormsModule,
//             FormsModule,
//             BrowserModule,
//             HttpClientModule,
//             ToastrModule.forRoot()
//           ],
//           providers: [
//             { provide: SimCardServiceService, useClass: SimcardServiceMock }
//           ]
//         }).compileComponents().then(() => {
//             fixture = TestBed.createComponent(ListSimCardsComponent);
//             comp = fixture.componentInstance;
//             debug = fixture.debugElement.query(By.css('#form'));
//             element = debug.nativeElement;
//         });
//     }));

//     it(`should have one card`, async(() => {
//         expect(comp.cards.length).toEqual(0);
//     }));

//     // this.simCardService.getAllSimCards().subscribe is not a function
//     it(`should have edited customer`, async(() => {
//         fixture.detectChanges();
//         spyOn(comp, 'onClickUpdate');
//         element=fixture.debugElement.query(By.css(`#update`)).nativeElement;
//         element.click();
//         expect(comp.onClickUpdate).toHaveBeenCalledTimes(1);
//     }));

//     // this.simCardService.getAllSimCards().subscribe is not a function
//     it(`should have one deleted customer`, async(() => {
//         fixture.detectChanges();
//         spyOn(comp, 'onClickDelete');
//         element=fixture.debugElement.query(By.css(`#delete`)).nativeElement;
//         element.click();
//         expect(comp.onClickDelete).toHaveBeenCalledTimes(1);
//     }));
// });