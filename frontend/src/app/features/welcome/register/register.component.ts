import {
  IonInput,
  IonButton,
  IonContent,
  IonSpinner,
  IonList,
  IonLabel,
  IonItem,
  IonRadioGroup,
  IonRadio,
} from '@ionic/angular/standalone';
import { Component, effect, inject, signal } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import {
  checkUppercase,
  checkLowercase,
  checkLength,
  checkNumber,
  checkSpecial,
  checkEmail,
} from 'src/app/shared/helpers/validators';

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
    IonList,
    IonLabel,
    IonItem,
    IonRadioGroup,
    IonRadio,
  ],
  providers: [ModalController],
})
export class RegisterComponent {
  emailValue = signal<string>('');
  passwordValue = signal<string>('');

  isLoading = signal<boolean>(false);
  isSubmitDisabled = signal<boolean>(true);

  authService = inject(AuthService);

  passwordValidator: { [key: string]: boolean } = {
    uppercase: false,
    lowercase: false,
    number: false,
    special: false,
    minLength: false,
  };
  validatorsKeys = Object.keys(this.passwordValidator);
  minLength = 8;

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

  register() {
    this.isLoading.set(true);
    this.authService
      .register(this.emailValue(), this.passwordValue())
      .subscribe({
        next: (value) => {
          if (!value) return;

          this.modalCtrl.dismiss();
        },
        error: () => {
          console.error('Error while registering');
        },
        complete: () => this.isLoading.set(false),
      });
  }
}
