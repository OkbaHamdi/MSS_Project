import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddInACSComponent } from './add-in-acs.component';

describe('AddInACSComponent', () => {
  let component: AddInACSComponent;
  let fixture: ComponentFixture<AddInACSComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddInACSComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddInACSComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
