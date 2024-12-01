import { Component, inject, signal } from '@angular/core';
import {
  IonContent,
  IonFab,
  IonFabButton,
  IonIcon,
  IonAccordionGroup,
  IonAccordion,
  IonItem,
  IonLabel,
} from '@ionic/angular/standalone';
import { HeaderComponent } from '../../shared/components/header/header.component';
import { addIcons } from 'ionicons';
import {
  add,
  chevronDownOutline,
  documentTextOutline,
  pencilOutline,
  trashOutline,
} from 'ionicons/icons';
import { Router, RouterModule } from '@angular/router';
import { CreateNoteComponent } from 'src/app/shared/components/create-note/create-note.component';
import { CommonModule } from '@angular/common';
import {
  SwipeCardAction,
  SwipeCardComponent,
} from '../../shared/components/swipe-card/swipe-card.component';
import { Note } from 'src/app/shared/types/note.types';
import { NoteService } from 'src/app/shared/services/note.service';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.page.html',
  styleUrls: ['./notes.page.scss'],
  imports: [
    IonIcon,
    IonFabButton,
    IonFab,
    IonContent,
    HeaderComponent,
    RouterModule,
    CommonModule,
    SwipeCardComponent,
    IonAccordionGroup,
    IonAccordion,
    IonItem,
    IonLabel,
  ],
})
export class NotesPage {
  public component = CreateNoteComponent;

  deletingNoteId = signal<string | null>(null);
  scrollY = signal<boolean>(true);
  router = inject(Router);
  isOverlayOpen = signal<boolean>(false);
  isClosingOverlay = signal<boolean>(false);
  notes: Note[] = [];

  noteService = inject(NoteService);

  constructor() {
    this.notes = this.noteService.getAllNotesByUserId();
    addIcons({
      add,
      documentTextOutline,
      trashOutline,
      pencilOutline,
      chevronDownOutline,
    });
  }

  swipeCardAction($event: { noteId: string; action: SwipeCardAction }) {
    if ($event.action === SwipeCardAction.DELETE) {
      // TODO
      this.deletingNoteId.set($event.noteId);
      setTimeout(() => {
        this.notes = this.notes.filter((note) => note.id !== $event.noteId);
      }, 500);
    } else if ($event.action === SwipeCardAction.OPEN) {
      this.router.navigate(['/edit-note', $event.noteId]);
    }
  }

  setScrollY($event: boolean) {
    this.scrollY.set($event);
  }
}
