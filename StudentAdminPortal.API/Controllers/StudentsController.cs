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
        private readonly IImageRepository _imageRepository;

        public StudentsController(
            IStudentRepository studentRepository, 
            IMapper mapper,
            IImageRepository imageRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("[controller]")] // gives controller name: students
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            // Set datamodel and create and return mapping
            var students = await _studentRepository.GetStudentsAsync();

            return Ok(_mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync(
            [FromRoute] Guid studentId)
        {
            // Fetch student and return student, if found
            var student = await _studentRepository.GetStudentAsync(studentId);            
            if (student == null) return NotFound();            
                        
            return Ok(_mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync(
            [FromRoute] Guid studentId, 
            [FromBody] UpdateStudentRequest request)
        {            
            if (await _studentRepository.Exists(studentId))
            {                
                // Map request to DataModels.Student and update student
                var updatedStudent = await _studentRepository.UpdateStudent(studentId, _mapper.Map<DataModels.Student>(request));

                if(updatedStudent != null) return Ok(_mapper.Map<Student>(updatedStudent));                
            }
            
            return NotFound();            
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await _studentRepository.Exists(studentId))
            {
                var student = await _studentRepository.DeleteStudent(studentId);
                return Ok(_mapper.Map<Student>(student));
            }            
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await _studentRepository.AddStudent(_mapper.Map<DataModels.Student>(request));

            // Note: createdataction results in a HTTP 201 response code == 'created'
            return CreatedAtAction(
                nameof(GetStudentAsync), 
                new { studentId = student.Id }, 
                _mapper.Map<Student>(student)
            );                
        }

        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadProfileImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            // Check if student exists
            if (await _studentRepository.Exists(studentId))
            {
                var profileImageFileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                
                // Upload image to local storage
                var profileImageFilePath = await _imageRepository.UploadProfileImage(profileImage, profileImageFileName);

                // Update profile image path in database
                await _studentRepository.UpdateProfileImage(studentId, profileImageFilePath);
                {
                    return Ok(profileImageFilePath);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
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