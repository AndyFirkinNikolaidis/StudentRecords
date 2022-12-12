using StudentRecords.WebApi.Controllers;
using StudentRecords.Shared.Models;
using StudentRecords.Shared.Services;

namespace StudentRecords.Tests.WebApi
{
    //Student controller test methods
    [TestClass]
    public class StudentsControllerTest
    {



        [TestMethod]
        public void CorrectNumberOfStudentsOnGet()
        {

            //There is 1 student in the mock service
            var count = 1;

            var studentDataService = new MockStudentDataService();
            var controller = new StudentsController(studentDataService);

            //Act
            var actionResult = controller.GetStudents().Result;
            var resultList = actionResult;

            //Assert
            Assert.AreEqual(count, resultList.Count());


        }

        [TestMethod]
        public void StudentGetCorrectFromId()
        {
            //id of student in mock is 1
            var idToTest = 1;

            var studentDataService = new MockStudentDataService();
            var controller = new StudentsController(studentDataService);


            //Act
            var actionResult = controller.GetStudent(idToTest).Result;
            var result = actionResult;


            //Assert
            Assert.AreEqual(idToTest, result.StudentId);


        }

        [TestMethod]
        public void CreateStudentAddsStudent()
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

            var studentDataService = new MockStudentDataService();
            var controller = new StudentsController(studentDataService);


            //Act
            var actionResult = controller.CreateStudent(studentToAdd).Result;

            var students = controller.GetStudents().Result;


            //Assert
            Assert.AreEqual(count + 1, students.Count());


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

            var studentDataService = new MockStudentDataService();
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


        }
        [TestMethod]
        public void UpdateStudentDoesNotAddStudent()
        {
            //mock service has 1 student at start
            var count = 1;

            //create new student
            var studentToUpdate = new Student()
            {
                StudentId = 1,
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

            var studentDataService = new MockStudentDataService();
            var controller = new StudentsController(studentDataService);


            //Act
            var actionResult = controller.UpdateStudent(studentToUpdate).Result;

            var students = controller.GetStudents().Result;


            //Assert
            Assert.AreEqual(count, students.Count());


        }

        [TestMethod]
        public void UpdateStudentUpdatesProperty()
        {
            //mock service has 1 student at start
            var count = 1;

            //create new student
            var studentToUpdate = new Student()
            {
                StudentId = 1,
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

            var studentDataService = new MockStudentDataService();
            var controller = new StudentsController(studentDataService);


            //Act
            var studentBefore = new Student();
            studentBefore = controller.GetStudent(1).Result;

            var actionResult = controller.UpdateStudent(studentToUpdate).Result;

            var studentFromGet = new Student();
            studentFromGet = controller.GetStudent(1).Result;

            

            //Assert
            Assert.AreNotEqual(studentBefore.FirstName, studentFromGet.FirstName);


        }

    }
}
