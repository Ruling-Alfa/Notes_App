using CRUD_API.Business.Models;

namespace CRUD_API.Business.Interfaces
{
    public interface INoteService
    {
        Task<NoteModel> CreateNote(NoteModel noteModel);
        Task DeleteNote(int noteId, int userId);
        Task<NoteModel> GetNote(int noteId, int userId);
        Task<List<NoteModel>> GetUserNotes(int userId);
        Task<NoteModel> UpdateNote(NoteModel noteModel);
    }
}