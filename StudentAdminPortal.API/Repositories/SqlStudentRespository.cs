using StudentAdminPortal.API.DataModels;
using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.API.Repositories
{
    public class SqlStudentRespository : IStudentRepository
    {
        private readonly StudentAdminContext _context;

        public SqlStudentRespository(StudentAdminContext context)
        {
            _context = context;
        }
        public List<Student> GetStudents()
        {
            return _context.Student
                .Include(nameof(Gender))
                .Include(nameof(Address))
                .ToList();
            // NOTE: nameof brings in details of the gender, associated with the student, keeping it typesave
        }
    }
}