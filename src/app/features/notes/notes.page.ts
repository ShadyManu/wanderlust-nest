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
import { add, documentTextOutline } from 'ionicons/icons';
import { RouterModule } from '@angular/router';
import { CreateNoteComponent } from 'src/app/shared/components/create-note/create-note.component';

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
    ]
})
export class NotesPage {
  public component = CreateNoteComponent;
  constructor() {
    addIcons({ add, documentTextOutline });
  }

  public createNote() {
    console.log('Create note');
  }
}
