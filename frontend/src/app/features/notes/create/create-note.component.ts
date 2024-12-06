import { Component, inject, signal } from '@angular/core';
import {
  IonContent,
  IonLabel,
  IonSpinner,
  IonButton,
} from '@ionic/angular/standalone';
import { HeaderComponent } from '../../../shared/components/header/header.component';
import { NoteService } from '../../../shared/services/note.service';
import { Note } from '../../../shared/types/note.types';
import { Router } from '@angular/router';
import { NoteStore } from 'src/app/features/notes/store/notes.store';
import { TextEditorComponent } from 'src/app/shared/components/text-editor/text-editor.component';
import { Editor } from '@tiptap/core';
import StarterKit from '@tiptap/starter-kit';
import Placeholder from '@tiptap/extension-placeholder';

@Component({
  selector: 'app-create-note',
  templateUrl: './create-note.component.html',
  styleUrls: ['./create-note.component.scss'],
  imports: [
    IonButton,
    IonContent,
    HeaderComponent,
    IonSpinner,
    IonLabel,
    TextEditorComponent,
  ],
  providers: [],
})
export class CreateNoteComponent {
  isLoading = signal(false);
  isSaveDisabled = signal(true);

  retrievedNote: Note | null = null;

  textValue = signal<string>('');

  noteService = inject(NoteService);
  noteStore = inject(NoteStore);
  router = inject(Router);

  editor: Editor;

  constructor() {
    this.editor = new Editor({
      extensions: [StarterKit, Placeholder],
    });
  }

  public onSaveClick() {
    this.isLoading.set(true);

    this.noteService.createNote({ text: this.textValue() }).subscribe({
      next: (note) => {
        this.noteStore.addNote(note);
        this.retrievedNote = note;

        this.router.navigate(['/notes']);
      },
      error: (err) => {
        console.error(err);
      },
      complete: () => {
        this.isLoading.set(false);
      },
    });
  }
}
