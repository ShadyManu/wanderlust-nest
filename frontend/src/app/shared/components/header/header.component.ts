import { Component, inject } from '@angular/core';
import { IonToolbar, IonTitle, IonHeader } from '@ionic/angular/standalone';
import { AppService } from '../../services/app.service';

@Component({
  selector: 'app-header',
  template: `
    <ion-header class="ion-no-border mt-8" [collapse]="'condense'">
      <ion-toolbar [color]="'light'">
        <div class="flex flex-row items-center justify-between">
          <ion-title size="large" class="mt-[3px]"
            >{{ appService.getCurrentPage() }}
          </ion-title>
        </div>
      </ion-toolbar>
    </ion-header>
  `,
  imports: [IonTitle, IonToolbar, IonHeader],
})
export class HeaderComponent {
  appService = inject(AppService);
}
