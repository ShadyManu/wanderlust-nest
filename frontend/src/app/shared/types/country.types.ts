export interface AppCountries {
  key: string;
  countryName: string;
  currencyName: string;
  symbol: string;
}
export interface ApiRateResponse {
  base: string;
  date: string;
  rates: Rates;
  success: boolean;
  timestamp: number;
}

interface Rates {
  USD: number;
}
