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
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            // set datamodel
            var students = await _studentRepository.GetStudentsAsync();

            // Create and return mapping            
            return Ok(_mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            // Fetch student
            var student = await _studentRepository.GetStudentAsync(studentId);

            // Return student, if found
            if (student == null)
            {
                return NotFound();
            }
                        
            return Ok(_mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync(
            [FromRoute] Guid studentId, 
            [FromBody] UpdateStudentRequest request)
        {
            // Check if student exists
            if (await _studentRepository.Exists(studentId))
            {                
                // Student found - update details
                // Map request to DataModels.Student
                var updatedStudent = await _studentRepository.UpdateStudent(studentId, _mapper.Map<DataModels.Student>(request));

                if(updatedStudent != null) 
                {
                    return Ok(_mapper.Map<Student>(updatedStudent));
                }
            }
            
            return NotFound();            
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