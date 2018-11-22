import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListRegistratedUsersComponent } from './list-registrated-users.component';

describe('ListRegistratedUsersComponent', () => {
  let component: ListRegistratedUsersComponent;
  let fixture: ComponentFixture<ListRegistratedUsersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListRegistratedUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListRegistratedUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
