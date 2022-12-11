using Microsoft.AspNetCore.Mvc;
using StudentRecords.Shared.Interfaces;
using StudentRecords.Shared.Models;

namespace StudentRecords.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        public IStudentDataService _studentDataService;

        public CoursesController(IStudentDataService studentDataService) {
            _studentDataService = studentDataService;
        }

        // GET: api/<CoursesController>
        [HttpGet]
        public  List<Course> Get(int count = 100, int pageSize = 100, int page = 1)
        {
            return  _studentDataService.GetCourses(count, pageSize, page).Result;
        }

    }
}
