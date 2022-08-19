using System.ComponentModel.DataAnnotations;

namespace UniversityAPIrestfull.Models.DataModels
{
    public class Chapter: BaseEntity
    {

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = new Course(); // Con esto conseguimos tener un Chapter asociado a un Course y un Course asociado a un Chapter.

        [Required]
        public string List = string.Empty;
    }
}
