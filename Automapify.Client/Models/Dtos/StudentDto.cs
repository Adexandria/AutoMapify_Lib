using Automapify.Services.Attributes;


namespace Automapify.Client.Models.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public bool IsDeleted { get; set; }
        public string Classroom { get; set; }

        [MapProperty("Classroom")]
        public Classroom Room { get; set; }

        public void DisplayFullName()
        {
            Console.WriteLine(Name);
        }

        public void DisplayCLassroom()
        {
            Console.WriteLine(Classroom);
        }

        public void DisplayAge()
        {
            Console.WriteLine(Age);
        }
    }
}
