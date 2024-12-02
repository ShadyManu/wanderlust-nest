import {
  Component,
  effect,
  inject,
  signal,
  WritableSignal,
} from '@angular/core';
import {
  IonContent,
  IonText,
  IonIcon,
  IonButton,
  IonSearchbar,
  IonItem,
  IonInput,
} from '@ionic/angular/standalone';
import {
  IonInputCustomEvent,
  InputInputEventDetail,
  IonSearchbarCustomEvent,
  SearchbarInputEventDetail,
} from '@ionic/core';
import { addIcons } from 'ionicons';
import { arrowForward, arrowForwardOutline } from 'ionicons/icons';
import { appCountries } from 'src/app/shared/constants/countries.constants';
import { CurrencyRateService } from 'src/app/shared/services/currency-rate.service';
import { AppCountries } from 'src/app/shared/types/country.types';
import { HeaderComponent } from '../../shared/components/header/header.component';

@Component({
    selector: 'app-currency-rates',
    templateUrl: './currency-rates.page.html',
    styleUrls: ['./currency-rates.page.scss'],
    imports: [
        IonInput,
        IonItem,
        IonSearchbar,
        IonButton,
        IonIcon,
        IonText,
        IonContent,
        HeaderComponent,
    ]
})
export class CurrencyRatesPage {
  // TODO: retrieve countries from backend
  readonly defaultCountries = Object.values(appCountries);

  public filteredCountries = signal<AppCountries[]>(this.defaultCountries);

  public selectedFirstCountry: WritableSignal<AppCountries | null> =
    signal(null);
  public selectedSecondCountry: WritableSignal<AppCountries | null> =
    signal(null);
  public firstCountryAmount: WritableSignal<number | null> = signal(null);
  public conversionResult = signal<string | null>(null);
  public currencyRate = signal<number | null>(null);

  private currencyRateService = inject(CurrencyRateService);

  constructor() {
    addIcons({ arrowForwardOutline, arrowForward });
    effect(
      () => {
        if (
          !this.selectedFirstCountry() ||
          !this.selectedSecondCountry() ||
          !this.firstCountryAmount()
        )
          return;

        if (this.selectedFirstCountry() === this.selectedSecondCountry()) {
          this.conversionResult.set(
            this.firstCountryAmount()!.toLocaleString('en-US', {
              maximumFractionDigits: 2,
            })
          );
          return;
        }

        const symbol = `${this.selectedSecondCountry()!.key}`;
        const base = `${this.selectedFirstCountry()!.key}`;

        this.currencyRateService.getCurrencyRates(base, symbol).subscribe({
          next: (data) => {
            const conversionRate =
              data.rates[symbol as keyof typeof data.rates];
            this.conversionResult.set(
              (this.firstCountryAmount()! * conversionRate).toLocaleString(
                'en-US',
                { maximumFractionDigits: 6 }
              )
            );
            this.currencyRate.set(conversionRate);
          },
        });
      },
      { allowSignalWrites: true }
    );
  }

  public onSearch($event: IonSearchbarCustomEvent<SearchbarInputEventDetail>) {
    const value = $event.detail.value;
    if (!value) {
      this.filteredCountries.set(this.defaultCountries);
      return;
    }

    const filtered = this.defaultCountries.filter(
      (country) =>
        country.countryName.toLowerCase().includes(value.toLowerCase()) ||
        country.key.toLowerCase().includes(value.toLowerCase())
    );
    this.filteredCountries.set(filtered);
  }

  public onInput(event: IonInputCustomEvent<InputInputEventDetail>) {
    const value = event.target!.value;
    if (!value) return;

    this.firstCountryAmount.set(value as number);
  }

  public onCurrencyClick(country: AppCountries) {
    const value = country;
    if (!value) return;

    if (!this.selectedFirstCountry()) {
      if (value.key === this.selectedSecondCountry()?.key) {
        this.selectedSecondCountry.set(null);
      } else this.selectedFirstCountry.set(value);
    } else if (value.key === this.selectedFirstCountry()?.key) {
      this.selectedFirstCountry.set(null);
    } else if (value.key === this.selectedSecondCountry()?.key) {
      this.selectedSecondCountry.set(null);
    } else {
      this.selectedSecondCountry.set(value);
    }

    if (!this.selectedFirstCountry() || !this.selectedSecondCountry()) {
      this.conversionResult.set(null);
      this.currencyRate.set(null);
    }
  }
}
