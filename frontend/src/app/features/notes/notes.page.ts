import { stripHTML } from './../../shared/helpers/string';
import { Component, inject, OnInit, signal } from '@angular/core';
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
  IonList,
  IonItemSliding,
  IonItemOption,
  IonItemOptions,
  IonRippleEffect,
} from '@ionic/angular/standalone';
import { HeaderComponent } from '../../shared/components/header/header.component';
import { addIcons } from 'ionicons';
import {
  add,
  chevronDownOutline,
  documentTextOutline,
  pencilOutline,
  trashOutline,
  pin,
  trash,
  close,
} from 'ionicons/icons';
import { Router, RouterModule } from '@angular/router';
import { CreateNoteComponent } from 'src/app/features/notes/create/create-note.component';
import { CommonModule } from '@angular/common';
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
    IonAccordionGroup,
    IonAccordion,
    IonItem,
    IonLabel,
    IonList,
    IonItemSliding,
    IonItemOption,
    IonItemOptions,
    IonRippleEffect,
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

  stripHTML = stripHTML;

  private readonly noteService = inject(NoteService);
  readonly noteStore = inject(NoteStore);

  private readonly routerOutlet = inject(IonRouterOutlet);

  ngOnInit(): void {
    this.routerOutlet.swipeGesture = true;
  }

  constructor() {
    addIcons({
      add,
      documentTextOutline,
      trashOutline,
      pencilOutline,
      chevronDownOutline,
      pin,
      trash,
      close,
    });
  }

  swipeCardAction(noteId: string, action: 'delete' | 'open') {
    if (action === 'delete') {
      this.deletingNoteId.set(noteId);
      this.noteService.deleteNoteById(noteId).subscribe({
        next: (res) => {
          if (!res) return;

          this.noteStore.deleteNoteById(noteId);
        },
      });
    } else if (action === 'open') {
      this.router.navigate(['/notes/edit-note', noteId]);
    }
  }

  putNoteToFavourites(note: Note) {
    const updatedNote: Note = {
      ...note,
      isFavourite: !note.isFavourite,
    };

    this.noteService
      .updateNote({
        noteId: note.id,
        text: note.text,
        isFavourite: updatedNote.isFavourite,
      })
      .subscribe({
        next: (res) => {
          if (!res) return;

          this.noteStore.updateNote(updatedNote);
        },
      });
  }

  setScrollY($event: boolean) {
    this.scrollY.set($event);
  }
}
