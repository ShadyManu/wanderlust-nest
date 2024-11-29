import { Component } from '@angular/core';
import {
  IonContent,
  IonFab,
  IonFabButton,
  IonIcon,
  IonFabList,
} from '@ionic/angular/standalone';
import { HeaderComponent } from '../../shared/components/header/header.component';
import { addIcons } from 'ionicons';
import {
  add,
  createOutline,
  documentTextOutline,
  pencilOutline,
  trashOutline,
} from 'ionicons/icons';
import { RouterModule } from '@angular/router';
import { CreateNoteComponent } from 'src/app/shared/components/create-note/create-note.component';
import { CommonModule } from '@angular/common';
import { NoteCardComponent } from '../../shared/components/note-card/note-card.component';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.page.html',
  styleUrls: ['./notes.page.scss'],
  imports: [
    IonFabList,
    IonIcon,
    IonFabButton,
    IonFab,
    IonContent,
    HeaderComponent,
    RouterModule,
    CommonModule,
    NoteCardComponent,
  ],
})
export class NotesPage {
  public component = CreateNoteComponent;

  notesId: string[] = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'];

  constructor() {
    addIcons({ add, documentTextOutline, trashOutline, pencilOutline });
  }

  public createNote() {
    console.log('Create note');
  }

  onEdit(event: Event): void {
    event.stopPropagation(); // Previene l'espansione durante il clic sul pulsante
    console.log('Modifica cliccato');
    // Collega qui la tua logica di modifica
  }

  onDelete(event: Event): void {
    event.stopPropagation(); // Previene l'espansione durante il clic sul pulsante
    console.log('Cancella cliccato');
    // Collega qui la tua logica di cancellazione
  }
}
