import { Component, input, signal } from '@angular/core';
import { addIcons } from 'ionicons';
import { trashOutline, pencilOutline, openOutline } from 'ionicons/icons';

@Component({
  selector: 'app-note-card',
  templateUrl: './note-card.component.html',
  imports: [],
})
export class NoteCardComponent {
  noteId = input.required<string>();
  isExpanded = signal(false);

  constructor() {
    addIcons({ trashOutline, pencilOutline, openOutline });
  }

  toggleExpand(): void {
    this.isExpanded.set(!this.isExpanded);
  }
}
