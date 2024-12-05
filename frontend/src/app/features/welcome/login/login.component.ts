import { TranslateModule } from '@ngx-translate/core';
import { Component, effect, inject, signal } from '@angular/core';
import {
  IonInput,
  IonButton,
  IonContent,
  IonSpinner,
} from '@ionic/angular/standalone';
import { ModalController } from '@ionic/angular';
import { AuthService } from 'src/app/shared/services/auth.service';
import { FormsModule } from '@angular/forms';
import { LoginResponse } from 'src/app/shared/types/backend.interfaces';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [
    IonInput,
    IonButton,
    IonContent,
    IonSpinner,
    TranslateModule,
    FormsModule,
  ],
  providers: [ModalController],
})
export class LoginComponent {
  emailValue = signal<string>('');
  passwordValue = signal<string>('');

  isLoading = signal<boolean>(false);
  isSubmitDisabled = signal<boolean>(true);

  constructor(public modalCtrl: ModalController) {
    effect(() => {
      this.isSubmitDisabled.set(
        this.emailValue().length === 0 || this.passwordValue().length === 0
      );
    });
  }

  authService = inject(AuthService);
  router = inject(Router);

  login() {
    this.isLoading.set(true);

    this.authService.login(this.emailValue(), this.passwordValue()).subscribe({
      next: (result) => {
        const value = result as LoginResponse;
        // TODO: set inside the signal store
        localStorage.setItem('token', value.accessToken);
        localStorage.setItem('refreshToken', value.refreshToken);

        this.isLoading.set(false);
        this.modalCtrl.dismiss();

        this.router.navigate(['/home']);
      },
      error: () => {
        console.error('Error while logging in');
      },
    });
  }

  async dismiss() {
    await this.modalCtrl.dismiss();
  }
}
