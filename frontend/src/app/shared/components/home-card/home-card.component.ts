import { Component, input } from '@angular/core';
import { IonCard, IonIcon, IonRippleEffect } from '@ionic/angular/standalone';
import {
  calendarOutline,
  pulseOutline,
  documentTextOutline,
  logOutOutline,
} from 'ionicons/icons';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-home-card',
  templateUrl: './home-card.component.html',
  styleUrls: ['./home-card.component.scss'],
  imports: [IonIcon, IonCard, IonRippleEffect],
})
export class HomeCardComponent {
  public homeCard = input.required<HomeCard>();

  constructor() {
    addIcons({
      calendarOutline,
      pulseOutline,
      documentTextOutline,
      'log-out-outline': logOutOutline,
    });
  }
}

export interface HomeCard {
  icon: 'pulse' | 'document-text' | 'calendar' | 'log-out'; // https://ionic.io/ionicons
  text: string;
  route: `/${string}`;
}
