using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automappify.Services;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Client
{
    public class BenchmarkTest
    {
        public BenchmarkTest()
        {
            _student = new Student(1, "Adeola", "Aderibigbe", "11/12/2000", "jss1");
        }

        [Benchmark]
        public void MapToAnExistingObject()
        {
            var studentDto = new StudentDto();
            var classroom = new Classroom("Jss2");
            studentDto.Map(classroom);
        }

        [Benchmark]
        public void MapToAnExistingObjectWithConfig()
        {
            var studentDto = new StudentDto();
            studentDto.Map(_student, MappingService.StudentConfig());
        }

        [Benchmark]
        public StudentDto MapToNewObject()
        {
            return _student.Map<Student, StudentDto>();
        }

        [Benchmark]
        public StudentDto MapToNewObjectWithConfig()
        {
            return _student.Map<Student, StudentDto>(MappingService.StudentConfig());
        }

        private readonly Student _student;
    }
}
