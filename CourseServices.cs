using System;
using System.Collections.Generic;

namespace cs330_proj1
{
    public class CourseServices
    {
        private CourseRepository repo = new CourseRepository();


        //As a student, I want to search for course offerings that meet core goals 
        // so that I can register easily for courses that meet my program requirements
         public List<CourseOffering> getOfferingsByGoalIdAndSemester(String theGoalId, String semester) {
          //finish this method during the tutorial 
          //use the repo to get the data from the database (data store)
         List<CoreGoal> theGoals = repo.Goals;
         List<CourseOffering> theOfferings = repo.Offerings;

         //Complete any other required functionality/business logic to satisfy the requirement
         CoreGoal theGoal=null;
         foreach(CoreGoal cg in theGoals) {
         if(cg.Id.Equals(theGoalId)) {
            theGoal=cg; break;

            }
         }
         if(theGoal==null) throw new Exception("Didn't find the goal");
            //search list of courses, then for each course, search offerings
      List<CourseOffering> courseOfferingsThatMeetGoal = new List<CourseOffering>();

      foreach(CourseOffering c in theOfferings) {
     if(c.Semester.Equals(semester) 
        && theGoal.Courses.Contains(c.TheCourse) ) 
     {
        courseOfferingsThatMeetGoal.Add(c);
     }
 
      }
      return courseOfferingsThatMeetGoal;
        }

        
        //Add more service functions here, as needed, for the project

        /* As a student, I want to see all available courses so that I know what my options are */
        public List<Course> getCourses() {
            //return a copy so callers can't modify the repository's own list
            List<Course> allCourses = new List<Course>(repo.Courses);
            allCourses.Sort();
            return allCourses;
        }

        /* As a student, I want to see all course offerings by semester, so that I can choose from what's
           available to register for next semester */

        /* As a student I want to see all course offerings by semester and department so that I can 
        choose major courses to register for */

        /* As a student I want to see all courses that meet a core goal, so that I can plan out
           my courses over the next few semesters and choose core courses that make sense for me */

        /* As a student I want to find a course that meets two different core goals, so that I can
        "feed two birds with one seed" (save time by taking one class that will fulfill two 
          requirements */

        /* As a freshman adviser, I want to see all the core goals which do not have any course offerings 
           for a given semester, so that I can work with departments to get some courses offered
           that students can take to meet those goals */

        
     }
}
