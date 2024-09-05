using Automapify.Services;
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
        public void ShouldMapFromSourceObjectsToNewObjectsUsingAttributes()
        {
            var classroomDtos = TestClassrooms.Map<List<Classroom>, List<ClassroomDto>>();

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDtos.FirstOrDefault().Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfLecturers));
                Assert.IsTrue(classroomDtos.Any());
            });
        }

        [Test]
        public void ShouldMapFromSourceObjectToNewObjectsUsingAttributes()
        {
            var classroomDtos = TestClassroom.Map<Classroom, List<ClassroomDto>>();

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDtos.FirstOrDefault().Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfLecturers));
                Assert.IsTrue(classroomDtos.Any());
            });
        }

        [Test]
        public void ShouldMapFromSourceObjectToNewObjectUsingConfiguration()
        {
            var classroomDto = TestClassroom.Map<Classroom,ClassroomDto>(MapifyConfiguration);

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
        public void ShouldMapFromSourceObjectsToNewObjectsUsingConfiguration()
        {
            var classroomDtos = TestClassrooms.Map<List<Classroom>, List<ClassroomDto>>(MapifyConfiguration);

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDtos.FirstOrDefault().Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfLecturers));
                Assert.That(TestClassroom.ClassCode.ToString(), Is.EqualTo(classroomDtos.FirstOrDefault().Code));
                Assert.That(classroomDtos.FirstOrDefault().IsActive, Is.True);
                Assert.That(TestClassroom.Courses.Select(s => s.LeadLecturer.Name).Single(), Is.EqualTo(classroomDtos.FirstOrDefault().LeadLecturers.Single()));
            });
        }

        [Test]
        public void ShouldMapFromSourceObjectToNewObjectsUsingConfiguration()
        {
            var classroomDtos = TestClassroom.Map<Classroom, List<ClassroomDto>>(MapifyConfiguration);

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDtos.FirstOrDefault().Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfLecturers));
                Assert.That(TestClassroom.ClassCode.ToString(), Is.EqualTo(classroomDtos.FirstOrDefault().Code));
                Assert.That(classroomDtos.FirstOrDefault().IsActive, Is.True);
                Assert.That(TestClassroom.Courses.Select(s => s.LeadLecturer.Name).Single(), Is.EqualTo(classroomDtos.FirstOrDefault().LeadLecturers.Single()));
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
        public void ShouldMapFromSourceObjectsToExistingObjectsUsingAttributes()
        {
            var classroomDtos = new List<ClassroomDto>();

            classroomDtos.Map(TestClassrooms);

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDtos.FirstOrDefault().Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfLecturers));
            });
        }

        [Test]
        public void ShouldMapFromSourceObjectToExistingObjectsUsingAttributes()
        {
            var classroomDtos = new List<ClassroomDto>();

            classroomDtos.Map(TestClassroom);

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDtos.FirstOrDefault().Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfLecturers));
            });
        }


        [Test]
        public void ShouldMapFromSourceObjectsToExistingObjectsUsingConfiguration()
        {
            var classroomDtos = new List<ClassroomDto>();

            classroomDtos.Map(TestClassrooms, MapifyConfiguration);

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDtos.FirstOrDefault().Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfLecturers));
                Assert.That(TestClassroom.ClassCode.ToString(), Is.EqualTo(classroomDtos.FirstOrDefault().Code));
                Assert.That(classroomDtos.FirstOrDefault().IsActive, Is.True);
                Assert.That(TestClassroom.Courses.Select(s => s.LeadLecturer.Name).Single(), Is.EqualTo(classroomDtos.FirstOrDefault().LeadLecturers.Single()));
            });
        }


        [Test]
        public void ShouldMapFromSourceObjectToExistingObjectsUsingConfiguration()
        {
            var classroomDtos = new List<ClassroomDto>();

            classroomDtos.Map(TestClassroom, MapifyConfiguration);

            Assert.Multiple(() =>
            {
                Assert.That(TestClassroom.Name, Is.EqualTo(classroomDtos.FirstOrDefault().Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(classroomDtos.FirstOrDefault().NoOfLecturers));
                Assert.That(TestClassroom.ClassCode.ToString(), Is.EqualTo(classroomDtos.FirstOrDefault().Code));
                Assert.That(classroomDtos.FirstOrDefault().IsActive, Is.True);
                Assert.That(TestClassroom.Courses.Select(s => s.LeadLecturer.Name).Single(), Is.EqualTo(classroomDtos.FirstOrDefault().LeadLecturers.Single()));
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

        [Test]
        public void ShouldMapFromSourceObjectToExistingObjectWithIgnoreAttributes()
        {
            var studentClassroomDto = new StudentClassroomDto();

            studentClassroomDto.Map(TestClassroom);

            Assert.Multiple(() =>
            {
                Assert.That(string.IsNullOrEmpty(studentClassroomDto.Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(studentClassroomDto.NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(studentClassroomDto.NoOfLecturers));
                Assert.IsTrue(string.IsNullOrEmpty(studentClassroomDto.Code));
            });
        }

        [Test]
        public void ShouldMapFromSourceObjectToExistingObjectWithIgnoreConfiguration()
        {
            var studentClassroomDto = new StudentClassroomDto();

            var testHelper = new TestHelper();

            studentClassroomDto.Map(TestClassroom,testHelper.SetupIgnoreConfiguration());

            Assert.Multiple(() =>
            {
                Assert.That(string.IsNullOrEmpty(studentClassroomDto.Name));
                Assert.That(TestClassroom.NumberOfStudents, Is.EqualTo(studentClassroomDto.NoOfStudents));
                Assert.That(TestClassroom.NumberOfTeachers, Is.EqualTo(studentClassroomDto.NoOfLecturers));
                Assert.IsTrue(string.IsNullOrEmpty(studentClassroomDto.Code));
                Assert.That(studentClassroomDto.IsActive, Is.True);
                Assert.That(TestClassroom.Courses.Select(s => s.LeadLecturer.Name).Single(), Is.EqualTo(studentClassroomDto.LeadLecturers.Single()));
            });
        }



    }
}