import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddsmsComponent } from './addsms.component';

describe('AddsmsComponent', () => {
  let component: AddsmsComponent;
  let fixture: ComponentFixture<AddsmsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddsmsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddsmsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
