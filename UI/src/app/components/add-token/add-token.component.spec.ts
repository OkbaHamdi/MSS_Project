import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTokenComponent } from './add-token.component';

describe('AddTokenComponent', () => {
  let component: AddTokenComponent;
  let fixture: ComponentFixture<AddTokenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTokenComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddTokenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
