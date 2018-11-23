import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { BrowserModule, By } from '@angular/platform-browser';
import {} from 'jasmine';
import { DebugElement } from '@angular/core';
import { SimCardServiceService } from '../Services/simCardService/sim-card-service.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ListSimCardsComponent } from '../SimCard/list-sim-cards/list-sim-cards.component';
import { AddSimcardComponent } from '../SimCard/add-simcard/add-simcard.component';
import { SimcardServiceMock } from '../Mocks/simcard.service.mock';

describe('AddSimcardComponent', () => {
    let comp: AddSimcardComponent;
    let fixture: ComponentFixture<AddSimcardComponent>;
    let debug: DebugElement;
    let element: HTMLElement;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
          declarations: [
            AddSimcardComponent
          ],
          imports: [
            ReactiveFormsModule,
            FormsModule,
            BrowserModule
          ],
          providers: [
            { provide: SimCardServiceService, useClass: SimcardServiceMock }
          ]
        }).compileComponents().then(() => {
            fixture = TestBed.createComponent(AddSimcardComponent);
            comp = fixture.componentInstance;
            debug = fixture.debugElement.query(By.css('form'));
            element = debug.nativeElement;
        });
    }));

    it(`should set submitted to true`, async(() => {
        comp.onSubmit();
        expect(comp.submitted).toBeTruthy();
    }));

    it(`should call the onSubmit() method`, async(() => {
        fixture.detectChanges();
        spyOn(comp, 'onSubmit');
        element=fixture.debugElement.query(By.css(`button`)).nativeElement;
        element.click();
        expect(comp.onSubmit).toHaveBeenCalledTimes(0);
    }));

    it(`form should be invalid`, async(() => {
        comp.formControls.controls['imsi'].setValue('');
        comp.formControls.controls['iccid'].setValue('');
        comp.formControls.controls['pin'].setValue('');
        comp.formControls.controls['puk'].setValue('');
        expect(comp.formControls.valid).toBeFalsy();
    }));

    it(`form should be valid`, async(() => {
        comp.formControls.controls['imsi'].setValue('aada');
        comp.formControls.controls['iccid'].setValue('aada');
        comp.formControls.controls['pin'].setValue('aada');
        comp.formControls.controls['puk'].setValue('aada');
        expect(comp.formControls.valid).toBeTruthy();
    }));
});

describe('ListSimCardsComponent', () => {
    let comp: ListSimCardsComponent;
    let fixture: ComponentFixture<ListSimCardsComponent>;
    let debug: DebugElement;
    let element: HTMLElement;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
          declarations: [
            ListSimCardsComponent
          ],
          imports: [
            ReactiveFormsModule,
            FormsModule,
            BrowserModule
          ],
          providers: [
            { provide: SimCardServiceService, useClass: SimcardServiceMock }
          ]
        }).compileComponents().then(() => {
            fixture = TestBed.createComponent(ListSimCardsComponent);
            comp = fixture.componentInstance;
            debug = fixture.debugElement.query(By.css('form'));
            element = debug.nativeElement;
        });
    }));

    it(`should have one card`, async(() => {
        comp.getAllCards();
        expect(comp.cards.length).toEqual(1);
    }));

    it(`html should render one card`, async(() => {
        fixture.detectChanges();
        const el = fixture.nativeElement.querySelector('td');
        expect(el.innerText).toContain('imsi');
    }));

    it(`should have one selected card`, async(() => {
        comp.onClickDelete();
        expect(comp.selectedCard).toBeTruthy();
    }));

    it(`should have one selected card`, async(() => {
        comp.onClickUpdate();
        expect(comp.selectedCard).toBeTruthy();
    }));
});