using Automapify.Client.Extensions;
using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automapify.Services;

namespace Automapify.Client
{
    public static class MappingService
    {
        public static MapifyConfiguration<Student,StudentDtos> StudentConfig()
        {
            return new MapifyConfigurationBuilder<Student, StudentDtos>()
                .Map(d => d.Name, s => $"{s.FirstName} {s.LastName}")
                .Map(d => d.Age, s => s.DateOfBirth.ToAge())
                .Map(d=>d.DOB, s =>s.DateOfBirth)
                .Map(d=>d.IsDeleted, s => false)
                .CreateConfig();
        }
    }
}
