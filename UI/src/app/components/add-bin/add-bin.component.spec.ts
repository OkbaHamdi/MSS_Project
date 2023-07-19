import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBinComponent } from './add-bin.component';

describe('AddBinComponent', () => {
  let component: AddBinComponent;
  let fixture: ComponentFixture<AddBinComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddBinComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddBinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
