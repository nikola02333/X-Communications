import { element } from 'protractor';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { BrowserModule, By } from '@angular/platform-browser';
import {} from 'jasmine';
import { DebugElement } from '@angular/core';
import { ListNumbersService } from '../../Services/numberService/list-numbers.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AddNumberComponent } from '../../Number/add-number/add-number.component'
import { NumberServiceMock } from '../../Mocks/number.service.mock'
import {  HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';

describe('AddNumberComponent', ()=> {
    let comp: AddNumberComponent;
    let fixture: ComponentFixture<AddNumberComponent>;
    let debug: DebugElement;
    let element: HTMLElement; //

    beforeEach( async( ()=>{
        TestBed.configureTestingModule({
            declarations: [
                AddNumberComponent
            ],
            imports:[
                FormsModule,
                ReactiveFormsModule,
                BrowserModule,
                HttpClientModule,
                ToastrModule.forRoot()
            ],
            providers:[
             { provide: ListNumbersService, useClass: NumberServiceMock}
            ]
        }).compileComponents().then(()=> {
        fixture = TestBed.createComponent(AddNumberComponent);
        comp = fixture.componentInstance;
            debug = fixture.debugElement.query(By.css('form'));
            element = debug.nativeElement;

        });
    }));


    it('should have a defined variable submitted', ()=>{
        expect(comp.submitted).toBeDefined(false);
    });

    it(`should have a defined variable valid`, () => {
        expect(comp.valid).toBeDefined(false);
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
        
    it(`should make instance of Number`, async() => {
        spyOn(comp, 'makeInstance');
        comp.makeInstance();
        expect(comp.makeInstance).toHaveBeenCalledTimes(1);
    });
    it(`should post Number`, async() => {
        spyOn(comp, 'postNumber');
        comp.postNumber();
        expect(comp.postNumber).toHaveBeenCalledTimes(1);
    });
    it(`should call onSubmit`, async() => {
        spyOn(comp, 'onSubmit');
        comp.onSubmit();
        expect(comp.onSubmit).toHaveBeenCalledTimes(1);
    });

})