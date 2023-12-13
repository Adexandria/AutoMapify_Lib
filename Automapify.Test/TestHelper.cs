using Automapify.Services;
using Automapify.Test.Models;

namespace Automapify.Test
{
    public class TestHelper
    {
        [SetUp]
        protected virtual void Setup()
        {
            TestClassroom = SetUpInstance();
            SetupConfiguration();
        }
        private Classroom SetUpInstance()
        {
            return new Classroom("TestingClassrom", 10, 5, "G302")
                .SetUpCourse("Economics100","Ec100","Adeola");
        }

        
        private MapifyConfiguration<Classroom, ClassroomDto> SetupConfiguration()
        {
            MapifyConfiguration = SettingConfiguration<Classroom,ClassroomDto>.CreateConfig()
                .Map(d=>d.Name,s=>s.Name)
                .Map(d=>d.NoOfLecturers, s=>s.NumberOfTeachers)
                .Map(d=>d.NoOfStudents, s=>s.NumberOfStudents)
                .Map(d=>d.LeadLecturers, s=>s.Courses.Select(s=>s.LeadLecturer.Name).ToList())
                .Map(d=>d.IsActive, s=>s.NumberOfTeachers > 0 && s.NumberOfStudents > 0)
                .Map(d=> d.Code, s=>s.ClassCode.ToString());
            return MapifyConfiguration;
        }
        protected MapifyConfiguration<Classroom,ClassroomDto> MapifyConfiguration { get; set; }
        protected Classroom TestClassroom { get; set; }
    }
}
