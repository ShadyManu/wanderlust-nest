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
import {
  checkUppercase,
  checkLowercase,
  checkNumber,
  checkSpecial,
  checkLength,
  checkEmail,
} from 'src/app/shared/helpers/validators';

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

  passwordValidator: { [key: string]: boolean } = {
    uppercase: false,
    lowercase: false,
    number: false,
    special: false,
    minLength: false,
  };
  validatorsKeys = Object.keys(this.passwordValidator);
  minLength = 8;

  isLoading = signal<boolean>(false);
  isSubmitDisabled = signal<boolean>(true);

  isForgotPassword = signal<boolean>(false);

  constructor(public modalCtrl: ModalController) {
    effect(() => {
      this.passwordValidator['uppercase'] = checkUppercase(
        this.passwordValue()
      );
      this.passwordValidator['lowercase'] = checkLowercase(
        this.passwordValue()
      );
      this.passwordValidator['number'] = checkNumber(this.passwordValue());
      this.passwordValidator['special'] = checkSpecial(this.passwordValue());
      this.passwordValidator['minLength'] = checkLength(
        this.passwordValue(),
        this.minLength
      );

      const isPasswordValid = Object.values(this.passwordValidator).every(
        (value) => value === true
      );
      const isEmailValid = checkEmail(this.emailValue());

      this.isSubmitDisabled.set(!isPasswordValid || !isEmailValid);
    });
  }

  authService = inject(AuthService);
  router = inject(Router);

  async login() {
    this.isLoading.set(true);

    this.authService.login(this.emailValue(), this.passwordValue()).subscribe({
      next: (result) => {
        const value = result as LoginResponse;
        // TODO: set inside the signal store / phone storage
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

  openIsForgotPassword() {
    this.isForgotPassword.set(true);
  }

  async dismiss() {
    await this.modalCtrl.dismiss();
  }
}
