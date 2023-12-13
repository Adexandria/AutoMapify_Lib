using Automapify.Test.Models;
using Automappify.Services;

namespace Automapify.Test
{
    [TestFixture]
    public class MapperTests : TestHelper
    {
        [SetUp]
        protected override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void ShouldMapFromSourceObjectToNewObjectUsingAttributes()
        {
            var classroomDto = TestClassroom.Map<Classroom, ClassroomDto>();

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDto.Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDto.NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDto.NoOfLecturers));
            });
        }

        [Test]
        public void ShouldMapFromSourceObjectToNewObjectUsingConfiguration()
        {
            var classroomDto = TestClassroom.Map(MapifyConfiguration);

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDto.Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDto.NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDto.NoOfLecturers));
                Assert.That(TestClassroom.ClassCode.ToString(), Is.EqualTo(classroomDto.Code));
                Assert.That(classroomDto.IsActive,Is.True);
                Assert.That(TestClassroom.Courses.Select(s=>s.LeadLecturer.Name).Single(), Is.EqualTo(classroomDto.LeadLecturers.Single()));
            });
        }

        [Test]
        public void ShouldMapFromSourceObjectToExistingObjectUsingAttributes()
        {
            var classroomDto = new ClassroomDto();

            classroomDto.Map(TestClassroom);

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDto.Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDto.NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDto.NoOfLecturers));
            });
        }

        [Test]
        public void ShouldMapFromSourceObjectToExistingObjectUsingConfiguration()
        {
            var classroomDto = new ClassroomDto();

            classroomDto.Map(TestClassroom,MapifyConfiguration);

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDto.Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDto.NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDto.NoOfLecturers));
                Assert.That(TestClassroom.ClassCode.ToString(), Is.EqualTo(classroomDto.Code));
                Assert.That(classroomDto.IsActive, Is.True);
                Assert.That(TestClassroom.Courses.Select(s => s.LeadLecturer.Name).Single(), Is.EqualTo(classroomDto.LeadLecturers.Single()));
            });
        }
    }
}