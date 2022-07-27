using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")] // gives controller name: students
        public IActionResult GetAllStudents()
        {
            // set datamodel
            var students = _studentRepository.GetStudents();

            // Create and return mapping            
            return Ok(_mapper.Map<List<Student>>(students));
        }
    }
}


/* Solution without using automapper
 * 
 * public IActionResult GetAllStudents()
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
                    GenderId = student.GenderId,
                    Address = new Address()
                    {
                        Id = student.Address.Id,
                        PhysicalAddress = student.Address.PhysicalAddress,
                        PostalAddress = student.Address.PostalAddress,
                    },
                    Gender = new Gender()
                    {
                        Id = student.Gender.Id,
                        Description = student.Gender.Description
                    }
                });
            }

            return Ok(domainModelsStudents);
        }*/