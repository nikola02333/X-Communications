import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { BrowserModule, By } from '@angular/platform-browser';
import {} from 'jasmine';
import { DebugElement } from '@angular/core';
import { SimCardServiceService } from '../../Services/simCardService/sim-card-service.service';
import { ReactiveFormsModule, FormsModule, FormGroup } from '@angular/forms';
import { AddSimcardComponent } from '../../SimCard/add-simcard/add-simcard.component';
import { SimcardServiceMock } from '../../Mocks/simcard.service.mock';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';


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
            FormsModule,
            ReactiveFormsModule,
            BrowserModule,
            HttpClientModule,
            ToastrModule.forRoot()
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

    it(`should have a defined variable submitted`, () => {
        expect(comp.submitted).toBeDefined();
    });

    it(`should have a defined variable valid`, () => {
        expect(comp.valid).toBeDefined();
    });

    it(`should have a defined variable formControls`, () => {
        expect(comp.formControls).toBeDefined();
    });

    it(`should set valid to true`, async(() => {
        comp.validate();
        expect(comp.valid).toBeTruthy();
    }));

    it(`should validate form`, async() => {
        spyOn(comp, 'validate');
        comp.validate();
        expect(comp.validate).toHaveBeenCalledTimes(1);
    });

    it(`should make instance of SimCard`, async() => {
        spyOn(comp, 'makeInstance');
        comp.makeInstance();
        expect(comp.makeInstance).toHaveBeenCalledTimes(1);
    });

    it(`should post SimCard`, async() => {
        spyOn(comp, 'postSimCard');
        comp.postSimCard();
        expect(comp.postSimCard).toHaveBeenCalledTimes(1);
    });

    it(`should call onSubmit`, async() => {
        spyOn(comp, 'onSubmit');
        comp.onSubmit();
        expect(comp.onSubmit).toHaveBeenCalledTimes(1);
    });
});