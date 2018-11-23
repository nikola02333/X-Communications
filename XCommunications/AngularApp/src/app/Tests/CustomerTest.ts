import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { BrowserModule, By } from '@angular/platform-browser';
import {} from 'jasmine';
import { DebugElement } from '@angular/core';
import { UserServiceService } from '../Services/user-service.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ListAllUsersComponent } from '../User/list-all-users/list-all-users.component';
import { AddUserComponent } from '../User/add-user/add-user.component';
import { UserServiceMock } from '../Mocks/user.service.mock';

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
            ReactiveFormsModule,
            FormsModule,
            BrowserModule
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

    it(`should call the onSubmit() method`, async(() => {
        fixture.detectChanges();
        spyOn(comp, 'onSubmit');
        element=fixture.debugElement.query(By.css(`button`)).nativeElement;
        element.click();
        expect(comp.onSubmit).toHaveBeenCalledTimes(0);
    }));

    it(`form should be invalid`, async(() => {
        comp.formControls.controls['fullName'].setValue('');
        comp.formControls.controls['lastname'].setValue('');
        comp.formControls.controls['id'].setValue('');
        expect(comp.formControls.valid).toBeFalsy();
    }));

    it(`form should be valid`, async(() => {
        comp.formControls.controls['fullName'].setValue('aada');
        comp.formControls.controls['lastname'].setValue('aada');
        comp.formControls.controls['id'].setValue('aada');
        expect(comp.formControls.valid).toBeTruthy();
    }));
});

describe('ListAllUsersComponent', () => {
    let comp: ListAllUsersComponent;
    let fixture: ComponentFixture<ListAllUsersComponent>;
    let debug: DebugElement;
    let element: HTMLElement;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
          declarations: [
            ListAllUsersComponent
          ],
          imports: [
            ReactiveFormsModule,
            FormsModule,
            BrowserModule
          ],
          providers: [
            { provide: UserServiceService, useClass: UserServiceMock }
          ]
        }).compileComponents().then(() => {
            fixture = TestBed.createComponent(ListAllUsersComponent);
            comp = fixture.componentInstance;
            debug = fixture.debugElement.query(By.css('form'));
            element = debug.nativeElement;
        });
    }));

    it(`should have one customer`, async(() => {
        comp.getAllUsers();
        expect(comp.users.length).toEqual(1);
    }));

    it(`html should render one customer`, async(() => {
        fixture.detectChanges();
        const el = fixture.nativeElement.querySelector('td');
        expect(el.innerText).toContain('id');
    }));

    it(`should have one selected customer`, async(() => {
        comp.onClickDelete();
        expect(comp.selectedUser).toBeTruthy();
    }));

    it(`should have one selected customer`, async(() => {
        comp.onClickEdit();
        expect(comp.selectedUser).toBeTruthy();
    }));
});