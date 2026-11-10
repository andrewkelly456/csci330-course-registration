using System;
using cs330_proj1;
using System.Collections.Generic;

namespace cs330courses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("My name is Andrew");
            
            CourseServices service = new CourseServices();

            Console.WriteLine("---------");
            List<CourseOffering> theList = service.getOfferingsByGoalIdAndSemester("CG2","Spring 2021");
            foreach(CourseOffering c in theList) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            
            // Make each of the next 6 sections of code work
            /*
            List<Course> theList2 = service.getCourses();
            foreach(Course c in theList2) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            List<CourseOffering> theList3 = service.getCourseOfferingsBySemester("Fall 2020");
            foreach(CourseOffering c in theList3) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            List<CourseOffering> theList4 = service.getCourseOfferingsBySemesterAndDept("Fall 2020","CSCI");
            foreach(CourseOffering c in theList4) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            
            List<Course> theList5 = service.getCoursesByGoalId("CG2");
            foreach(Course c in theList5) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            List<Course> theList6 = service.getCoursesByGoalIds("CG2","CG1");
            foreach(Course c in theList6) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            List<CoreGoal> theList7 = service.getCoreGoalsThatAreNotCoveredBySemester("Fall 2020");
            foreach(CoreGoal c in theList7) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");
           */


            

        }//end main
    }
}
