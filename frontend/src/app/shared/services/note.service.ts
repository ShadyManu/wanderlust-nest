import { CreateNoteDto, UpdateNoteDto } from './../types/backend.interfaces';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Note } from '../types/note.types';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class NoteService {
  private http = inject(HttpClient);
  private noteEndpoint = environment.apiUrl + '/api/notes';

  public getAllNotesByUserId(): Observable<Note[]> {
    return this.http.get<Note[]>(this.noteEndpoint);
  }

  public deleteNoteById(id: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.noteEndpoint}/${id}`);
  }

  public createNote(createNoteDto: CreateNoteDto): Observable<Note> {
    return this.http.post<Note>(this.noteEndpoint, createNoteDto);
  }

  public updateNote(updatedNote: UpdateNoteDto): Observable<boolean> {
    return this.http.put<boolean>(this.noteEndpoint, updatedNote);
  }
}
