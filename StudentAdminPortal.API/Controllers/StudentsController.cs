using Microsoft.AspNetCore.Mvc;
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
        [Route("[controller")] // gives controller name: students
        public IActionResult GetAllStudents()
        {
            return Ok(_studentRepository.GetStudents());
        }
    }
}
