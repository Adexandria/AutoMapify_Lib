using Automapify.Services;
using Automapify.Services.Extensions;
using Automapify.Test.Models;

namespace Automapify.Test
{
    public class TestHelper
    {
        [SetUp]
        protected virtual void Setup()
        {
            TestClassroom = SetUpInstance();
            TestClassrooms = SetUpInstances();
            SetupConfiguration();
        }
        private Classroom SetUpInstance()
        {
            return new Classroom("TestingClassrom", 10, 5, "G302")
                .SetUpCourse("Economics100","Ec100","Adeola");
        }


        private List<Classroom> SetUpInstances()
        {
            return new List<Classroom>() 
            { 
                TestClassroom
            };

        }

        private MapifyConfiguration SetupConfiguration()
        {
            MapifyConfiguration = new MapifyConfigurationBuilder<Classroom,ClassroomDto>()
                .Map(d=>d.Name,s=>s.Name)
                .Map(d=>d.NoOfLecturers, s=>s.NumberOfTeachers)
                .Map(d=>d.NoOfStudents, s=>s.NumberOfStudents)
                .Map(d=>d.LeadLecturers, s => s.Courses.MapFrom(s=>s.LeadLecturer.Name))
                .Map(d=>d.IsActive, s => s.NumberOfTeachers > 0 && s.NumberOfStudents > 0)
                .Map(d=> d.Code, s => s.ClassCode.ToString())
                .CreateConfig();
            return MapifyConfiguration;
        }
        protected MapifyConfiguration MapifyConfiguration { get; set; }
        protected Classroom TestClassroom { get; set; }

        protected List<Classroom> TestClassrooms { get; set; }
    }
}
