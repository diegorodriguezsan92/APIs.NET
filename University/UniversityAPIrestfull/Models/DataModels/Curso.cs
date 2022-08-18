using System.ComponentModel.DataAnnotations;

namespace UniversityAPIrestfull.Models.DataModels
{

    public class Course: BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required, StringLength(1000)]
        public string LongDescription { get; set; } = string.Empty;

        [Required]
        public string PublicObjective { get; set; } = string.Empty;

        [Required]
        public string Objectives { get; set; } = string.Empty;

        [Required]
        public string Requirements { get; set; } = string.Empty;

        public enum Level
        {
            Basic, Medium, Advanced
        }


    }
}
