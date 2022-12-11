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
        public  List<Student> GetStudents(int count = 100, int pageSize = 100, int page = 1)
        {
            return  _studentDataService.GetStudents(count, pageSize, page).Result;
        }

        
        [HttpGet]
        [Route("api/Students/GetStudent/{id}")]
        public Student GetStudent(int id)
        {
            return _studentDataService.GetStudent(id).Result;
        }

        
        [HttpPost]
        [Route("api/Students/CreateStudent")]
        public void CreateStudent([FromBody] Student student)
        {
            _studentDataService.CreateStudent(student);
        }

        [HttpPost]
        [Route("api/Students/UpdateStudent")]
        public void UpdateStudent([FromBody] Student student)
        {
            _studentDataService.UpdateStudent(student);
        }

        [HttpPost]
        [Route("api/Students/DeleteStudent")]
        public void DeleteStudent(int id)
        {
            _studentDataService.DeleteStudent(id);
        }
    }
}
