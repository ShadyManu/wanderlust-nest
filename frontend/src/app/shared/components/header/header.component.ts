import { Component, inject } from '@angular/core';
import {
  IonToolbar,
  IonTitle,
  IonHeader,
  IonIcon,
  IonButton,
} from '@ionic/angular/standalone';
import { AppService } from '../../services/app.service';
import { addIcons } from 'ionicons';
import { power } from 'ionicons/icons';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  template: `
    <ion-header class="ion-no-border mt-8" [collapse]="'condense'">
      <ion-toolbar [color]="'light'">
        <div class="flex flex-row items-center justify-between">
          <ion-title size="large" class="mt-[3px]"
            >{{ appService.getCurrentPage() }}

            <ion-button
              shape="round"
              class="absolute right-0 mt-[10px]"
              (click)="logout()"
              (keydown)="logout()"
            >
              <ion-icon slot="icon-only" name="power"></ion-icon>
            </ion-button>
          </ion-title>
        </div>
      </ion-toolbar>
    </ion-header>
  `,
  imports: [IonTitle, IonToolbar, IonHeader, IonIcon, IonButton],
})
export class HeaderComponent {
  appService = inject(AppService);
  router = inject(Router);

  constructor() {
    addIcons({ power });
  }

  logout() {
    // TODO remove from phone storage
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');

    this.router.navigate(['/welcome']);
  }
}
