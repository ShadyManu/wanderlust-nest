import { IonToast } from '@ionic/angular/standalone';
import { Component, Injectable, signal, inject, effect } from '@angular/core';

@Component({
  selector: 'app-toast',
  template: `
    <ion-toast
      [isOpen]="isOpen()"
      [message]="message()"
      swipeGesture="vertical"
      [position]="position()"
    />
  `,
  imports: [IonToast],
})
export class ToastComponent {
  isOpen = signal<boolean>(false);
  message = signal<string>('');
  position = signal<'top' | 'bottom' | 'middle'>('bottom');
  duration = signal<number>(3000);

  toastService = inject(ToastService);

  constructor() {
    this.position.set(this.toastService.position());
    this.duration.set(this.toastService.duration());

    effect(() => {
      const isToastOpen = this.toastService.isToastOpen();
      this.isOpen.set(isToastOpen);

      const toastMessage = this.toastService.toastMessage();
      this.message.set(toastMessage);
    });
  }
}

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  isToastOpen = signal<boolean>(false);
  _isToastOpenEffect = effect(() => {
    if (this.isToastOpen()) {
      setTimeout(() => {
        this.isToastOpen.set(false);
      }, this.duration());
    }
  });

  toastMessage = signal<string>('');

  readonly position = signal<'top' | 'bottom' | 'middle'>('bottom');
  readonly duration = signal<number>(3000);
}
