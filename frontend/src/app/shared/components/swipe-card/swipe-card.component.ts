import { IonIcon, IonRippleEffect } from '@ionic/angular/standalone';
import { CommonModule } from '@angular/common';
import { Component, input, output, signal } from '@angular/core';
import { addIcons } from 'ionicons';
import { openOutline, trashOutline } from 'ionicons/icons';
import { Note } from '../../types/note.types';

@Component({
  selector: 'app-swipe-card',
  templateUrl: './swipe-card.component.html',
  styleUrls: ['./swipe-card.component.scss'],
  imports: [CommonModule, IonIcon, IonRippleEffect],
})
export class SwipeCardComponent {
  note = input.required<Note>();

  showDeleteIcon = signal<boolean>(true);
  deleteIcon = 'trash-outline';
  openIcon = 'open-outline';
  translateX = 0;
  startX = 0;
  swipeDirection: 'left' | null = null;

  swipeCardOutput = output<{ noteId: string; action: SwipeCardAction }>();
  scrollY = output<boolean>();

  constructor() {
    addIcons({ trashOutline, openOutline });
  }

  onSwipeStart(event: MouseEvent | TouchEvent): void {
    this.startX = this.getPoint(event).x;
  }

  onSwipeMove(event: MouseEvent | TouchEvent): void {
    const target = event.target as HTMLElement;
    if (
      target.nodeName !== 'ION-ICON' &&
      target.firstChild?.nodeName !== 'ION-ICON'
    )
      return;

    const currentX = this.getPoint(event).x;
    if (Math.abs(currentX - this.startX) < 10) {
      return;
    }

    this.showDeleteIcon.set(false);

    const iconName =
      target.nodeName === 'ION-ICON'
        ? target.getAttribute('name')
        : (target.firstChild as HTMLElement).getAttribute('name');
    if (currentX - this.startX > 0 && iconName === this.deleteIcon) {
      return;
    } else if (currentX - this.startX < 0 && iconName === this.openIcon) {
      return;
    }
    this.scrollY.emit(false);
    this.translateX = currentX - this.startX;

    this.swipeDirection = this.translateX > 0 ? null : 'left';
  }

  onSwipeEnd(): void {
    this.scrollY.emit(true);
    const cardElement = document.querySelector(
      '.relative.ripple-parent'
    ) as HTMLElement;

    if (cardElement) {
      const cardWidth = cardElement.offsetWidth;
      const halfwayPoint = cardWidth / 2;

      if (Math.abs(this.translateX) >= halfwayPoint) {
        const targetTranslateX = -cardWidth;

        this.animateSwipe(targetTranslateX, () => {
          this.deleteTriggered();
        });

        return;
      }
    }

    this.animateSwipe(0, () => {
      this.translateX = 0;
      this.swipeDirection = null;
      this.showDeleteIcon.set(true);
    });
  }

  private animateSwipe(target: number, onComplete: () => void): void {
    const step = 10;
    const direction = this.translateX < target ? 1 : -1;

    const animation = () => {
      if (Math.abs(this.translateX - target) > step) {
        this.translateX += direction * step;
        requestAnimationFrame(animation);
      } else {
        this.translateX = target;
        onComplete();

        this.translateX = 0;
        this.swipeDirection = null;
      }
    };

    animation();
  }

  deleteTriggered(doSwipeAnimationFirst = false) {
    if (doSwipeAnimationFirst) {
      this.animateSwipe(-1000, () => {});
    }

    this.swipeCardOutput.emit({
      noteId: this.note().id,
      action: SwipeCardAction.DELETE,
    });
  }

  openTriggered() {
    this.swipeCardOutput.emit({
      noteId: this.note().id,
      action: SwipeCardAction.OPEN,
    });
  }

  private getPoint(event: MouseEvent | TouchEvent): { x: number; y: number } {
    if ('touches' in event && event.touches.length > 0) {
      return { x: event.touches[0].clientX, y: event.touches[0].clientY };
    }
    return {
      x: (event as MouseEvent).clientX,
      y: (event as MouseEvent).clientY,
    };
  }
}

export enum SwipeCardAction {
  DELETE = 'delete',
  OPEN = 'open',
}
