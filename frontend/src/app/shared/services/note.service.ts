import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Note } from '../types/note.types';

@Injectable({
  providedIn: 'root',
})
export class NoteService {
  private http = inject(HttpClient);

  private notes: Note[] = [
    {
      id: 'b05fef70-4791-4bcd-8fcb-ed5ec58c0332',
      title: 'Alla scoperta delle isole greche',
      description:
        'Un viaggio tra spiagge, cultura e tramonti mozzafiato nelle isole dell’Egeo.',
    },
    {
      id: '5819c1dd-b480-4c95-95cc-1d934bce950c',
      title: 'Esplorando le foreste tropicali in Costa Rica',
      description:
        'Avventura tra cascate, vulcani e natura selvaggia in Centro America.',
    },
    {
      id: 'b757cf4a-d333-483a-946d-4f2d94f0c0ac',
      title: 'Il viaggio in Giappone',
      description:
        'Dai templi antichi di Kyoto alle luci di Tokyo, un viaggio indimenticabile.',
    },
    {
      id: 'd3be7d5d-5891-484c-88d9-ce456be5dd2d',
      title: "Un'avventura nelle Alpi",
      description:
        'Escursioni mozzafiato tra cime innevate e vallate pittoresche.',
    },
    {
      id: '7ce7b234-c93b-408f-8fd9-e019263161e3',
      title: "Alla ricerca della pace nei templi dell'India",
      description:
        'Meditazione e spiritualità tra i colori e profumi dell’India.',
    },
    {
      id: 'bece3b63-153d-4657-aacf-93c85adf6c8c',
      title: 'Sulle tracce di Game of Thrones in Croazia',
      description:
        'Scopri le location che hanno ispirato il mondo di Westeros.',
    },
    {
      id: '9951d77a-6bed-4059-b605-bf9a5f8a5828',
      title: 'Racconti da un safari in Kenya',
      description:
        'Un’avventura tra leoni, giraffe e tramonti nella savana africana.',
    },
  ];

  // TODO: define Note structure and retrieve notes from the server
  public getAllNotesByUserId() {
    return this.notes;
  }

  // TODO: Retrieve note from the server
  public getNoteById(noteId: string): Observable<Note> {
    // emulate returning a string as observable
    return new Observable((observer) => {
      observer.next(this.notes.find((note) => note.id === noteId));
      observer.complete();
    });
  }
}
