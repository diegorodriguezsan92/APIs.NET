using Microsoft.EntityFrameworkCore;
using UniversityAPIrestfull.Models.DataModels;

namespace UniversityAPIrestfull.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options)
        { 
        
        }

        // TODO: Add DbSets (tables of our data base)
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Student>? Students { get; set; }


    }
}
