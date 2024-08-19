using Automapify.Client.Extensions;
using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automapify.Services;

namespace Automapify.Client
{
    public static class MappingService
    {
        public static MapifyConfiguration StudentConfig()
        {
            return new MapifyConfigurationBuilder<Student, StudentDto>()
                .Map(d => d.Name, s => $"{s.FirstName} {s.LastName}")
                .Map(d=> d.Classroom, s => s.Classroom.Room)
                .Map(d=> d.Room, s => s.Classroom)
                .Map(d => d.Age, s => s.DateOfBirth.ToAge())
                .Map(d=>d.DOB, s => s.DateOfBirth)
                .Map(d=>d.IsDeleted, s => false)
                .CreateConfig();
        }
    }
}
