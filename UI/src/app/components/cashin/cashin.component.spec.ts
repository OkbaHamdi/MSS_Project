import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CashinComponent } from './cashin.component';

describe('CashinComponent', () => {
  let component: CashinComponent;
  let fixture: ComponentFixture<CashinComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CashinComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CashinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
