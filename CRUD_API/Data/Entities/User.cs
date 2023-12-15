using CrossCutting.Persistance.SQL.Entities;

namespace CRUD_API.Data.Entities
{
    public record User : BaseEntity
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPasswordHash { get; set; }
        public string HashSalt { get; set; }
        public List<Note> Notes { get; set; }
    }
}
