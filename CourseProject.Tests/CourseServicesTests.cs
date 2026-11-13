using System;
using Xunit;
using cs330_proj1;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace CourseProject.Tests
{
    public class CourseServicesTests
    {
        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalNotFound_ExceptionThrown()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(GetTestCourses());
            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>()
            {
                new CoreGoal()
                {
                    Courses = GetTestCourses(),
                    Description = "test",
                    Id = "CG1",
                    Name = "English Literacy"
                }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>()
            {
                new CourseOffering()
                {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = GetTestCourses().First()
                }
            });

            var courseServices = new CourseServices(mockRepository.Object);
            var goalId = "CG5";
            var semester = "Spring 2021";

            // Act/Assert
            Assert.Throws<Exception>(() => courseServices.getOfferingsByGoalIdAndSemester(goalId, semester));
        }

        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndOneCourseOfferingIsInSemester_OfferingIsReturned()
        {
            //Arrange
            var course = new Course()
            {
                Name="ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course>
            {
                course
            });

            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>()
            {
                new CoreGoal()
                {
                    Courses = GetTestCourses(),
                    Description = "test",
                    Id = "CG1",
                    Name = "English Literacy"
                }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>()
            {
                new CourseOffering()
                {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course
                }
            });

            var goalId = "CG1";
            var semester = "Spring 2021";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.getOfferingsByGoalIdAndSemester(goalId, semester);

            //Assert
            var itemInList = Assert.Single(result);
            // Assert.Equal(2, result.Count());
            Assert.Equal(semester, itemInList.Semester);
            Assert.Equal(course.Name, itemInList.TheCourse.Name);
        }

        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndMultipleCourseOfferingsAreInSemester_OfferingAreReturned()
        {
            // Arrange
            var courses = GetTestCourses();
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>()
            {
                new CoreGoal() { Courses = courses, Description = "test", Id = "CG1", Name = "English Literacy" }
            });
            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>()
            {
                new CourseOffering() { Section = "1", Semester = "Spring 2021", TheCourse = courses[0] },
                new CourseOffering() { Section = "1", Semester = "Spring 2021", TheCourse = courses[1] },
                // different semester: should be excluded
                new CourseOffering() { Section = "1", Semester = "Fall 2021", TheCourse = courses[0] },
                // course not in the goal: should be excluded
                new CourseOffering()
                {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = new Course() { Name = "MATH 101", Title = "math", Credits = 3.0, Description = "math descr" }
                }
            });
            var courseServices = new CourseServices(mockRepository.Object);

            // Act
            var result = courseServices.getOfferingsByGoalIdAndSemester("CG1", "Spring 2021");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, o => Assert.Equal("Spring 2021", o.Semester));
            Assert.Contains(result, o => o.TheCourse.Name == "ARTD 201");
            Assert.Contains(result, o => o.TheCourse.Name == "ARTS 101");
        }

        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndNoCourseOfferingIsInSemester_EmtpyListIsReturned()
        {
            // Arrange
            var courses = GetTestCourses();
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>()
            {
                new CoreGoal() { Courses = courses, Description = "test", Id = "CG1", Name = "English Literacy" }
            });
            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>()
            {
                new CourseOffering() { Section = "1", Semester = "Fall 2021", TheCourse = courses[0] }
            });
            var courseServices = new CourseServices(mockRepository.Object);

            // Act
            var result = courseServices.getOfferingsByGoalIdAndSemester("CG1", "Spring 2021");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // ---------- getCourses ----------

        [Fact]
        public void GetCourses_RepositoryHasCourses_AllCoursesAreReturned()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(GetTestCourses());
            var courseServices = new CourseServices(mockRepository.Object);

            // Act
            var result = courseServices.getCourses();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "ARTD 201");
            Assert.Contains(result, c => c.Name == "ARTS 101");
        }

        [Fact]
        public void GetCourses_CoursesAreUnordered_CoursesAreReturnedSortedByName()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course>()
            {
                new Course() { Name = "STAT 201", Title = "stats", Credits = 4.0, Description = "d" },
                new Course() { Name = "ARTD 201", Title = "graphic design", Credits = 3.0, Description = "d" },
                new Course() { Name = "CSCI 330", Title = "languages", Credits = 3.0, Description = "d" }
            });
            var courseServices = new CourseServices(mockRepository.Object);

            // Act
            var result = courseServices.getCourses();

            // Assert
            Assert.Equal(new[] { "ARTD 201", "CSCI 330", "STAT 201" }, result.Select(c => c.Name));
        }

        [Fact]
        public void GetCourses_ResultIsModified_RepositoryListIsNotAffected()
        {
            // Arrange
            var repoCourses = GetTestCourses();
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(repoCourses);
            var courseServices = new CourseServices(mockRepository.Object);

            // Act
            var result = courseServices.getCourses();
            result.Clear();

            // Assert
            Assert.Equal(2, repoCourses.Count);
        }

        private List<Course> GetTestCourses()
        {
            return new List<Course>()
            {
                new Course() { Name="ARTD 201", Title="graphic design", Credits=3.0, Description="graphic design descr" },
                new Course() { Name="ARTS 101", Title="art studio", Credits=3.0, Description="studio descr" }
            };
        }
    }
}
