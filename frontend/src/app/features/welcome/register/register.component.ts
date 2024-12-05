import {
  IonInput,
  IonButton,
  IonContent,
  IonSpinner,
} from '@ionic/angular/standalone';
import { Component, effect, inject, signal } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  imports: [
    IonInput,
    IonButton,
    IonContent,
    TranslateModule,
    FormsModule,
    IonSpinner,
  ],
  providers: [ModalController],
})
export class RegisterComponent {
  emailValue = signal<string>('');
  passwordValue = signal<string>('');

  isLoading = signal<boolean>(false);
  isSubmitDisabled = signal<boolean>(true);

  authService = inject(AuthService);

  constructor(public modalCtrl: ModalController) {
    effect(() => {
      this.isSubmitDisabled.set(
        this.emailValue().length === 0 || this.passwordValue().length === 0
      );
    });
  }

  register() {
    this.isLoading.set(true);
    // setTimeout(() => {
    //   this.isLoading.set(false);
    //   this.modalCtrl.dismiss();
    // }, 3000);
    this.authService
      .register(this.emailValue(), this.passwordValue())
      .subscribe({
        next: () => {
          this.modalCtrl.dismiss();
        },
        error: () => {
          console.error('Error while registering');
        },
        complete: () => this.isLoading.set(false),
      });
  }
}
