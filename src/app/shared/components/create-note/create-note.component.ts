import {
  Component,
  inject,
  input,
  OnDestroy,
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
import Blockquote from '@tiptap/extension-blockquote';
import { NoteService } from '../../services/note.service';
import { Subject, takeUntil } from 'rxjs';

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
})
export class CreateNoteComponent implements OnDestroy {
  noteId = input.required<string>();
  retrievedNote: string = '';
  value = ''; // can be HTML or JSON, see https://www.tiptap.dev/api/editor#content

  isLoading = signal(false);
  isSaveDisabled = signal(true);

  noteService = inject(NoteService);
  editor = new Editor({
    extensions: [StarterKit, Placeholder, Blockquote],
  });

  private destroy$ = new Subject<void>();

  constructor() {
    this.noteService
      .getNoteById(this.noteId())
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (note) => {
          if (!note) return;

          this.retrievedNote = note;
          this.value = note;
        },
      });
  }

  onModelChange($event: any) {
    this.isSaveDisabled.set($event === this.retrievedNote);
  }

  public onSaveClick() {
    this.isLoading.set(true);
    console.log(this.editor.getHTML());
    setTimeout(() => {
      this.isLoading.set(false);
    }, 2000);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.editor.destroy();
  }
}
