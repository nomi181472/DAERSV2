/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from 'src/app/users/user-list/node_modules/@angular/core/testing';
import { By } from 'src/app/users/user-list/node_modules/@angular/platform-browser';
import { DebugElement } from 'src/app/users/user-list/node_modules/@angular/core';

import { UserListComponent } from './user-list.component';

describe('UserListComponent', () => {
  let component: UserListComponent;
  let fixture: ComponentFixture<UserListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
