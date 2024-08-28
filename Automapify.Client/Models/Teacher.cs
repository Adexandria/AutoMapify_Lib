using Automapify.Client.Models.Dtos;

namespace Automapify.Client.Models
{
    public class Teacher
    {
        public Teacher()
        {
                
        }
        public Teacher(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Teacher AssignClassroom(Classroom classroom)
        {
            Classroom = classroom;
            return this;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Classroom Classroom { get; set; }
    }
}