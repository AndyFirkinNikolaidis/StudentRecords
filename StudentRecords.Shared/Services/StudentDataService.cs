using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StudentRecords.Shared.Interfaces;
using StudentRecords.Shared.Models;
using static System.Net.Mime.MediaTypeNames;

namespace StudentRecords.Shared.Services
{
    //Student data service to read write to the json file
    public class StudentDataService : IStudentDataService
    {
        public IConfiguration _configuration;

        public string StudentPath;

        public string CoursesPath;

        public StudentDataService(IConfiguration configuration) {

            _configuration = configuration;
            StudentPath = _configuration["StudentJsonPath"];
            CoursesPath = _configuration["CourseJsonPath"];
        
        }

        //Get all students
        //Paging added for future usage in large datasets
        public async Task<List<Student>> GetStudents(int count = 100, int pageSize = 100, int page = 1)
        {
            var students = new List<Student>();

            students = await ReadStudents();

            var skip = pageSize * (page - 1);

            return students.Skip(skip).Take(count).ToList();
        }

        //Get single student by id
        public async Task<Student?> GetStudent(int id)
        {
            var students = await ReadStudents();

            return students.FirstOrDefault(x => x.StudentId == id);
        }

        //create student with incremented id
        public async Task<bool> CreateStudent(Student student)
        {
            var students = await ReadStudents();

            //get highest student id 
            int hightest = (int)students.Max(a => a.StudentId);

            //set id as one higher than the existing highest
            var studentId = hightest + 1;
            student.StudentId = studentId;
            //Network id is id prefixed byS (this is an assumtpion)
            student.NetworkId = $"S{studentId}";

            students.Add(student);

            return await WriteStudents(students);
            
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            var students = await ReadStudents();

            //Not happy with how i do this but as a proof of concept it works
            //needs refactoring
            //get existing student by id 
            var existingStudent = students.FirstOrDefault(x => x.StudentId == student.StudentId);

            //get the other students and cast to a list
            var otherStudents = students.Where(x => x.StudentId != student.StudentId).ToList();

            if (existingStudent != null)
            {
                //add the new version to the list
                otherStudents.Add(student);

                //save
                return await WriteStudents(otherStudents);

            }
            else
            {
                return false;
            }

        }

        public async Task<bool> DeleteStudent(int id)
        {
            var students = await ReadStudents();

            //get existing student by id 
            var existingStudentsWithoutDeleted = students.Where(x => x.StudentId != id);

            return await WriteStudents(existingStudentsWithoutDeleted.ToList());

        }


        //Get all courses
        //Paging added for future usage in large datasets
        public async Task<List<Course>> GetCourses(int count = 100, int pageSize = 100, int page = 1)
        {
            var courses = new List<Course>();

            courses = await ReadCourses();

            var skip = pageSize * (page - 1);

            return courses.Skip(skip).Take(count).ToList();
        }



        //Methods for read write to json below
        

        async Task<List<Student>> ReadStudents()
        {
            var students = new List<Student>();

            //read students from file
            using (StreamReader r = new StreamReader(StudentPath))
            {
                string json = r.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(json);
                return students;
            }


        }

        async Task<bool> WriteStudents(List<Student> students)
        {
            try
            {
                using (StreamWriter file = File.CreateText(StudentPath))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    serializer.Serialize(file, students);

                    return true;
                }
                
            }
            catch (Exception ex)
            {

                //Log the exception somewhere


                return false;
            }
            

        }

        async Task<List<Course>> ReadCourses()
        {
            var courses = new List<Course>();

            using (StreamReader r = new StreamReader(CoursesPath))
            {
                string json = r.ReadToEnd();
                courses = JsonConvert.DeserializeObject<List<Course>>(json);
                return courses;
            }
        }
    }
}
