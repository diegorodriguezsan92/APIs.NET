// En BaseEntity.cs establecemos todos los requisitos o campos que queremos que tengan TODAS nuestras tablas.
using System.ComponentModel.DataAnnotations;

namespace UniversityAPIrestfull.Models.DataModels
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UptdatedAt { get; set; } = DateTime.Now;
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime DeletedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;



    }
}
