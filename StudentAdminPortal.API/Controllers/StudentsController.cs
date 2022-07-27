using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Route("[controller]")] // gives controller name: students
        public IActionResult GetAllStudents()
        {
            // set datamodel
            var students = _studentRepository.GetStudents(); 

            // set domainmodel
            var domainModelsStudents = new List<Student>();

            // populate list of domain model students
            foreach (var student in students)
            {
                domainModelsStudents.Add(new Student()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DateOfBirth = student.DateOfBirth,
                    Email = student.Email,
                    Mobile = student.Mobile,
                    ProfileImageUrl = student.ProfileImageUrl,
                    GenderId = student.GenderId
                });
            }

            return Ok(domainModelsStudents);
        }
    }
}
