import { Component, inject, OnInit, signal } from '@angular/core';
import { IonApp, IonRouterOutlet } from '@ionic/angular/standalone';
import { NavigationEnd, Router } from '@angular/router';
import { AppService } from './shared/services/app.service';
import { AppPage } from './shared/types/layout.types';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { GlobalStore } from './shared/global-store/global.store';
import { AuthService } from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  template: `
    @if (!isAppLoading()) {
    <ion-app>
      <ion-router-outlet></ion-router-outlet>
    </ion-app>
    }
  `,
  imports: [IonApp, IonRouterOutlet, TranslateModule],
  providers: [GlobalStore],
})
export class AppComponent implements OnInit {
  appService = inject(AppService);
  authService = inject(AuthService);
  router = inject(Router);
  globalStore = inject(GlobalStore);

  isAppLoading = signal<boolean>(true);

  constructor(private translate: TranslateService) {
    const defaultLanguage = localStorage.getItem('language') ?? 'en';

    this.translate.addLangs(['it', 'en', 'es']);
    this.translate.setDefaultLang(defaultLanguage);
    this.translate.use(defaultLanguage);

    this.globalStore.setLanguage(defaultLanguage);

    this.isAppLoading.set(false);
  }

  ngOnInit(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        const url = event.urlAfterRedirects.split('/')[1];
        const page = this.mapUrlToAppPage(url);
        if (page) {
          this.appService.setCurrentPage(page);
        }
      }
    });
  }

  private mapUrlToAppPage(url: string): AppPage | null {
    const lowerUrl = url.toLowerCase();
    switch (true) {
      case lowerUrl.includes('login'):
        return AppPage.Login;
      case lowerUrl.includes('home'):
        return AppPage.Home;
      case lowerUrl.includes('currency-rates'):
        return AppPage.CurrencyRates;
      case lowerUrl.includes('notes'):
        return AppPage.Notes;
      case lowerUrl.includes('create-note'):
        return AppPage.CreateNote;
      case lowerUrl.includes('edit-note'):
        return AppPage.EditNote;
      case lowerUrl.includes('planner'):
        return AppPage.Planner;
      default:
        return null;
    }
  }
}
