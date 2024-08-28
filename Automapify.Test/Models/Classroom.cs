using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Test.Models
{
    public class Classroom
    {
        public Classroom()
        {
            
        }
        public Classroom(string name,int noOfStudents, int noOfTeachers,string classCode)
        {
            Id = 1;
            Name = name;
            NumberOfStudents = noOfStudents;
            NumberOfTeachers = noOfTeachers;
            ClassCode = SetUpClassCode(classCode);
            Courses = new List<Course>();
        }

        private ClassCode SetUpClassCode(string classCode)
        {
            Enum.TryParse(classCode, out ClassCode code);
            return code;
        }

        public Classroom SetUpCourse(string name, string courseCode, string leadLecturer)
        {
            Courses.Add(new Course(name, courseCode).AddLeadLecturer(leadLecturer));
            return this;
        }
        public int Id { get; set; }
        public string Name { get; private set; }
        public int NumberOfStudents { get; private set; }
        public int NumberOfTeachers { get; private set; }
        public ClassCode ClassCode { get; private set; }
        public IList<Course> Courses { get; private set; }
    }
}
