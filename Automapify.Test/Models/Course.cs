namespace Automapify.Test.Models
{
    public class Course
    {
        public Course(string name, string courseCode)
        {
            Id = 1;
            Name = name;
            CourseCode = courseCode;
        }

        public Course AddLeadLecturer(string name)
        {
            LeadLecturer = new Lecturer(name);
            return this;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public Lecturer LeadLecturer { get; set; }
    }
}