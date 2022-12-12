using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRecords.Shared.Interfaces;
using StudentRecords.Shared.Models;

namespace StudentRecords.WebApi.Controllers
{
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public IStudentDataService _studentDataService;

        public StudentsController(IStudentDataService studentDataService) {
            _studentDataService = studentDataService;
        }

        
        [HttpGet]
        [Route("api/Students/GetStudents")]
        public async Task<List<Student>> GetStudents(int count = 100, int pageSize = 100, int page = 1)
        {
            return await _studentDataService.GetStudents(count, pageSize, page);
        }

        
        [HttpGet]
        [Route("api/Students/GetStudent/{id}")]
        public async Task<Student> GetStudent(int id)
        {
            return await _studentDataService.GetStudent(id);
        }

        
        [HttpPost]
        [Route("api/Students/CreateStudent")]
        public async Task<bool> CreateStudent([FromBody] Student student)
        {
            var result = await _studentDataService.CreateStudent(student);

            return result;
        }

        [HttpPost]
        [Route("api/Students/UpdateStudent")]
        public async Task<bool> UpdateStudent([FromBody] Student student)
        {
            var result = await _studentDataService.UpdateStudent(student);

            return result;
        }

    }
}
