using System.ComponentModel.DataAnnotations;

namespace UniversityAPIrestfull.Models.DataModels
{

    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }


    public class Course: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string LongDescription { get; set; } = string.Empty;

        [Required]
        public string PublicObjective { get; set; } = string.Empty;

        [Required]
        public string Objectives { get; set; } = string.Empty;

        [Required]
        public string Requirements { get; set; } = string.Empty;

        public Level Level { get; set; } = Level.Basic;



        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public Chapter Chapters { get; set; } = new Chapter();  //  public Chapter Chapter { get; set; } = new Chapter();

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
