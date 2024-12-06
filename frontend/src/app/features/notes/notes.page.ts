import {
  Component,
  effect,
  inject,
  OnInit,
  signal,
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
  IonRouterOutlet,
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
import { CreateNoteComponent } from 'src/app/features/notes/create/create-note.component';
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
  providers: [],
})
export class NotesPage implements OnInit {
  public component = CreateNoteComponent;

  deletingNoteId = signal<string | null>(null);
  scrollY = signal<boolean>(true);
  router = inject(Router);
  isOverlayOpen = signal<boolean>(false);
  isClosingOverlay = signal<boolean>(false);

  allNotes: WritableSignal<Note[] | null> = signal(null);
  favoriteNotes: Note[] = [];

  private readonly noteService = inject(NoteService);
  readonly noteStore = inject(NoteStore);

  private readonly routerOutlet = inject(IonRouterOutlet);

  ngOnInit(): void {
    this.routerOutlet.swipeGesture = true;
  }

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
          if (!res) return;

          this.noteStore.deleteNoteById($event.noteId);
        },
      });
    } else if ($event.action === SwipeCardAction.OPEN) {
      this.router.navigate(['/notes/edit-note', $event.noteId]);
    }
  }

  setScrollY($event: boolean) {
    this.scrollY.set($event);
  }
}
