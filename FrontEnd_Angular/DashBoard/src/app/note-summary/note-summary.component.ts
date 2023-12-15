import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { NoteSummary } from '../Models/not-summary';
import { Subject, takeUntil } from 'rxjs';
import { NoteDataServiceService } from '../Services/note-data-service.service';

@Component({
  selector: 'app-note-summary',
  templateUrl: './note-summary.component.html',
  styleUrl: './note-summary.component.css'
})
export class NoteSummaryComponent implements OnInit, OnDestroy {
  private readonly unsubscribe$ = new Subject();
  @Output() recordDeleted: EventEmitter<number> = new EventEmitter<number>();
  @Output() recordEditing: EventEmitter<NoteSummary> = new EventEmitter<NoteSummary>();

  @Input() noteSummary!: NoteSummary;
  constructor(private noteService: NoteDataServiceService) {

  }



  ngOnInit() {
  }

  ngOnDestroy() {
    this.unsubscribe$.next(null);
  }

  public onDeleteNote() {
    if (confirm(`Are you sure to delete ${this.noteSummary.noteTitle}`)) {
      this.noteService.DeleteNote(this.noteSummary.id)
        .pipe(takeUntil(this.unsubscribe$)).subscribe((n) => {
          console.log(`Deleted with id ${this.noteSummary.id}`);
          this.recordDeleted.emit(this.noteSummary.id);
        });
    }
  }

  public onRecordEditing() {
    this.recordEditing.emit(this.noteSummary);
  }

}
