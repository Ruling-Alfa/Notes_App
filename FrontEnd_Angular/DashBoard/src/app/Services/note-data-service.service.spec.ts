import { TestBed } from '@angular/core/testing';

import { NoteDataServiceService } from './note-data-service.service';

describe('NoteDataServiceService', () => {
  let service: NoteDataServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NoteDataServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
