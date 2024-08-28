namespace Automapify.Test.Models
{
    public class Lecturer
    {
        public Lecturer()
        {
            
        }
        public Lecturer(string name)
        {
            Id = 1;
            Name = name;
            Courses = new List<Course>();   
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }
}