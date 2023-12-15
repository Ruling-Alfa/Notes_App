using CrossCutting.Persistance.SQL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_API.Data.Entities
{
    public record Note : BaseEntity
    {
        public string NoteTitle { get; set; }
        public string NoteDetail { get; set; }
        [ForeignKey("User")]
        public int CreatedByUser { get; set; }

        public User User { get; set; }
    }
}