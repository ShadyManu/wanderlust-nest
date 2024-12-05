import {
  Component,
  effect,
  inject,
  signal,
  ViewEncapsulation,
  WritableSignal,
} from '@angular/core';
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
import { NoteStore } from './store/notes.store';

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
  providers: [NoteStore],
})
export class NotesPage {
  public component = CreateNoteComponent;

  deletingNoteId = signal<string | null>(null);
  scrollY = signal<boolean>(true);
  router = inject(Router);
  isOverlayOpen = signal<boolean>(false);
  isClosingOverlay = signal<boolean>(false);

  allNotes: WritableSignal<Note[] | null> = signal(null);
  favoriteNotes: Note[] = [];

  noteService = inject(NoteService);
  noteStore = inject(NoteStore);

  constructor() {
    effect(() => {
      const notes = this.noteStore.getNotes()();
      this.allNotes.set(this.noteStore.getNotes()());
      console.log('allNotes:', notes);
    });
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
      this.deletingNoteId.set($event.noteId);
      this.noteService.deleteNoteById($event.noteId).subscribe({
        next: (res) => {
          if (res) {
            this.noteStore.deleteNoteById($event.noteId);
          }
        },
      });
      // setTimeout(() => {
      //   this.allNotes =
      //     this.allNotes?.filter((note) => note.id !== $event.noteId) ?? null;
      // }, 500);
    } else if ($event.action === SwipeCardAction.OPEN) {
      this.router.navigate(['/edit-note', $event.noteId]);
    }
  }

  setScrollY($event: boolean) {
    this.scrollY.set($event);
  }
}
