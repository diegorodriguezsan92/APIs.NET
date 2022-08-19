using System.ComponentModel.DataAnnotations;

namespace UniversityAPIrestfull.Models.DataModels
{
    public class Student: BaseEntity
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime Dob { get; set; }   // Date Of Birth


        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();


    }
}
