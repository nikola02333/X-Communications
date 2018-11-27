import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { BrowserModule, By } from '@angular/platform-browser';
import {} from 'jasmine';
import { DebugElement } from '@angular/core';
import { UserServiceService } from '../../Services/user-service.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AddUserComponent } from '../../User/add-user/add-user.component';
import { UserServiceMock } from '../../Mocks/user.service.mock';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';

describe('AddUserComponent', () => {
    let comp: AddUserComponent;
    let fixture: ComponentFixture<AddUserComponent>;
    let debug: DebugElement;
    let element: HTMLElement;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
          declarations: [
            AddUserComponent
          ],
          imports: [
            FormsModule,
            ReactiveFormsModule,
            BrowserModule,
            HttpClientModule,
            ToastrModule.forRoot()
          ],
          providers: [
            { provide: UserServiceService, useClass: UserServiceMock }
          ]
        }).compileComponents().then(() => {
            fixture = TestBed.createComponent(AddUserComponent);
            comp = fixture.componentInstance;
            debug = fixture.debugElement.query(By.css('form'));
            element = debug.nativeElement;
        });
    }));

    it(`should set submitted to true`, async(() => {
        comp.onSubmit();
        expect(comp.submitted).toBeTruthy();
    }));

    it(`form should be invalid`, async(() => {
        comp.formControls['fullName'].setValue('');
        comp.formControls['lastname'].setValue('');
        comp.formControls['id'].setValue('');
        expect(!comp.formControls.valid).toBeTruthy();
    }));

    it(`should call the onSubmit() method`, async(() => {
        fixture.detectChanges();
        spyOn(comp, 'onSubmit');
        element=fixture.debugElement.query(By.css(`#saveUser`)).nativeElement;
        element.click();
        expect(comp.onSubmit).toHaveBeenCalledTimes(1);
    }));

    it(`form should be valid`, async(() => {
        comp.formControls['fullName'].setValue('aada');
        comp.formControls['lastname'].setValue('aada');
        comp.formControls['id'].setValue('aada');
        expect(comp.formControls.valid).toBeFalsy();
    }));
});