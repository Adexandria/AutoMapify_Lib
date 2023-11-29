using Automapify.Services.Attributes;


namespace Automapify.Client.Models.Dtos
{
    public class StudentDtos
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Classroom { get; set; }


        public void DisplayName()
        {
            Console.WriteLine($"{FirstName} {LastName}");
        }

        public void DisplayCLassroom()
        {
            Console.WriteLine(Classroom);
        }
    }
}
