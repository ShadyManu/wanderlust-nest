import {
  Component,
  input,
  InputSignal,
  OnDestroy,
  OnInit,
  output,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Editor } from '@tiptap/core';
import { TiptapEditorDirective, TiptapBubbleMenuDirective } from 'ngx-tiptap';
import { Note } from '../../types/note.types';

@Component({
  selector: 'app-text-editor',
  template: `
    <div class="w-full h-full">
      <tiptap-editor
        [editor]="editor()"
        [(ngModel)]="value"
        class="flex h-full w-full"
        (ngModelChange)="onModelChange($event)"
      />
      <tiptap-bubble-menu [editor]="editor()">
        <!-- Anything that should be rendered inside bubble menu -->
        prova bubble menu
      </tiptap-bubble-menu>
    </div>
  `,
  styles: [``],
  imports: [TiptapEditorDirective, TiptapBubbleMenuDirective, FormsModule],
})
export class TextEditorComponent implements OnInit, OnDestroy {
  retrievedNote: InputSignal<Note | null> = input<Note | null>(null);

  hasTextChangedFromStart = output<boolean>();
  value: string = ''; // can be HTML or JSON, see https://www.tiptap.dev/api/editor#content

  outputValue = output<string>();

  editor: InputSignal<Editor> = input.required<Editor>();

  ngOnInit(): void {
    this.value = this.retrievedNote()?.text ?? '';
  }

  onModelChange($event: any) {
    this.hasTextChangedFromStart.emit($event === this.retrievedNote);
    this.outputValue.emit(this.value);
  }

  ngOnDestroy() {
    this.editor().destroy();
  }
}
