using StudentRecords.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecords.Shared.Interfaces
{
    public interface IStudentDataService
    {
        public Task<List<Student>> GetStudents(int count = 100, int pageSize = 100, int page = 1);

        public Task<Student?> GetStudent(int id);

        public Task<bool> CreateStudent(Student student);

        public Task<bool> UpdateStudent(Student student);

        public Task<bool> DeleteStudent(int id);

        public Task<List<Course>> GetCourses(int count = 100, int pageSize = 100, int page = 1);
    }
}
