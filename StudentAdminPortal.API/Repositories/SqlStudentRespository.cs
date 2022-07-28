﻿using StudentAdminPortal.API.DataModels;
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
    }
}