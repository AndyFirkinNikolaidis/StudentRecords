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

namespace StudentRecords.Tests.WebApi
{
    [TestClass]
    public class CourseControllerTest
    {

        [TestMethod]
        public void CorrectNumberOfCourses()
        {
            //There are 3 courses in the mock service
            var count = 3;
            
            var studentDataService = new MockStudentDataService();
            var controller = new CoursesController(studentDataService);

            //Act
            var actionResult = controller.Get();
            var resultList = actionResult.Result;

            //Assert
            Assert.AreEqual(count, resultList.Count());


        }
    }
}
