using System;

namespace cs330_proj1
{
    public class Course: IComparable<Course>
    {
        public string Name {get;set;}
        public string Title {get;set;}
        public double Credits {get;set;}
        public string Description {get;set;}

        //the department is the prefix of the course name, e.g. "CSCI" for "CSCI 330"
        public string Department {
            get { return Name.Split(' ')[0]; }
        }

        public override String ToString() {
            return $"{Name}: {Title} ({Credits})\n{Description}\n";

        }
        public int CompareTo(Course other) {
            return this.Name.CompareTo(other.Name);
        }
    }
}
