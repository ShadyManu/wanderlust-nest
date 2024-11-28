import { Component, inject, OnInit } from '@angular/core';
import { IonApp, IonRouterOutlet, IonContent } from '@ionic/angular/standalone';
import { NavigationEnd, Router } from '@angular/router';
import { AppService } from './shared/services/app.service';
import { AppPage } from './shared/types/layout.types';

@Component({
    selector: 'app-root',
    templateUrl: 'app.component.html',
    imports: [IonContent, IonApp, IonRouterOutlet]
})
export class AppComponent implements OnInit {
  appService = inject(AppService);
  router = inject(Router);

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
      case lowerUrl.includes('planner'):
        return AppPage.Planner;
      default:
        return null;
    }
  }
}
