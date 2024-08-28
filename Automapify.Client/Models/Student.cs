using Automapify.Client.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Client.Models
{
    public class Student
    {
        public Student()
        {
                
        }
        public Student(int id, string firstName, string lastName, string dateOfBirth, string _class)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = DateTime.Parse(dateOfBirth);
            Class = _class;
            Teachers = new List<Teacher>();
            Classrooms = new List<Classroom>();
        }


        public void AddTeacher(string firstName, string lastName)
        {
            Teachers.Add(new Teacher(firstName, lastName).AssignClassroom(Classroom));
        }

        public void AddClassroom(string room)
        {
            Classrooms.Add(new Classroom(room));
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Class { get; set; }
        public Classroom Classroom { get; set; }
        public List<Classroom> Classrooms { get; set; } 
        public List<Teacher> Teachers { get; set; }
    }
}
