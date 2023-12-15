namespace CRUD_API.Business.Models
{
    public record NoteModel
    {
        public int Id { get; set; }
        public string NoteTitle { get; set; }
        public string NoteDetail { get; set; }
        public int CreatedByUser { get; set; }
    }
}