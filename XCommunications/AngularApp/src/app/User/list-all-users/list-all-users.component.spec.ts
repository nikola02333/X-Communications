// import { TestBed, async, ComponentFixture } from '@angular/core/testing';
// import { BrowserModule, By } from '@angular/platform-browser';
// import {} from 'jasmine';
// import { DebugElement } from '@angular/core';
// import { UserServiceService } from '../../Services/user-service.service';
// import { ReactiveFormsModule, FormsModule } from '@angular/forms';
// import { ListAllUsersComponent } from '../../User/list-all-users/list-all-users.component';
// import { UserServiceMock } from '../../Mocks/user.service.mock';
// import { HttpClient, HttpClientModule } from '@angular/common/http';
// import { ToastrModule } from 'ngx-toastr';

// describe('ListAllUsersComponent', () => {
//     let comp: ListAllUsersComponent;
//     let fixture: ComponentFixture<ListAllUsersComponent>;
//     let debug: DebugElement;
//     let element: HTMLElement;

//     beforeEach(async(() => {
//         TestBed.configureTestingModule({
//           declarations: [
//             ListAllUsersComponent
//           ],
//           imports: [
//             ReactiveFormsModule,
//             FormsModule,
//             BrowserModule,
//             HttpClientModule,
//             ToastrModule.forRoot()
//           ],
//           providers: [
//             { provide: UserServiceService, useClass: UserServiceMock }
//           ]
//         }).compileComponents().then(() => {
//             fixture = TestBed.createComponent(ListAllUsersComponent);
//             comp = fixture.componentInstance;
//             debug = fixture.debugElement.query(By.css('#form'));
//             element=debug.nativeElement;
//         });
//     }));

//     it(`should have one customer in table`, async(() => {
//         comp.getAllUsers();
//         expect(comp.users.length).toEqual(0);
//     }));

//     // cannot read property 'nativeElement' of null
//     it(`should have edited customer`, async(() => {
//         fixture.detectChanges();
//         spyOn(comp, 'onClickEdit');
//         element=fixture.debugElement.query(By.css(`#editUser`)).nativeElement;
//         element.click();
//         expect(comp.onClickEdit).toHaveBeenCalledTimes(1);
//     }));

//     // cannot read property 'nativeElement' of null
//     it(`should have one deleted customer`, async(() => {
//         fixture.detectChanges();
//         spyOn(comp, 'onClickDelete');
//         element=fixture.debugElement.query(By.css(`#deleteUser`)).nativeElement;
//         element.click();
//         expect(comp.onClickDelete).toHaveBeenCalledTimes(1);
//     }));
// });