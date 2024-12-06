import { UpdateNoteDto } from './../../../shared/types/backend.interfaces';
import { Component, inject, input, OnInit, signal } from '@angular/core';
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
  selector: 'app-edit-note',
  templateUrl: './edit-note.component.html',
  styleUrls: ['./edit-note.component.scss'],
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
export class EditNoteComponent implements OnInit {
  id = input.required<string>();
  retrievedNote: Note | null = null;

  isLoading = signal(false);
  isSaveDisabled = signal(true);

  noteService = inject(NoteService);
  noteStore = inject(NoteStore);

  router = inject(Router);
  textValue = signal<string>('');

  editor: Editor;

  constructor() {
    this.editor = new Editor({
      extensions: [StarterKit, Placeholder],
    });
  }

  ngOnInit(): void {
    this.retrievedNote = this.noteStore.getNoteById()(this.id());
    console.log(this.retrievedNote);
  }

  public onEditSaveClick() {
    this.isLoading.set(true);

    const updatedNote: UpdateNoteDto = {
      noteId: this.id(),
      text: this.textValue(),
    };

    this.noteService.updateNote(updatedNote).subscribe({
      next: (succeded) => {
        if (!succeded) return;

        const newNote: Note = {
          id: updatedNote.noteId,
          text: updatedNote.text,
          lastModified: new Date().toISOString(),
        };
        this.noteStore.updateNote(newNote);
        this.retrievedNote = newNote;

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
