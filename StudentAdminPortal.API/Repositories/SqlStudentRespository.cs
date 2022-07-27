using StudentAdminPortal.API.DataModels;

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
            return _context.Student.ToList();
        }
    }
}