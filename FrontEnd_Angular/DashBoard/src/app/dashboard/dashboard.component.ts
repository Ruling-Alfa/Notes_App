import { Component, OnDestroy, OnInit } from '@angular/core';
import { NoteSummary } from '../Models/not-summary';
import { NoteDataServiceService } from '../Services/note-data-service.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit, OnDestroy {

  private readonly unsubscribe$ = new Subject();
  isEditing: boolean = false;
  notes: Array<NoteSummary>;
  constructor(private noteService: NoteDataServiceService, private router: Router) {
    this.notes = [];
  }
  newNote: NoteSummary = new NoteSummary();

  public onAddNewNote() {
    this.noteService.AddNewNote(this.newNote)
      .pipe(takeUntil(this.unsubscribe$)).subscribe((n) => {
        this.newNote = new NoteSummary();
        this.RefreshNotes();
      });

  }

  onSaveEditNote() {
    this.noteService.UpdateNewNote(this.newNote)
      .pipe(takeUntil(this.unsubscribe$)).subscribe((n) => {
        this.isEditing = false;
        this.newNote = new NoteSummary();
        this.RefreshNotes();
      });
  }

  ngOnInit() {
    this.RefreshNotes();
  }

  ngOnDestroy() {
    this.unsubscribe$.next(null);
  }

  RefreshNotes() {
    this.noteService.GetNotesFrombackEnd()
      .pipe(takeUntil(this.unsubscribe$)).subscribe((n) => {
        this.notes = n;
      });
  }

  EditRecord(note: NoteSummary) {
    this.newNote = note;
    this.isEditing = true;
  }

  onLogOut() {
    localStorage.setItem('token', '')
    this.router.navigate(['login']);
  }
}
