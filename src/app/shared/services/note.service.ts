import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NoteService {
  private http = inject(HttpClient);

  // TODO: define Note structure and retrieve notes from the server
  public getAllNotesByUserId(userId: string) {
    return [];
  }

  // TODO: Retrieve note from the server
  public getNoteById(noteId: string): Observable<string> {
    // emulate returning a string as observable
    return new Observable((observer) => {
      observer.next('note content');
      observer.complete();
    });
  }
}
