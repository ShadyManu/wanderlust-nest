import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { ModalController } from '@ionic/angular';
import {
  IonContent,
  IonButton,
  IonModal,
  IonRouterOutlet,
} from '@ionic/angular/standalone';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TranslateModule } from '@ngx-translate/core';
import { GlobalStore } from 'src/app/shared/global-store/global.store';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.page.html',
  styleUrls: ['./welcome.page.scss'],
  standalone: true,
  imports: [
    IonContent,
    IonButton,
    IonModal,
    RegisterComponent,
    LoginComponent,
    TranslateModule,
  ],
  providers: [ModalController],
})
export class WelcomePage implements OnInit {
  globalStore = inject(GlobalStore);

  logo = signal<string>(
    `../../../assets/logo-${this.globalStore.getLogo()}.png`
  );

  private readonly routerOutlet = inject(IonRouterOutlet);

  ngOnInit(): void {
    // TODO clear phone storage
    localStorage.clear();
    this.routerOutlet.swipeGesture = false;
  }

  constructor(public modalCtrl: ModalController) {
    effect(() => {
      this.logo.set(`../../../assets/logo-${this.globalStore.getLogo()}.png`);
    });
  }
}
