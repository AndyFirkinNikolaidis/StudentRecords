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
    public class MockStudentDataService : IStudentDataService
    {
        List<Student> Students { get; set; }
        List<Course> Courses { get; set; }


        public MockStudentDataService() {
            Courses = new List<Course>()
            {
                new Course()
                {

                    CourseCode = "CP0007",
                    CourseName = "Certificate of Attendance in Subject Knowledge Enhancement in Mathematics"
                },
               new Course()
               {

                   CourseCode = "CP0012N",
                   CourseName = "Medical Ultrasound Modules (PT)"
               },
               new Course()
               {

                   CourseCode = "CP0039",
                   CourseName = "Preparation for Mentors (PT)"
               }
            };

            Students = new List<Student>() {
                new Student()
                {
                    StudentId = 1,
                    FirstName= "Test",
                    LastName= "Test 03",
                    KnownAs = "Test",
                    DisplayName = "Test 03 Test 03",
                    DateOfBirth= DateTime.Parse("1989-03-07T00:00:00"),
                    Gender = null,
                    UniversityEmail = "Test.Test6@mail.bcu.ac.uk",
                    NetworkId = "S77777703",
                    HomeOrOverseas = "H",
                    CourseEnrolment = new List<CourseEnrolment>()
                    {
                        new CourseEnrolment()
                        {
                            EnrolmentId = "77777703/2",
                            AcademicYear =  "2020/1",
                            YearOfStudy = 1,
                            Occurrence= "SEP",
                            ModeOfAttendance= "FULL TIME",
                            EnrolmentStatus = "E",
                            CourseEntryDate = DateTime.Parse("2020-03-07T00:00:00"),
                            ExpectedEndDate = DateTime.Parse("2021-03-07T00:00:00"),
                            Course = new Course()
                            {
                                CourseCode = "UP0000",
                                CourseName = "Used for testing"
                            }
        
                        }

                    }
                }
            
            };

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
            return Students;


        }

        async Task<bool> WriteStudents(List<Student> students)
        {
            Students = students;

            return true;
        }

        async Task<List<Course>> ReadCourses()
        {
            return Courses;

            
        }
    }
}
