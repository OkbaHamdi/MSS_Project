import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBinFileComponent } from './add-bin-file.component';

describe('AddBinFileComponent', () => {
  let component: AddBinFileComponent;
  let fixture: ComponentFixture<AddBinFileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddBinFileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddBinFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
