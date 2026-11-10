using System;

namespace cs330_proj1
{
    public class Course: IComparable<Course>
    {
        public string Name {get;set;}
        public string Title {get;set;}
        public double Credits {get;set;}
        public string Description {get;set;}

        public override String ToString() {
            return $"{Name}: {Title} ({Credits})\n{Description}\n";

        }
        public int CompareTo(Course other) {
            return this.Name.CompareTo(other.Name);
        }
    }
}
