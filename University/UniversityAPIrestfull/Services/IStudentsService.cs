// This is an INTERFACE class type (that is the reason the doc is named IStudentsService).
// The INTERFACE type class runs for add complex logic operations wich we need to implement.

using UniversityAPIrestfull.Models.DataModels;

namespace UniversityAPIrestfull.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();

    }
}
