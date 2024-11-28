import { Component, OnDestroy, signal, ViewEncapsulation } from '@angular/core';
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
import {
  TiptapBubbleMenuDirective,
  TiptapEditorDirective,
  TiptapFloatingMenuDirective,
} from 'ngx-tiptap';
import Placeholder from '@tiptap/extension-placeholder';
import Blockquote from '@tiptap/extension-blockquote';

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
  isLoading = signal(false);
  isSaveDisabled = signal(true);

  editor = new Editor({
    extensions: [StarterKit, Placeholder, Blockquote],
  });

  value = '<p>Hello, Tiptap!</p>'; // can be HTML or JSON, see https://www.tiptap.dev/api/editor#content

  public onSaveClick() {
    this.isLoading.set(true);
    setTimeout(() => {
      this.isLoading.set(false);
    }, 2000);
  }
  ngOnDestroy(): void {
    this.editor.destroy();
  }
}
