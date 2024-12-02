import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiRateResponse } from '../types/country.types';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CurrencyRateService {
  private http = inject(HttpClient);
  readonly API_KEY = '8K4yzYf2C216ZP7tCbLaodzB3EyrHQci';

  // TODO : Retrieve currency rates from personal backend
  getCurrencyRates(
    base: string,
    convertTo: string
  ): Observable<ApiRateResponse> {
    return this.http.get<ApiRateResponse>(
      `https://api.apilayer.com/exchangerates_data/latest?symbols=${convertTo}&base=${base}&apikey=${this.API_KEY}`
    );
  }
}
