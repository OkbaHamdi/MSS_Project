import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlimentationWalletComponent } from './alimentation-wallet.component';

describe('AlimentationWalletComponent', () => {
  let component: AlimentationWalletComponent;
  let fixture: ComponentFixture<AlimentationWalletComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlimentationWalletComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AlimentationWalletComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
