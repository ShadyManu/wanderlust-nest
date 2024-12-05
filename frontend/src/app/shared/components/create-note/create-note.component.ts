import {
  Component,
  inject,
  input,
  OnDestroy,
  OnInit,
  signal,
  ViewEncapsulation,
} from '@angular/core';
import {
  IonContent,
  IonLabel,
  IonSpinner,
  IonButton,
} from '@ionic/angular/standalone';
import { HeaderComponent } from '../header/header.component';
import { Editor } from '@tiptap/core';
import { FormsModule } from '@angular/forms';
import StarterKit from '@tiptap/starter-kit';
import { TiptapBubbleMenuDirective, TiptapEditorDirective } from 'ngx-tiptap';
import Placeholder from '@tiptap/extension-placeholder';
import { NoteService } from '../../services/note.service';
import { Subject } from 'rxjs';
import { Note } from '../../types/note.types';
import { Router } from '@angular/router';
import { NoteStore } from 'src/app/features/notes/store/notes.store';

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
    FormsModule,
    TiptapEditorDirective,
    TiptapBubbleMenuDirective,
  ],
  encapsulation: ViewEncapsulation.None,
  providers: [NoteStore],
})
export class CreateNoteComponent implements OnInit, OnDestroy {
  id = input.required<string>();
  retrievedNote: Note | null = null;
  value = ''; // can be HTML or JSON, see https://www.tiptap.dev/api/editor#content

  isLoading = signal(false);
  isSaveDisabled = signal(true);

  noteService = inject(NoteService);
  noteStore = inject(NoteStore);

  router = inject(Router);
  editor = new Editor({
    extensions: [StarterKit, Placeholder],
  });

  private destroy$ = new Subject<void>();

  ngOnInit(): void {
    // TODO : read note by id from signal store

    this.isSaveDisabled.set(false);
    console.log(this.isSaveDisabled());
  }

  onModelChange($event: any) {
    this.isSaveDisabled.set($event === this.retrievedNote);
  }

  public onSaveClick() {
    this.isLoading.set(true);

    console.log(this.editor.getHTML());

    this.noteService.createNote({ text: this.editor.getHTML() }).subscribe({
      next: (note) => {
        this.noteStore.addNote(note);
        console.log(this.noteStore.notes());
        this.isLoading.set(false);
        this.retrievedNote = note;
        this.isSaveDisabled.set(true);

        this.router.navigate(['/notes']);
      },
      error: (err) => {
        console.error(err);
        this.isLoading.set(false);
      },
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.editor.destroy();
  }
}
