import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CurrencyRatesPage } from './currency-rates.page';

describe('CurrencyRatesPage', () => {
  let component: CurrencyRatesPage;
  let fixture: ComponentFixture<CurrencyRatesPage>;

  beforeEach(() => {
    fixture = TestBed.createComponent(CurrencyRatesPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
