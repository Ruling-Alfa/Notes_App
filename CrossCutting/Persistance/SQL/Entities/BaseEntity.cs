using System;
using System.ComponentModel.DataAnnotations;


namespace CrossCutting.Persistance.SQL.Entities
{
    public record BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
