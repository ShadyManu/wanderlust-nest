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

const sortNotesByDate = (notes: Note[]): Note[] =>
  notes
    .slice()
    .sort(
      (a, b) =>
        new Date(b.lastModified).getTime() - new Date(a.lastModified).getTime()
    );

export const NoteStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withComputed(({ notes }) => ({
    getNotes: computed(() => notes),
  })),
  withMethods((store) => ({
    addNote(note: Note): void {
      const notes = [...store.notes(), note];
      const sortedNotesByModifiedDate = sortNotesByDate(notes);
      patchState(store, { notes: sortedNotesByModifiedDate });
    },
    loadNotes(notes: Note[]): void {
      patchState(store, { notes });
    },
    updateNote(note: Note): void {
      const updatedNotes = store
        .notes()
        .map((n) => (n.id === note.id ? note : n));
      const sortedNotesByModifiedDate = sortNotesByDate(updatedNotes);
      patchState(store, { notes: sortedNotesByModifiedDate });
    },
    deleteNoteById(id: string): void {
      patchState(store, (state) => ({
        notes: state.notes.filter((note) => note.id !== id),
      }));
    },
    getNoteById:
      () =>
      (id: string): Note | null => {
        return store.notes().find((note) => note.id === id) ?? null;
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
