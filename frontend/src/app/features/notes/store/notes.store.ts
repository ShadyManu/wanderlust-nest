import { computed, inject } from '@angular/core';
import {
  patchState,
  signalStore,
  withComputed,
  withHooks,
  withMethods,
  withState,
} from '@ngrx/signals';
import { NoteService } from 'src/app/shared/services/note.service';
import { Note } from 'src/app/shared/types/note.types';

const notesStoreKey = 'ng-notes-store';

type NoteState = {
  notes: Note[];
};

const initialState: NoteState = {
  notes: [],
};

export const NoteStore = signalStore(
  // { providedIn: 'root' },
  withState(initialState),
  withComputed(({ notes }) => ({
    getNotes: computed(() => notes),
    getNoteById: computed(
      () => (id: string) => notes().find((note) => note.id === id)
    ),
  })),
  withMethods((store) => ({
    addNote(note: Note): void {
      patchState(store, (state) => ({ notes: [...state.notes, note] }));
    },
    loadNotes(notes: Note[]): void {
      patchState(store, { notes });
    },
    updateNote(note: Note): void {
      patchState(store, (state) => ({
        notes: state.notes.map((n) => (n.id === note.id ? note : n)),
      }));
    },
    deleteNoteById(id: string): void {
      patchState(store, (state) => ({
        notes: state.notes.filter((note) => note.id !== id),
      }));
    },
  })),
  withHooks({
    onInit(store) {
      const noteService = inject(NoteService);

      noteService.getAllNotesByUserId().subscribe({
        next: (notes) => {
          store.loadNotes(notes);
        },
        error: () => store.deleteNoteById(''),
      });
    },
  })
);
