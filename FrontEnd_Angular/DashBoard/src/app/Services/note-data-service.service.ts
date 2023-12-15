import { Injectable } from '@angular/core';
import { NoteSummary } from '../Models/not-summary';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TokenService } from '../token.service';

@Injectable({
  providedIn: 'root'
})
export class NoteDataServiceService {



  private url = 'https://localhost:7245/api/Notes';

  constructor(private httpClient: HttpClient) {

  }

  public GetNotesFrombackEnd() {
    return this.httpClient.get<Array<NoteSummary>>(this.url, { headers: this.getHeaders() });
  }

  getHeaders(): HttpHeaders {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.GetToken()}`
    })
    return headers;
  }

  public AddNewNote(newNote: NoteSummary) {
    return this.httpClient.post<Array<NoteSummary>>(this.url, newNote, { headers: this.getHeaders() });
  }

  public UpdateNewNote(newNote: NoteSummary) {
    return this.httpClient.put<Array<NoteSummary>>(this.url, newNote, { headers: this.getHeaders() });
  }

  public GetNote(id: number) {
    return this.httpClient.get<Array<NoteSummary>>(`${this.url}/id`, { headers: this.getHeaders() });
  }

  public DeleteNote(id: number) {
    return this.httpClient.delete<Array<NoteSummary>>(`${this.url}/${id}`, { headers: this.getHeaders() });
  }

  private GetToken(){
    return localStorage.getItem('token');
  }
}
