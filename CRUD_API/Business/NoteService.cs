using AutoMapper;
using CRUD_API.Business.Interfaces;
using CRUD_API.Business.Models;
using CRUD_API.Data;
using CRUD_API.Data.Entities;

namespace CRUD_API.Business
{
    public class NoteService : INoteService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NoteService(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<NoteModel> CreateNote(NoteModel noteModel)
        {
            var note = _mapper.Map<Note>(noteModel);
            var noteEntity = await _unitOfWork.GetRepository<Note>().Insert(note);
            await _unitOfWork.Save();

            note = noteEntity.Entity;

            return _mapper.Map<NoteModel>(note);
        }
        public async Task<NoteModel> GetNote(int noteId, int userId)
        {
            var note = await _unitOfWork.GetRepository<Note>().GetOneByQuery(n =>
                                        n.Id == noteId && n.CreatedByUser == userId);
            return _mapper.Map<NoteModel>(note);
        }

        public async Task<List<NoteModel>> GetUserNotes(int userId)
        {
            var note = await _unitOfWork.GetRepository<Note>().Get(n => n.CreatedByUser == userId);
            return _mapper.Map<List<NoteModel>>(note);
        }

        public async Task<NoteModel> UpdateNote(NoteModel noteModel)
        {
            var existingNote = await _unitOfWork.GetRepository<Note>().GetOneByQuery(n =>
                                        n.Id == noteModel.Id && n.CreatedByUser == noteModel.CreatedByUser);
            if (existingNote is not null)
            {
                existingNote.NoteDetail = noteModel.NoteDetail;
                existingNote.NoteTitle = noteModel.NoteTitle;
                
                _unitOfWork.GetRepository<Note>().Update(existingNote);
                await _unitOfWork.Save();

                return noteModel;
            }
            return default;
        }

        public async Task DeleteNote(int noteId, int userId)
        {
            var existingNote = await _unitOfWork.GetRepository<Note>().GetOneByQuery(n =>
                                             n.Id == noteId && n.CreatedByUser == userId);
            if (existingNote is not null)
            {
                await _unitOfWork.GetRepository<Note>().Delete(noteId);
                await _unitOfWork.Save();
            }
        }

    }
}
