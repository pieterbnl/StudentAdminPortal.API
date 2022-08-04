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

        public async Task<bool> Exists(Guid studentId)
        {
            return await _context.Student.AnyAsync(studentEl => studentEl.Id == studentId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _context.Gender.ToListAsync();
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await _context.Student
                .Include(nameof(Gender))
                .Include(nameof(Address))
                .FirstOrDefaultAsync(studentEl => studentEl.Id == studentId);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Student
                .Include(nameof(Gender))
                .Include(nameof(Address))
                .ToListAsync();
            // NOTE: nameof brings in details of the gender, associated with the student, keeping it typesave
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student studentToUpdate)
        {
            var existingStudent = await GetStudentAsync(studentId);
            
            if(existingStudent != null)
            {
                existingStudent.FirstName = studentToUpdate.FirstName;
                existingStudent.LastName = studentToUpdate.LastName;
                existingStudent.DateOfBirth = studentToUpdate.DateOfBirth;
                existingStudent.Email = studentToUpdate.Email;
                existingStudent.Mobile = studentToUpdate.Mobile;
                existingStudent.GenderId = studentToUpdate.GenderId;
                existingStudent.Address.PhysicalAddress = studentToUpdate.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = studentToUpdate.Address.PostalAddress;

                _context.SaveChangesAsync();
                return existingStudent;
            }
            
            return null;

            // To check update student in Swagger:
            //{
            //    "firstName": "Johny",
            //    "lastName": "D",
            //    "dateOfBirth": "1989-06-11T00:00:00",
            //    "email": "Santos.Valencia@gmaill.com",
            //    "mobile": 91254684826,
            //    "genderId": "6f08fab6-c62e-4306-9d77-c82c9c6a23ac",   
            //    "physicalAddress": null,
            //    "postalAddress": null
            //}
        }
    }
}