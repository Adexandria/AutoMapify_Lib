using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automapify.Services;

namespace Automapify.Client
{
    public static class MappingService
    {
        public static MapifyConfiguration<Student,StudentDtos> StudentConfig()
        {
            return SettingConfiguration<Student, StudentDtos>
                .CreateConfiguration()
                .Map(d => d.Name, s => $"{s.FirstName} {s.LastName}");
        }
    }
}
