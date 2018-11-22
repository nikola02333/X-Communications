import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRegistratedUserComponent } from './add-registrated-user.component';

describe('AddRegistratedUserComponent', () => {
  let component: AddRegistratedUserComponent;
  let fixture: ComponentFixture<AddRegistratedUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddRegistratedUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRegistratedUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
