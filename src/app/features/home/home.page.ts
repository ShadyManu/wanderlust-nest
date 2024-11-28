import { IonContent } from '@ionic/angular/standalone';
import {
  HomeCard,
  HomeCardComponent,
} from './../../shared/components/home-card/home-card.component';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from '../../shared/components/header/header.component';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
  imports: [HomeCardComponent, RouterModule, HeaderComponent, IonContent],
})
export class HomePage {
  homeCards: HomeCard[] = [
    {
      icon: 'pulse',
      text: 'Currency Exchange Rates',
      route: '/currency-rates',
    },
    {
      icon: 'document-text',
      text: 'Notes',
      route: '/notes',
    },
    {
      icon: 'calendar',
      text: 'Planner',
      route: '/planner',
    },
  ];
  constructor() {}
}
