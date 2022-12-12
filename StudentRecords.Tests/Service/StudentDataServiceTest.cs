using StudentRecords.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecords.Shared.Interfaces;
using StudentRecords.Shared.Models;
using System.Runtime.CompilerServices;
using StudentRecords.Shared.Services;
using Newtonsoft.Json;

namespace StudentRecords.Tests.Services
{
    //Tests to test that the student data service properly reas and writes from json as expected
    [TestClass]
    public class StudentDataServiceTest
    {
        private string studentPath = "TestData/Students.json";
        private string coursesPath = "TestData/Courses.json";

        //This student details is what the json gets reset to after each test 
        private Student TestStudent = new Student()
        {
            StudentId = 77777703,
            FirstName = "Test",
            LastName = "Test 03",
            KnownAs = "Test",
            DisplayName = "Test 03 Test 03",
            DateOfBirth = DateTime.Parse("1989-03-07T00:00:00"),
            Gender = null,
            UniversityEmail = "Test.Test6@mail.bcu.ac.uk",
            NetworkId = "S77777703",
            HomeOrOverseas = "H",
            CourseEnrolment = new List<CourseEnrolment>()
                {
                    new CourseEnrolment()
                    {
                        EnrolmentId = "77777703/2",
                        AcademicYear = "2020/1",
                        YearOfStudy = 1,
                        Occurrence = "SEP",
                        ModeOfAttendance = "FULL TIME",
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
        };

        [TestMethod]
        public void CorrectNumberOfCourses()
        {
            //There are 3 courses in the test data json
            var count = 3;
            
            var studentDataService = new StudentDataService(studentPath,coursesPath);
            var controller = new CoursesController(studentDataService);

            //Act
            var actionResult = controller.Get();
            var resultList = actionResult.Result;

            //Assert
            Assert.AreEqual(count, resultList.Count());


        }

        [TestMethod]
        public void CorrectNumberOfStudents()
        {
            //There is 1 student in the test data json
            var count = 1;

            var studentDataService = new StudentDataService(studentPath, coursesPath);
            var controller = new StudentsController(studentDataService);

            //Act
            var actionResult = controller.GetStudents();
            var resultList = actionResult.Result;

            //Assert
            Assert.AreEqual(count, resultList.Count());


        }

        [TestMethod]
        public void GetStudentByIdGetsCorrectStudent()
        {
            //Id for the student in the test data
            var id = 77777703;

            var studentDataService = new StudentDataService(studentPath, coursesPath);
            var controller = new StudentsController(studentDataService);

            //Act
            var actionResult = controller.GetStudent(id);
            var result = actionResult.Result;

            //Assert
            Assert.AreEqual(id, result.StudentId);


        }

        [TestMethod]
        public void GetStudentByIdGetsCorrectStudentDetails()
        {
            //Id for the student in the test data
            var id = 77777703;

            var studentDataService = new StudentDataService(studentPath, coursesPath);
            var controller = new StudentsController(studentDataService);

            //Act
            var actionResult = controller.GetStudent(id);
            var result = actionResult.Result;


            //Assert
            Assert.AreEqual(TestStudent.FirstName, result.FirstName);
            Assert.AreEqual(TestStudent.LastName, result.LastName);
            Assert.AreEqual(TestStudent.DisplayName, result.DisplayName);
            Assert.AreEqual(TestStudent.KnownAs, result.KnownAs);
            Assert.AreEqual(TestStudent.DateOfBirth, result.DateOfBirth);
            Assert.AreEqual(TestStudent.NetworkId, result.NetworkId);
            Assert.AreEqual(TestStudent.UniversityEmail, result.UniversityEmail);
            Assert.AreEqual(TestStudent.Gender, result.Gender);
            Assert.AreEqual(TestStudent.HomeOrOverseas, result.HomeOrOverseas);
            Assert.AreEqual(TestStudent.CourseEnrolment.Count(), result.CourseEnrolment.Count());


        }

        [TestMethod]
        public void AddStudentAddsStudent()
        {
            //mock service has 1 student at start
            var count = 1;

            //create new student
            var studentToAdd = new Student()
            {
                FirstName = "Test",
                LastName = "Test 03",
                KnownAs = "Test",
                DisplayName = "Test 03 Test 03",
                DateOfBirth = DateTime.Parse("1989-03-07T00:00:00"),
                Gender = null,
                UniversityEmail = "Test.Test6@mail.bcu.ac.uk",
                NetworkId = "S77777703",
                HomeOrOverseas = "H",
                CourseEnrolment = new List<CourseEnrolment>()
                {
                    new CourseEnrolment()
                    {
                        EnrolmentId = "77777703/2",
                        AcademicYear = "2020/1",
                        YearOfStudy = 1,
                        Occurrence = "SEP",
                        ModeOfAttendance = "FULL TIME",
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
            };

            var studentDataService = new StudentDataService(studentPath, coursesPath);
            var controller = new StudentsController(studentDataService);



            //Act
            var actionResult = controller.CreateStudent(studentToAdd).Result;

            var students = controller.GetStudents().Result;


            //Assert
            Assert.AreEqual(count + 1, students.Count());

            ResetJsonData();
        }

        [TestMethod]
        public void CreateStudentAddsCorrectStudent()
        {
            //mock service has 1 student at start
            var count = 1;

            //create new student
            var studentToAdd = new Student()
            {
                FirstName = "Test",
                LastName = "Test 03",
                KnownAs = "Test",
                DisplayName = "Test 03 Test 03",
                DateOfBirth = DateTime.Parse("1989-03-07T00:00:00"),
                Gender = null,
                UniversityEmail = "Test.Test6@mail.bcu.ac.uk",
                NetworkId = "S77777703",
                HomeOrOverseas = "H",
                CourseEnrolment = new List<CourseEnrolment>()
                {
                    new CourseEnrolment()
                    {
                        EnrolmentId = "77777703/2",
                        AcademicYear = "2020/1",
                        YearOfStudy = 1,
                        Occurrence = "SEP",
                        ModeOfAttendance = "FULL TIME",
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
            };

            var studentDataService = new StudentDataService(studentPath, coursesPath);
            var controller = new StudentsController(studentDataService);


            //Act
            var actionResult = controller.CreateStudent(studentToAdd).Result;

            var studentsFromGet = controller.GetStudents().Result;

            var studentFromGet = studentsFromGet.FirstOrDefault(x => x.StudentId == studentToAdd.StudentId);



            //Assert
            Assert.AreEqual(studentToAdd.FirstName, studentFromGet.FirstName);
            Assert.AreEqual(studentToAdd.LastName, studentFromGet.LastName);
            Assert.AreEqual(studentToAdd.DisplayName, studentFromGet.DisplayName);
            Assert.AreEqual(studentToAdd.KnownAs, studentFromGet.KnownAs);
            Assert.AreEqual(studentToAdd.DateOfBirth, studentFromGet.DateOfBirth);
            Assert.AreEqual(studentToAdd.NetworkId, studentFromGet.NetworkId);
            Assert.AreEqual(studentToAdd.UniversityEmail, studentFromGet.UniversityEmail);
            Assert.AreEqual(studentToAdd.Gender, studentFromGet.Gender);
            Assert.AreEqual(studentToAdd.HomeOrOverseas, studentFromGet.HomeOrOverseas);
            Assert.AreEqual(studentToAdd.CourseEnrolment.Count(), studentFromGet.CourseEnrolment.Count());

            ResetJsonData();
        }

        [TestMethod]
        public void UpdateStudentDoesNotAddStudent()
        {
            //mock service has 1 student at start
            var count = 1;

            //create new student
            var studentToUpdate = new Student()
            {
                StudentId = 77777703,
                FirstName = "Updated",
                LastName = "Test 03",
                KnownAs = "Test",
                DisplayName = "Test 03 Test 03",
                DateOfBirth = DateTime.Parse("1989-03-07T00:00:00"),
                Gender = null,
                UniversityEmail = "Test.Test6@mail.bcu.ac.uk",
                NetworkId = "S77777703",
                HomeOrOverseas = "H",
                CourseEnrolment = new List<CourseEnrolment>()
                {
                    new CourseEnrolment()
                    {
                        EnrolmentId = "77777703/2",
                        AcademicYear = "2020/1",
                        YearOfStudy = 1,
                        Occurrence = "SEP",
                        ModeOfAttendance = "FULL TIME",
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
            };

            var studentDataService = new StudentDataService(studentPath, coursesPath);
            var controller = new StudentsController(studentDataService);


            //Act
            var actionResult = controller.UpdateStudent(studentToUpdate).Result;

            var students = controller.GetStudents().Result;


            //Assert
            Assert.AreEqual(count, students.Count());

            ResetJsonData();
        }


        [TestMethod]
        public void UpdateStudentUpdatesProperty()
        {
            //mock service has 1 student at start
            var count = 1;

            //create new student
            var studentToUpdate = new Student()
            {
                StudentId = 77777703,
                FirstName = "Updated",
                LastName = "Test 03",
                KnownAs = "Test",
                DisplayName = "Test 03 Test 03",
                DateOfBirth = DateTime.Parse("1989-03-07T00:00:00"),
                Gender = null,
                UniversityEmail = "Test.Test6@mail.bcu.ac.uk",
                NetworkId = "S77777703",
                HomeOrOverseas = "H",
                CourseEnrolment = new List<CourseEnrolment>()
                {
                    new CourseEnrolment()
                    {
                        EnrolmentId = "77777703/2",
                        AcademicYear = "2020/1",
                        YearOfStudy = 1,
                        Occurrence = "SEP",
                        ModeOfAttendance = "FULL TIME",
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
            };

            var studentDataService = new StudentDataService(studentPath, coursesPath);
            var controller = new StudentsController(studentDataService);


            //Act
            var studentBefore = new Student();
            studentBefore = controller.GetStudent(77777703).Result;

            var actionResult = controller.UpdateStudent(studentToUpdate).Result;

            var studentFromGet = new Student();
            studentFromGet = controller.GetStudent(77777703).Result;



            //Assert
            Assert.AreNotEqual(studentBefore.FirstName, studentFromGet.FirstName);

            ResetJsonData();
        }

        public void ResetJsonData()
        {
            var students = new List<Student>();

            students.Add(TestStudent);

            using (StreamWriter file = File.CreateText(studentPath))
            {
                JsonSerializer serializer = new JsonSerializer();

                serializer.Serialize(file, students);

            }

        }

    }

    

}

